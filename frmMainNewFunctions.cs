using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;


namespace AIEdit
{
	using IniDictionary = Dictionary<string, OrderedDictionary>;

	public partial class frmMainNew : Form
	{
		private static uint ID_BASE = 0x01000000;
		private uint idCounter = ID_BASE;

		// TECHNOTYPE TABLES
		private List<TechnoType> unitTypes;     // sorted by name
		private List<TechnoType> buildingTypes; // sorted by [BuildingTypes] index (very important)
		private List<TechnoType> technoTypes;   // sorted by name

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

		private TeamType noneTeam = new TeamType("<none>", "<none>", null);

		// AI TABLES
		//private OrderedDictionary taskForces;
		private AITable<TaskForce> taskForces;
		private AITable<ScriptType> scriptTypes;
		private AITable<TeamType> teamTypes;
		private AITable<TriggerType> triggerTypes;

		private string nextID()
		{
			uint id = idCounter++;
			return id.ToString("X8") + "-G";
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
					uint cost = (section.Contains("Cost")) ? uint.Parse(section["Cost"] as string) : 0;

					if (section.Contains(editorName)) name = section[editorName] as string;
					else if (section.Contains("Name")) name = section["Name"] as string;

					TechnoType tt = technos.SingleOrDefault(t => t.ID == id);

					if(tt == null)
					{
						tt = new TechnoType(id, name, cost, tableIndex++);
						technos.Add(tt);
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

		private List<IActionType> LoadActionTypes(IniDictionary config)
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
				//{"ScriptTypes", OrderedDictToList<object>(config["ScriptTypes"])},
				//{"TeamTypes", OrderedDictToList<object>(config["TeamTypes"])},
			};

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
					/*
				else if (type.CompareTo("ScriptTypes") == 0)
				{
					actionType = new ActionTypeList(code, name, desc, scriptTypes.Items, ScriptParamType.AIObject);
				}
				else if (type.CompareTo("TeamTypes") == 0)
				{
					actionType = new ActionTypeList(code, name, desc, teamTypes.Items, ScriptParamType.AIObject);
				}
					 * */
				else if (type.CompareTo("BuildingTypes") == 0)
				{
					actionType = new ActionTypeList(code, name, desc, typeLists[type], ScriptParamType.TechnoType);
				}
				else
				{
					actionType = new ActionTypeList(code, name, desc, typeLists[type], ScriptParamType.List);
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
			technoTypes.Add(new TechnoType("<none>", "<none>", 0, 0));
			technoTypes.AddRange(unitTypes);
			technoTypes.AddRange(buildingTypes);
			technoTypes.Sort();
		}

		private void LoadData(string rulesPath, string aiPath)
		{
			IniDictionary rules = IniParser.ParseToDictionary(rulesPath);
			IniDictionary ai = IniParser.ParseToDictionary(aiPath);
			IniDictionary config;
			string configPath = "config/ts.ini";

			// autodetect ra2/ts
			if( rules["General"].Contains("PrismType") ) configPath = "config/ra2.ini";
			config = IniParser.ParseToDictionary(configPath);

			idCounter = ID_BASE;
			if (ai.ContainsKey("AIEdit"))
			{
				idCounter = uint.Parse(ai["AIEdit"]["Index"] as string);
			}

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

			actionTypes = LoadActionTypes(config);
			groupTypes = LoadAITypeList(config, "Group");
			veterancyTypes = LoadAITypeList(config, "VeteranLevels");
			mindControlTypes = LoadAITypeList(config, "MCDecisions");
			sides = LoadAITypeList(config, "Sides");
			conditionTypes = LoadAITypeList(config, "Conditions");
			operatorTypes = LoadAITypeList(config, "Operators");

			taskForces = LoadTaskForces(ai, technoTypes, groupTypes);

			scriptTypes = LoadScriptTypes(scriptTypes, ai, actionTypes);

			teamTypeOptions = LoadTeamTypeOptions(config);
			teamTypes = LoadTeamTypes(teamTypes, ai, teamTypeOptions);

			triggerTypeOptions = LoadTriggerTypeOptions(config);
			triggerTypes = LoadTriggerTypes(ai);
		}

		private AITable<TaskForce> LoadTaskForces(IniDictionary ai,
			List<TechnoType> technoTypes, List<AITypeListEntry> groupTypes)
		{
			List<TaskForce> items = new List<TaskForce>();
			OrderedDictionary aiTaskForces = ai["TaskForces"] as OrderedDictionary;

			foreach (DictionaryEntry entry in aiTaskForces)
			{
				string id = entry.Value as string;
				OrderedDictionary section = ai[id];
				TaskForce tf = TaskForce.Parse(id, section, unitTypes, groupTypes);
				items.Add(tf);
			}

			return new AITable<TaskForce>("TaskForces", items);
		}

		private AITable<ScriptType> LoadScriptTypes(AITable<ScriptType> table, IniDictionary ai,
			List<IActionType> actionTypes)
		{
			List<ScriptType> items = table.Items;// new List<ScriptType>();
			OrderedDictionary aiScriptTypes = ai["ScriptTypes"] as OrderedDictionary;

			foreach(DictionaryEntry entry in aiScriptTypes)
			{
				string id = entry.Value as string;
				OrderedDictionary section = ai[id];
				ScriptType tf = ScriptType.Parse(id, section, actionTypes);
				items.Add(tf);
			}

			return table;// new AITable<ScriptType>("ScriptTypes", items);
		}

		private AITable<TeamType> LoadTeamTypes(AITable<TeamType> table, IniDictionary ai,
			List<TeamTypeOption> teamTypeOptions)
		{
			List<TeamType> items = table.Items;// new List<TeamType>();
			OrderedDictionary aiTeamTypes = ai["TeamTypes"] as OrderedDictionary;

			foreach(DictionaryEntry entry in aiTeamTypes)
			{
				string id = entry.Value as string;
				OrderedDictionary section = ai[id];
				TeamType tt = TeamType.Parse(id, section, teamTypeOptions);
				items.Add(tt);
			}

			return table;// new AITable<TeamType>("TeamTypes", items);
		}

		private AITable<TriggerType> LoadTriggerTypes(IniDictionary ai)
		{
			List<TriggerType> items = new List<TriggerType>();
			OrderedDictionary aiTriggerTypes = ai["AITriggerTypes"] as OrderedDictionary;

			foreach (DictionaryEntry entry in aiTriggerTypes)
			{
				string id = entry.Key as string;
				string data = entry.Value as string;
				TriggerType tr = TriggerType.Parse(id, data, triggerTypeOptions, noneTeam, technoTypes[0]);
				items.Add(tr);
			}

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
			stream.WriteLine("[AIEdit]");
			stream.WriteLine("Index=" + idCounter);
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
