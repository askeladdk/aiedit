using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Globalization;
using System.Text.RegularExpressions;

namespace AIEdit
{
	using IniDictionary = Dictionary<string, OrderedDictionary>;

	public partial class frmMainNew : Form
	{
		private static uint ID_BASE = 0x02000000;
		private static uint ID_BASE_FS = 0x03000000;
		private uint idCounter = ID_BASE;
		private string idPrefix = null;
		private string idSuffix = null;
		private HashSet<string> iniIDs;
		private bool sameUnitMultiEntry = false;

		// TECHNOTYPE TABLES
		private List<TechnoType> unitTypes;     // sorted by name
		private List<TechnoType> buildingTypes; // sorted by [BuildingTypes] index (very important)
		private List<TechnoType> technoTypes;   // sorted by name except <none>

		// AI CONFIG TABLES
		private List<AITypeListEntry> groupTypes;
		private List<AITypeListEntry> veterancyTypes;
		private List<AITypeListEntry> mindControlTypes;
		private List<AITypeListEntry> teamHouseTypes;
		private List<AITypeListEntry> triggerHouseTypes;
		private List<AITypeListEntry> sides;
		private List<AITypeListEntry> conditionTypes;
		private List<AITypeListEntry> operatorTypes;
		private List<IActionType> actionTypes;
		private List<TeamTypeOption> teamTypeOptions;
		private Dictionary<string, TriggerTypeOption> triggerTypeOptions;

		// AI TABLES
		private AITable<TaskForce> taskForces;
		private AITable<ScriptType> scriptTypes;
		private AITable<TeamType> teamTypes;
		private AITable<TriggerType> triggerTypes;

		// OTHER VALUES
		private TeamType noneTeam = new TeamType("<none>", "<none>", null);
		private string digestString = null;

		private Logger logger = new Logger();
		private string saveFilename = null;

		private string nextID()
		{
			uint id;
			string newID;

			do
			{
				id = idCounter++;
				newID = idPrefix + id.ToString("X8") + idSuffix;
			} while (iniIDs.Contains(newID));

			return newID;
		}

		private void ParseTechnoTypes(List<TechnoType> technos, IniDictionary ini, string type, string editorName)
		{
			uint tableIndex = 0;
			foreach (DictionaryEntry de in ini[type])
			{
				string id = de.Value as string;

				if (ini.ContainsKey(id))
				{
					OrderedDictionary section = ini[id];
					string name = id;
					int cost = 0;
                    try
                    {
                        if(section.Contains("Cost"))
                        {
                            cost = int.Parse(section["Cost"] as string, NumberStyles.Any);
                        }
                    }
                    catch (Exception e)
                    {
						logger.Add("Error occured in parsing of Cost in [" + name + "] in rules!");
                    }

					if (section.Contains(editorName)) name = section[editorName] as string;
					else if (section.Contains("Name")) name = section["Name"] as string;
					
					name = name + " [" + id + "]";

					TechnoType tt = technos.SingleOrDefault(t => t.ID == id);

					if(tt == null)
					{
						tt = new TechnoType(id, name, cost, tableIndex++);
						technos.Add(tt);
					}
					else
					{
						logger.Add("Duplicate " + type + " [" + id + "] in rules!");
					}
				}
				else
				{
					tableIndex++;
				}
			}
		}

		private IList ToStringList(OrderedDictionary od)
		{
			List<IParamListEntry> lst = new List<IParamListEntry>();
			uint idx = 0;
			foreach (DictionaryEntry entry in od)
			{
				lst.Add(new ParamListEntry(entry.Value as string, idx++));
			}
			return lst;
		}

