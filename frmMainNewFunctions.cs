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
		private static int DEFAULT_GROUP = -1;
		private static uint ID_BASE = 0x01000000;
		private uint idCounter = ID_BASE;

		// TECHNOTYPE TABLES
		private List<TechnoType> unitTypes;     // sorted by name
		private List<TechnoType> buildingTypes; // sorted by [BuildingTypes] index (very important)
		private List<TechnoType> technoTypes;   // sorted by name
		private string[] houses;           // sorted by [Houses] index (very important)

		// AI CONFIG TABLES
		private List<GroupType> groupTypes;
		private List<IActionType> actionTypes;

		// AI TABLES
		//private OrderedDictionary taskForces;
		private AITable<TaskForce> taskForces;
		private AITable<ScriptType> scriptTypes;

		private string nextID()
		{
			uint id = idCounter++;
			return id.ToString("X8") + "-G";
		}

		private void LoadTechnoTypes(List<TechnoType> technos, IniDictionary ini, string type, string editorName)
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

		private void LoadRules(string path, string sectionHouses, string editorName)
		{
			IniDictionary ini = IniParser.ParseToDictionary(path);

			// load houses
			houses = ini[sectionHouses].Values.Cast<string>().ToArray();

			// load units
			unitTypes = new List<TechnoType>();
			LoadTechnoTypes(unitTypes, ini, "AircraftTypes", editorName);
			LoadTechnoTypes(unitTypes, ini, "InfantryTypes", editorName);
			LoadTechnoTypes(unitTypes, ini, "VehicleTypes", editorName);
			unitTypes.Sort();

			// load buildings
			buildingTypes = new List<TechnoType>();
			LoadTechnoTypes(buildingTypes, ini, "BuildingTypes", editorName);
			buildingTypes.Sort();

			// sort and combine technotypes
			/*
			technoTypes = new List<TechnoType>();
			technoTypes.AddRange(unitTypes);
			technoTypes.AddRange(buildingTypes);
			technoTypes.Sort();
			 */
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

		private List<GroupType> LoadGroupTypes(IniDictionary config)
		{
			List<GroupType> groupTypes = new List<GroupType>();
			OrderedDictionary section = config["Group"];
			foreach(DictionaryEntry entry in section)
			{
				int idx = int.Parse(entry.Key as string);
				string name = entry.Value as string;
				groupTypes.Add(new GroupType(idx, name));
			}
			return groupTypes;
		}

		private void LoadConfig(IniDictionary config)
		{
			actionTypes = LoadActionTypes(config);
			groupTypes = LoadGroupTypes(config);
		}

		private AITable<TaskForce> LoadTaskForces(IniDictionary ai,
			List<TechnoType> technoTypes, List<GroupType> groupTypes)
		{
			List<TaskForce> items = new List<TaskForce>();
			OrderedDictionary aiTaskForces = ai["TaskForces"] as OrderedDictionary;

			foreach (DictionaryEntry entry in aiTaskForces)
			{
				string id = entry.Value as string;
				OrderedDictionary section = ai[id] as OrderedDictionary;
				TaskForce tf = TaskForce.Parse(id, section, unitTypes, groupTypes);
				items.Add(tf);
			}

			return new AITable<TaskForce>("TaskForces", items);
		}

		private AITable<ScriptType> LoadScriptTypes(IniDictionary ai, List<IActionType> actionTypes)
		{
			List<ScriptType> items = new List<ScriptType>();
			OrderedDictionary aiScriptTypes = ai["ScriptTypes"] as OrderedDictionary;

			foreach(DictionaryEntry entry in aiScriptTypes)
			{
				string id = entry.Value as string;
				OrderedDictionary section = ai[id] as OrderedDictionary;
				ScriptType tf = ScriptType.Parse(id, section, actionTypes);
				items.Add(tf);
			}

			return new AITable<ScriptType>("ScriptTypes", items);
		}

		private void LoadAI(string path)
		{
			IniDictionary ai = IniParser.ParseToDictionary(path);

			idCounter = ID_BASE;
			if (ai.ContainsKey("AIEdit"))
			{
				idCounter = uint.Parse(ai["AIEdit"]["Counter"] as string);
			}

			taskForces = LoadTaskForces(ai, technoTypes, groupTypes);
			scriptTypes = LoadScriptTypes(ai, actionTypes);
		}

		private void WriteAI(string path)
		{
			StreamWriter stream = new StreamWriter(path);

			stream.WriteLine("[AIEdit]");
			stream.WriteLine("Index=" + idCounter);
			stream.WriteLine();

			taskForces.Write(stream);
			scriptTypes.Write(stream);

			stream.Close();
		}

		private void UpdateTFCost()
		{
			TaskForce tf = olvTF.SelectedObject as TaskForce;
			if (tf == null) return;
			txtTFTotalCost.Text = tf.TotalCost().ToString();
		}
	}
}
