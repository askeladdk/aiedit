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

		private List<string> orderTaskForces;

		// TECHNOTYPE TABLES
		private List<TechnoType> unitTypes;     // sorted by name
		private List<string> buildingNames; // sorted by [BuildingTypes] index (very important)
		private List<TechnoType> technoTypes;   // sorted by name
		private string[] houses;           // sorted by [Houses] index (very important)

		// AI CONFIG TABLES
		private OrderedDictionary tfGroup;
		private ScriptActionType[] scriptActionTypes;

		// AI TABLES
		//private OrderedDictionary taskForces;
		private AITable<TaskForce> taskForces;

		private string nextID()
		{
			uint id = idCounter++;
			return id.ToString("X8") + "-G";
		}

		private void LoadTechnoTypes(List<TechnoType> technos, IniDictionary ini, string type, string editorName)
		{
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
						tt = new TechnoType(id, name, cost);
						technos.Add(tt);
					}
					else
					{
						tt.Name = name;
						tt.Cost = cost;
					}
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
			List<TechnoType> buildingTypes = new List<TechnoType>();
			LoadTechnoTypes(buildingTypes, ini, "BuildingTypes", editorName);

			// sort and combine technotypes
			technoTypes = new List<TechnoType>();
			technoTypes.AddRange(unitTypes);
			technoTypes.AddRange(buildingTypes);
			technoTypes.Sort();

			// Building names
			buildingNames = new List<string>();
			foreach(TechnoType blg in buildingTypes)
			{
				buildingNames.Add(blg.Name);
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
					list = null;// buildingNames;
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

		private AITable<TaskForce> LoadTaskForces(IniDictionary ai, List<TechnoType> technoTypes)
		{
			List<TaskForce> tfs = new List<TaskForce>();
			OrderedDictionary aiTaskForces = ai["TaskForces"] as OrderedDictionary;

			foreach (DictionaryEntry entry in aiTaskForces)
			{
				string id = entry.Value as string;
				OrderedDictionary section = ai[id] as OrderedDictionary;
				TaskForce tf = TaskForce.Parse(id, section, technoTypes);
				tfs.Add(tf);
			}

			return new AITable<TaskForce>("TaskForces", tfs);
		}

		private void LoadAI(string path)
		{
			IniDictionary ai = IniParser.ParseToDictionary(path);

			idCounter = ID_BASE;
			if (ai.ContainsKey("AIEdit"))
			{
				idCounter = uint.Parse(ai["AIEdit"]["Counter"] as string);
			}

			taskForces = LoadTaskForces(ai, technoTypes);
		}

		private void WriteAI(string path)
		{
			StreamWriter stream = new StreamWriter(path);

			stream.WriteLine("[AIEdit]");
			stream.WriteLine("Index=" + idCounter);
			stream.WriteLine();

			taskForces.Write(stream);

			stream.Close();
		}

		private void UpdateTFCost()
		{
			TaskForce tf = olvTF.SelectedObject as TaskForce;
			if (tf == null) return;
			txtTFTotalCost.Text = "Total Cost: " + tf.TotalCost();
		}

		/*
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
		 */

	}
}