		private List<IActionType> LoadActionTypes(IniDictionary config, IniDictionary rules)
		{
			List<IActionType> actionTypes = new List<IActionType>();

			List<IParamListEntry> buildings = new List<IParamListEntry>();
			foreach (TechnoType tt in buildingTypes) buildings.Add(tt);

			Dictionary<string, IList> typeLists = new Dictionary<string, IList>()
			{
				{"BuildingTypes", buildings},
				{"NoTypes", ToStringList(config["NoTypes"])},
				{"TargetTypes", ToStringList(config["TargetTypes"])},
				{"UnloadTypes", ToStringList(config["UnloadTypes"])},
				{"MissionTypes", ToStringList(config["MissionTypes"])},
				{"FacingTypes", ToStringList(config["FacingTypes"])},
				{"TalkBubbleTypes", ToStringList(config["TalkBubbleTypes"])},
			};
            // Script and team index has to be fetched dynamically and any updates 
            // to those list should address its uses. TS Firestorm expansion uses 
            // merged index of ai.ini + aifs.ini.

            // Custom script param dropdowns
            if (config.ContainsKey("ScriptParamTypes"))
            {
                foreach(DictionaryEntry entry in config["ScriptParamTypes"])
                {
                    string[] split = (entry.Value as string).Split(',');
                    string sectionName = split[0].Trim();
                    uint sectionFrom = 0;
                    uint.TryParse(split[1] as string, out sectionFrom);
                    if (!string.IsNullOrEmpty(sectionName))
                    {
                        if (sectionFrom == 0 && config.ContainsKey(sectionName))
                        {
                            typeLists.Add(sectionName, ToStringList(config[sectionName]));
                        }
                        else if (sectionFrom == 1 && rules.ContainsKey(sectionName))
                        {
                            typeLists.Add(sectionName, ToStringList(rules[sectionName]));
                        }
                    }
                }
            }

			foreach(DictionaryEntry entry in config["ActionTypes"])
			{
				uint code = uint.Parse(entry.Key as string);
				string[] split = (entry.Value as string).Split(',');
				string name = split[0];
				string type = split[1];
				string desc = split[2];
				IActionType actionType;

				if (desc.Length == 0) desc = name + ".";

				if(type.CompareTo("Number") == 0)
				{
					actionType = new ActionTypeNumber(code, name, desc);
				}
				else if(type.CompareTo("NumPlusMinus") == 0)
				{
					actionType = new ActionTypeNumPlusMinus(code, name, desc);
				}
				else if (type.CompareTo("BuildingTypes") == 0)
				{
					actionType = new ActionTypeList(code, name, desc, typeLists[type], ScriptParamType.TechnoType);
				}
				else if (typeLists.ContainsKey(type))
				{
					actionType = new ActionTypeList(code, name, desc, typeLists[type], ScriptParamType.List);
				}
                else
                {
					actionType = new ActionTypeNumber(code, name, desc);
                }

				actionTypes.Add(actionType);
			}

			return actionTypes;
		}

		private List<AITypeListEntry> LoadAITypeList(IniDictionary config, string sectionName,
			List<AITypeListEntry> listTypes)
		{
			OrderedDictionary section = config[sectionName];
			foreach(DictionaryEntry entry in section)
			{
				int idx = int.Parse(entry.Key as string);
				string name = entry.Value as string;
				listTypes.Add(new AITypeListEntry(idx, name));
			}
			return listTypes;
		}

		private List<AITypeListEntry> LoadAITypeList(IniDictionary config, string sectionName)
		{
			List<AITypeListEntry> listTypes = new List<AITypeListEntry>();
			return LoadAITypeList(config, sectionName, listTypes);
		}

		private List<TeamTypeOption> LoadTeamTypeOptions(IniDictionary config)
		{
			List<TeamTypeOption> options = new List<TeamTypeOption>();
			OrderedDictionary section = config["TeamTypeOptions"];

			foreach(DictionaryEntry entry in section)
			{
				string[] split = (entry.Value as string).Split(',');
				string tag = split[0];
				string name = split[1];
				string type = split[2];
				TeamTypeOption option = null;

				if(type.CompareTo("TASKFORCES") == 0)
				{
					option = new TeamTypeOptionAIObject(tag, name, taskForces.Items);
				}
				else if (type.CompareTo("SCRIPTTYPES") == 0)
				{
					option = new TeamTypeOptionAIObject(tag, name, scriptTypes.Items);
				}
				else if (type.CompareTo("VETERANCY") == 0)
				{
					option = new TeamTypeOptionList(tag, name, veterancyTypes);
				}
				else if (type.CompareTo("GROUP") == 0)
				{
					option = new TeamTypeOptionList(tag, name, groupTypes);
				}
				else if (type.CompareTo("MIND") == 0)
				{
					option = new TeamTypeOptionList(tag, name, mindControlTypes);
				}
				else if (type.CompareTo("HOUSE") == 0)
				{
					option = new TeamTypeOptionStringList(tag, name, teamHouseTypes);
				}
				else if(type.CompareTo("BOOL") == 0)
				{
					option = new TeamTypeOptionBool(tag, name);
				}
				else
				{
					option = new TeamTypeOptionNumber(tag, name);
				}

				options.Add(option);
			}

			return options;
		}

