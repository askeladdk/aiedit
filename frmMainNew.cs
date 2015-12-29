using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Linq;

namespace AIEdit
{
	using IniDictionary = Dictionary<string, OrderedDictionary>;

	public partial class frmMainNew : Form
	{
		public frmMainNew()
		{
			InitializeComponent();
		}

		private void mnuLoadRA_Click(object sender, EventArgs e)
		{
			IniDictionary config = IniParser.ParseToDictionary("config/ra2.ini");
			OrderedDictionary general = config["General"];
			string sectionHouses = general["Houses"] as string;
			string editorName = general["EditorName"] as string;

			LoadRules("rulesmd.ini", sectionHouses, editorName);
			LoadConfig(config);
			LoadAI("aimd.ini");

			cmbTFUnit.Items.Clear();
			foreach(TechnoType entry in unitTypes)
			{
				cmbTFUnit.Items.Add(entry.Name);
			}
			cmbTFUnit.SelectedIndex = 0;
			
			olvTF.Sort(olvColTFName, SortOrder.Ascending);
			olvTF.SetObjects(taskForces.Items);
		}

		private void UpdateTFUnit(int mod)
		{
			TechnoType tt = unitTypes[cmbTFUnit.SelectedIndex] as TechnoType;
			TaskForce tf = olvTF.SelectedObject as TaskForce;

			if (tf.Mod(tt, mod) != 0)
			{
				olvTFUnits.SetObjects(tf);
			}
			else
			{
				TaskForceEntry tfe = tf.SingleOrDefault(s => s.Unit == tt);
				olvTFUnits.RefreshObject(tfe);
			}

			UpdateTFCost();
		}

		private void btnTFAddUnit_Click(object sender, EventArgs e)
		{
			UpdateTFUnit(1);
		}

		private void btnTFDelUnit_Click(object sender, EventArgs e)
		{
			UpdateTFUnit(-1);
		}

		private void saveAIToolStripMenuItem_Click(object sender, EventArgs e)
		{
			WriteAI("aimd_out.ini");
		}
		
		private void DelActiveTF()
		{
			DialogResult res = MessageBox.Show("Are you sure you want to delete this Task Force?",
				"Delete Task Force", MessageBoxButtons.YesNo);

			if (res == DialogResult.Yes)
			{
				int idx = Math.Min(olvTF.SelectedIndex, olvTF.Items.Count - 1);
				TaskForce tf = olvTF.SelectedObject as TaskForce;
				taskForces.Remove(tf);
				olvTF.BeginUpdate();
				olvTF.RemoveObject(tf);
				olvTF.EndUpdate();
				olvTF.SelectedIndex = idx;
			}
		}

		private void mnuDelTF_Click(object sender, EventArgs e)
		{
			DelActiveTF();
		}

		private void mnuNewTF_Click(object sender, EventArgs e)
		{
			InputBox.InputResult res = InputBox.Show("New Task Force", "Enter name:");

			if (res.ReturnCode == DialogResult.OK)
			{
				string id = nextID();
				TaskForce tf = new TaskForce(id, res.Text, DEFAULT_GROUP);
				taskForces.Add(tf);
				olvTF.BeginUpdate();
				olvTF.AddObject(tf);
				olvTF.EndUpdate();
				olvTF.SelectedObject = tf;
			}
		}

		private void olvTF_SelectedIndexChanged(object sender, EventArgs e)
		{
			TaskForce tf = olvTF.SelectedObject as TaskForce;
			olvTFUnits.SetObjects(tf);
			UpdateTFCost();
		}

		private void olvTF_CellEditFinished(object sender, BrightIdeasSoftware.CellEditEventArgs e)
		{
			olvTF.Sort();
		}

		private void olvTFUnits_CellEditStarting(object sender, BrightIdeasSoftware.CellEditEventArgs e)
		{
			if (e.SubItemIndex == 1)
			{
				TaskForceEntry tfe = e.RowObject as TaskForceEntry;
				int idx = unitTypes.TakeWhile(s => s != tfe.Unit).Count();

				// Unit selector
				ComboBox cmb = new ComboBox();
				cmb.FlatStyle = FlatStyle.Flat;
				cmb.DropDownStyle = ComboBoxStyle.DropDownList;
				foreach (TechnoType entry in unitTypes)
				{
					cmb.Items.Add(entry.Name);
				}

				cmb.Bounds = e.CellBounds;
				cmb.SelectedIndex = idx;
				e.Control = cmb;
			}
		}

		private void olvTFUnits_CellEditFinishing(object sender, BrightIdeasSoftware.CellEditEventArgs e)
		{
			TaskForce tf = olvTF.SelectedObject as TaskForce;
			if (e.SubItemIndex == 1)
			{
				ComboBox cmb = e.Control as ComboBox;
				TaskForceEntry tfe = e.RowObject as TaskForceEntry;
				TechnoType unit = unitTypes[cmb.SelectedIndex];

				TaskForceEntry exists = tf.SingleOrDefault(s => s.Unit == unit);

				if (exists != null && exists != tfe)
				{
					tf.Remove(tfe.Unit);
					exists.Count = exists.Count + tfe.Count;
					olvTFUnits.SetObjects(tf);
				}
				else
				{
					tfe.Unit = unitTypes[cmb.SelectedIndex];
					olvTFUnits.RefreshItem(e.ListViewItem);
				}
			}
		}

		private void olvTFUnits_CellEditFinished(object sender, BrightIdeasSoftware.CellEditEventArgs e)
		{
			TaskForce tf = olvTF.SelectedObject as TaskForce;
			if (e.SubItemIndex == 0)
			{
				uint val = (uint)e.NewValue;
				TaskForceEntry tfe = e.RowObject as TaskForceEntry;

				if (val == 0)
				{
					tf.Remove(tfe.Unit);
					olvTFUnits.SetObjects(tf);
				}
			}

			UpdateTFCost();
		}

		private void olvTF_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Delete) DelActiveTF();
		}

		private void olvTFUnits_KeyDown(object sender, KeyEventArgs e)
		{
			if(e.KeyCode == Keys.Delete)
			{
				TaskForce tf = olvTF.SelectedObject as TaskForce;
				TaskForceEntry tfe = olvTFUnits.SelectedObject as TaskForceEntry;
				tf.Remove(tfe.Unit);
				olvTFUnits.SetObjects(tf);
			}
		}

	}
}
