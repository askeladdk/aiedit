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
		private OrderedDictionary unitTypes;     // sorted by name
		private OrderedDictionary buildingTypes; // sorted by [BuildingTypes] index (very important)
		private OrderedDictionary technoTypes;   // sorted by name
		private string[] houses;           // sorted by [Houses] index (very important)

		// AI CONFIG TABLES
		private OrderedDictionary tfGroup;
		private ScriptActionType[] scriptActionTypes;

		// AI TABLES
		private OrderedDictionary taskForces;

		private TaskForce activeTaskForce;

		private string nextID()
		{
			uint id = idCounter++;
			return id.ToString("X8") + "-G";
		}

		/*
		 * Sorts ordered dictionary so that indices are sorted by value.
		 */
		private OrderedDictionary SortOrderedDict<K, V>(OrderedDictionary od)
		{
			K[] keys = od.Keys.Cast<K>().ToArray();
			V[] vals = od.Values.Cast<V>().ToArray();
			Array.Sort<V, K>(vals, keys);
			OrderedDictionary od2 = new OrderedDictionary();
			for (int i = 0; i < keys.Length; i++) od2.Add(keys[i], vals[i]);
			return od2;
		}

		private void LoadTechnoTypes(OrderedDictionary technos, IniDictionary ini, string type, string editorName)
		{
			foreach (DictionaryEntry de in ini[type])
			{
				string id = de.Value as string;
				if (ini.ContainsKey(id))
				{
					OrderedDictionary section = ini[id];
					string name = id;
					int cost = (section.Contains("Cost")) ? int.Parse(section["Cost"] as string) : 0;

					if (section.Contains(editorName)) name = section[editorName] as string;
					else if (section.Contains("Name")) name = section["Name"] as string;

					TechnoType tt = new TechnoType(id, name, cost);
					technos[id] = tt;
				}
			}
		}

		private void LoadRules(string path, string sectionHouses, string editorName)
		{
			IniDictionary ini = IniParser.ParseToDictionary(path);

			// load houses
			houses = ini[sectionHouses].Values.Cast<string>().ToArray();

			// load units
			unitTypes = new OrderedDictionary();
			LoadTechnoTypes(unitTypes, ini, "AircraftTypes", editorName);
			LoadTechnoTypes(unitTypes, ini, "InfantryTypes", editorName);
			LoadTechnoTypes(unitTypes, ini, "VehicleTypes", editorName);
			unitTypes = SortOrderedDict<string, TechnoType>(unitTypes);

			// load buildings
			buildingTypes = new OrderedDictionary();
			LoadTechnoTypes(buildingTypes, ini, "BuildingTypes", editorName);

			// sort and combine technotypes
			technoTypes = new OrderedDictionary();
			foreach (DictionaryEntry entry in unitTypes) technoTypes[entry.Key] = entry.Value;
			foreach (DictionaryEntry entry in buildingTypes) technoTypes[entry.Key] = entry.Value;
			technoTypes = SortOrderedDict<string, TechnoType>(technoTypes);

			for(int i = 0; i < buildingTypes.Count; i++)
			{
				buildingTypes[i] = (buildingTypes[i] as TechnoType).Name;
			}
		}

		private void LoadConfig(IniDictionary config)
		{
			OrderedDictionary actionTypes = config["ActionTypes"];
			OrderedDictionary noTypes = config["NoTypes"];
			List<ScriptActionType> actions = new List<ScriptActionType>();

			cmbAction.Items.Clear();

			// script action types.
			foreach (DictionaryEntry entry in actionTypes)
			{
				string[] split = (entry.Value as string).Split(',');
				string name = split[0];
				string listType = split[1];
				string desc = split[2];
				ComboBoxStyle style = ComboBoxStyle.DropDownList;
				OrderedDictionary list = noTypes;

				if (desc.Length == 0) desc = name;

				if (listType == "Number")
				{
					style = ComboBoxStyle.DropDown;
					list  = null;
				}
				else if (listType == "BuildingTypes")
				{
					list = buildingTypes;
				}
				else
				{
					list = config[listType];
				}

				actions.Add(new ScriptActionType(name, desc, style, list));
				cmbAction.Items.Add(name);
			}

			cmbAction.SelectedIndex = 0;

			scriptActionTypes = actions.ToArray();

			// TaskForce groupings
			tfGroup = new OrderedDictionary();
			cmbTFGroup.Items.Clear();
			foreach (DictionaryEntry entry in config["Group"])
			{
				tfGroup.Add(int.Parse(entry.Key as string), entry.Value);
				cmbTFGroup.Items.Add(entry.Value);
			}
			cmbTFGroup.SelectedIndex = 0;
		}

		private void LoadAI(string path)
		{
			IniDictionary ai = IniParser.ParseToDictionary(path);
			OrderedDictionary tfTable = ai["TaskForces"];
			taskForces = new OrderedDictionary();

			idCounter = ID_BASE;
			if (ai.ContainsKey("AIEdit"))
			{
				idCounter = uint.Parse(ai["AIEdit"]["Counter"] as string);
			}

			for (int i = 0; i < tfTable.Count; i++)
			{
				string id = tfTable[i] as string;
				TaskForce tf = TaskForce.Parse(id, ai[id], technoTypes);
				taskForces.Add(id, tf);
			}
		}

		private void WriteTaskForces(StreamWriter writer)
		{
			int n = 0;

			writer.WriteLine("[TaskForces]");

			foreach(object id in taskForces.Keys)
			{
				writer.WriteLine(n + "=" + id);
				n++;
			}

			writer.WriteLine();

			foreach(DictionaryEntry entry in taskForces)
			{
				TaskForce tf = entry.Value as TaskForce;
				tf.Write(writer);
			}
		}

		private void WriteAI(string path)
		{
			StreamWriter writer = new StreamWriter(path);

			writer.WriteLine("[AIEdit]");
			writer.WriteLine("Index=" + idCounter);
			writer.WriteLine();

			WriteTaskForces(writer);

			writer.Close();
		}

		private void RefreshTFList(bool select=true)
		{
			int selected = lstTF.Items.Count > 0 ? lstTF.SelectedIndices[0] : 0;
			lstTF.Items.Clear();
			
			foreach (DictionaryEntry entry in taskForces)
			{
				TaskForce tf = entry.Value as TaskForce;
				string[] items = { tf.Name, tf.ID, "0" };
				ListViewItem lvi = new ListViewItem(items);
				lvi.Tag = tf;
				lstTF.Items.Add(lvi);
			}

			if (select && lstTF.Items.Count > 0)
			{
				selected = Math.Min(selected, lstTF.Items.Count - 1);
				lstTF.Items[selected].Selected = true;
			}
		}

		private int SelectedTFUnitIndex()
		{
			if (lstTFUnits.SelectedIndices.Count == 0) return 0;
			return lstTFUnits.SelectedIndices[0];
		}

		private bool ShowTF(TaskForce tf)
		{
			int total = 0;
			lstTFUnits.Items.Clear();
			foreach (DictionaryEntry entry in tf.Units)
			{
				TechnoType tt = entry.Key as TechnoType;
				int count = (int)entry.Value;
				string[] items = { count.ToString(), tt.Name, (count * tt.Cost).ToString() };
				ListViewItem lvi = new ListViewItem(items);
				lvi.Tag = tt;
				lstTFUnits.Items.Add(lvi);
				total += count * tt.Cost;
			}

			txtTFTotalCost.Text = "Total Cost: " + total;

			activeTaskForce = tf;
			return tf.Units.Count > 0;
		}

	}
}