		private Dictionary<string, TriggerTypeOption> LoadTriggerTypeOptions(IniDictionary config)
		{
			Dictionary<string, TriggerTypeOption> options = new Dictionary<string, TriggerTypeOption>();
			OrderedDictionary section = config["TriggerTypeOptions"];
			int sortOrder = 0;

			foreach(DictionaryEntry entry in section)
			{
				string[] split = (entry.Value as string).Split(',');
				string key  = split[0];
				string name = split[1];
				string type = split[2];
				TriggerTypeOption option = null;

				if (type.CompareTo("SIDE") == 0)
				{
					option = new TriggerTypeOptionList(name, sortOrder, sides);
				}
				else if (type.CompareTo("HOUSE") == 0)
				{
					option = new TriggerTypeOptionStringList(name, sortOrder, triggerHouseTypes);
				}
				else if (type.CompareTo("TECHNOTYPE") == 0)
				{
					option = new TriggerTypeOptionTechno(name, sortOrder, technoTypes);
				}
				else if (type.CompareTo("TEAM") == 0)
				{
					option = new TriggerTypeOptionAIObject(name, sortOrder, teamTypes.Items);
				}
				else if (type.CompareTo("CONDITION") == 0)
				{
					option = new TriggerTypeOptionList(name, sortOrder, conditionTypes);
				}
				else if (type.CompareTo("OPERATOR") == 0)
				{
					option = new TriggerTypeOptionList(name, sortOrder, operatorTypes);
				}
				else if (type.CompareTo("BOOL") == 0)
				{
					option = new TriggerTypeOptionBool(name, sortOrder);
				}
				else
				{
					option = new TriggerTypeOptionNumber(name, sortOrder);
				}

				options.Add(key, option);
				sortOrder++;
			}

			return options;
		}

		private void LoadTechnoTypes(IniDictionary rules, string editorName)
		{
			List<TechnoType> sortTechnoTypes = new List<TechnoType>();

			// load units
			unitTypes = new List<TechnoType>();
			ParseTechnoTypes(unitTypes, rules, "AircraftTypes", editorName);
			ParseTechnoTypes(unitTypes, rules, "InfantryTypes", editorName);
			ParseTechnoTypes(unitTypes, rules, "VehicleTypes", editorName);
			unitTypes.Sort();

			// load buildings
			buildingTypes = new List<TechnoType>();
			ParseTechnoTypes(buildingTypes, rules, "BuildingTypes", editorName);
			buildingTypes.Sort();

			// combine technotypes
			technoTypes = new List<TechnoType>();
			sortTechnoTypes.AddRange(unitTypes);
			sortTechnoTypes.AddRange(buildingTypes);
			sortTechnoTypes.Sort();

			technoTypes.Add(new TechnoType("<none>", "<none>", 0, 0));
			technoTypes.AddRange(sortTechnoTypes);
		}

		private void LoadData(string rulesPath, string aiPath)
		{
			IniDictionary rules = IniParser.ParseToDictionary(rulesPath, logger);
			IniDictionary ai = IniParser.ParseToDictionary(aiPath, logger);
			IniDictionary config;
			string appPath = "";
			string configPath = "";
			NumberFormatInfo culture = CultureInfo.InvariantCulture.NumberFormat;
			string aiFilename = Path.GetFileName(aiPath);

			appPath = System.AppDomain.CurrentDomain.BaseDirectory;
			configPath = appPath + "config\\ts.ini";

			// autodetect yr
			if (rules["General"].Contains("DominatorWarhead")) configPath = appPath + "config\\yr.ini";
			// autodetect ra2
			else if( rules["General"].Contains("PrismType") ) configPath = appPath + "config\\ra2.ini";

			config = IniParser.ParseToDictionary(configPath, logger);

			idCounter = ID_BASE;
			if (config.ContainsKey("General"))
			{
				if (config["General"].Contains("StartIndex"))
				{
					uint.TryParse(config["General"].GetString("StartIndex"), NumberStyles.HexNumber, culture, out idCounter);
				}
				
                if (!string.IsNullOrEmpty(aiFilename) && aiFilename.ToLower().Contains("aifs"))
                {
                    idCounter = ID_BASE_FS;
                    if (config["General"].Contains("StartIndexFS"))
                    {
                        uint.TryParse(config["General"].GetString("StartIndexFS"), NumberStyles.HexNumber, culture, out idCounter);
                    }
                }
				string idPrefixTemp = "";
				string idSuffixTemp = "";
				idPrefix = "";
				idSuffix = "-G";

				if (config["General"].Contains("IDPrefix")) idPrefixTemp = config["General"].GetString("IDPrefix");
				if (config["General"].Contains("IDSuffix")) idSuffixTemp = config["General"].GetString("IDSuffix");
				if (!String.IsNullOrEmpty(idPrefixTemp))
				{
					if (Regex.IsMatch(idPrefixTemp, @"^[a-zA-Z0-9_-]+$") && idPrefixTemp.Length < 16)
						idPrefix = idPrefixTemp.ToUpper();
				}
				if (!String.IsNullOrEmpty(idSuffixTemp))
				{
					if (Regex.IsMatch(idSuffixTemp, @"^[a-zA-Z0-9_-]+$"))
						idSuffix = idSuffixTemp.ToUpper();
				}

				string unitMultiEntry = "";
				if (config["General"].Contains("SameUnitMultiEntry")) unitMultiEntry = config["General"].GetString("SameUnitMultiEntry");
				if (!String.IsNullOrEmpty(unitMultiEntry) && (unitMultiEntry.Equals("yes", StringComparison.InvariantCultureIgnoreCase) || 
					unitMultiEntry.Equals("true", StringComparison.InvariantCultureIgnoreCase)))
					sameUnitMultiEntry = true;
			}

			if (ai.ContainsKey("Digest")) digestString = ai["Digest"].GetString("1");
			else digestString = config["General"].GetString("Digest");

			string sectionHouses = config["General"].GetString("Houses");
			string editorName    = config["General"].GetString("EditorName");

			LoadTechnoTypes(rules, editorName);

			teamHouseTypes = LoadAITypeList(rules, sectionHouses);
			teamHouseTypes.Add(new AITypeListEntry(-1, "<none>"));
			teamHouseTypes.Sort();

			triggerHouseTypes = LoadAITypeList(rules, sectionHouses);
			triggerHouseTypes.Add(new AITypeListEntry(-1, "<all>"));
			triggerHouseTypes.Sort();

			scriptTypes = new AITable<ScriptType>("ScriptTypes", new List<ScriptType>());
			teamTypes = new AITable<TeamType>("TeamTypes", new List<TeamType>());

			actionTypes = LoadActionTypes(config, rules);
			groupTypes = LoadAITypeList(config, "Group");
			veterancyTypes = LoadAITypeList(config, "VeteranLevels");
			mindControlTypes = LoadAITypeList(config, "MCDecisions");
			sides = LoadAITypeList(config, "Sides");
			conditionTypes = LoadAITypeList(config, "Conditions");
			operatorTypes = LoadAITypeList(config, "Operators");

			// TaskForces being 1st of the 4 types, ID duplicates accross ai(md).ini is not checked.
			taskForces = LoadTaskForces(ai, technoTypes, groupTypes); 

			scriptTypes = LoadScriptTypes(ai, actionTypes);

			teamTypeOptions = LoadTeamTypeOptions(config);
			teamTypes = LoadTeamTypes(ai, teamTypeOptions);

			triggerTypeOptions = LoadTriggerTypeOptions(config);
			triggerTypes = LoadTriggerTypes(ai);
		}

		private AITable<TaskForce> LoadTaskForces(IniDictionary ai,
			List<TechnoType> technoTypes, List<AITypeListEntry> groupTypes)
		{
			HashSet<string> ids = new HashSet<string>();
			List<TaskForce> items = new List<TaskForce>();
			OrderedDictionary aiTaskForces = ai["TaskForces"] as OrderedDictionary;

			foreach (DictionaryEntry entry in aiTaskForces)
			{
				string id = entry.Value as string;

				if (id == "") continue;

				if(ids.Contains(id))
				{
					logger.Add("Duplicate Task Force [" + id + "] found!");
					continue;
				}

				if(!ai.ContainsKey(id))
				{
					logger.Add("Listed Task Force [" + id + "] does not exist!");
					continue;
				}

				OrderedDictionary section = ai[id];
				TaskForce tf = TaskForce.Parse(id, section, unitTypes, groupTypes, logger);
				items.Add(tf);
				ids.Add(id);
			}

			iniIDs = new HashSet<string>();
			iniIDs.UnionWith(ids);
			return new AITable<TaskForce>("TaskForces", items);
		}

		private AITable<ScriptType> LoadScriptTypes(IniDictionary ai,
			List<IActionType> actionTypes)
		{
			HashSet<string> ids = new HashSet<string>();
			List<ScriptType> items = new List<ScriptType>();
			OrderedDictionary aiScriptTypes = ai["ScriptTypes"] as OrderedDictionary;

			foreach(DictionaryEntry entry in aiScriptTypes)
			{
				string id = entry.Value as string;

				if (id == "") continue;

				if (ids.Contains(id) || iniIDs.Contains(id))
				{
					if (iniIDs.Contains(id))
						logger.Add("Duplicate Script [" + id + "] found in other list(s)!");
					else
						logger.Add("Duplicate Script [" + id + "] found!");
					continue;
				}

				if (!ai.ContainsKey(id))
				{
					logger.Add("Listed Script [" + id + "] does not exist!");
					continue;
				}

				OrderedDictionary section = ai[id];
				ScriptType tf = ScriptType.Parse(id, section, actionTypes, logger);
				items.Add(tf);
				ids.Add(id);
			}
			iniIDs.UnionWith(ids);
			return new AITable<ScriptType>("ScriptTypes", items);
		}

		private AITable<TeamType> LoadTeamTypes(IniDictionary ai,
			List<TeamTypeOption> teamTypeOptions)
		{
			HashSet<string> ids = new HashSet<string>();
			List<TeamType> items = new List<TeamType>();
			OrderedDictionary aiTeamTypes = ai["TeamTypes"] as OrderedDictionary;

			foreach(DictionaryEntry entry in aiTeamTypes)
			{
				string id = entry.Value as string;

				if (id == "") continue;

				if (ids.Contains(id) || iniIDs.Contains(id))
				{
					if (iniIDs.Contains(id))
						logger.Add("Duplicate Team [" + id + "] found in other list(s)!");
					else
						logger.Add("Duplicate Team [" + id + "] found!");
					continue;
				}

				if (!ai.ContainsKey(id))
				{
					logger.Add("Listed Team [" + id + "] does not exist!");
					continue;
				}

				OrderedDictionary section = ai[id];
				TeamType tt = TeamType.Parse(id, section, teamTypeOptions);
				items.Add(tt);
				ids.Add(id);
			}
			iniIDs.UnionWith(ids);
			return new AITable<TeamType>("TeamTypes", items);
		}

		private AITable<TriggerType> LoadTriggerTypes(IniDictionary ai)
		{
			HashSet<string> ids = new HashSet<string>();
			List<TriggerType> items = new List<TriggerType>();
			OrderedDictionary aiTriggerTypes = ai["AITriggerTypes"] as OrderedDictionary;

			foreach (DictionaryEntry entry in aiTriggerTypes)
			{
				string id = entry.Key as string;
				string data = entry.Value as string;

				// Vanilla TS AITrigger already contains 2 IDs same as TeamType IDs, so skipping other list check here.
				if (ids.Contains(id))
				{
					logger.Add("Duplicate Trigger [" + id + "] found!");
					continue;
				}

				TriggerType tr = TriggerType.Parse(id, data, triggerTypeOptions, noneTeam, technoTypes[0], logger);

				if (tr != null)
				{
					items.Add(tr);
					ids.Add(id);
				}
			}
			iniIDs.UnionWith(ids);
			return new AITable<TriggerType>("AITriggerTypes", items);
		}

		private void WriteAI(string path)
		{
			StreamWriter stream = new StreamWriter(path);
			
			taskForces.Write(stream);
			scriptTypes.Write(stream);
			teamTypes.Write(stream);
			triggerTypes.Write(stream, false);
			stream.WriteLine();

			stream.WriteLine("[Digest]");
			stream.WriteLine("1=" + digestString);
			stream.WriteLine();

			stream.Close();
		}

		private void UpdateTFCost()
		{
			TaskForce tf = olvTF.SelectedObject as TaskForce;
			if (tf == null) return;
			txtTFTotalCost.Text = "$" + tf.TotalCost().ToString();
		}

		private IEnumerable FilterTeamTypes(object val)
		{
			var tts = from tt in teamTypes.Items
					  where tt.HasObject(val)
					  select tt;
			return tts;
		}
	}
}
