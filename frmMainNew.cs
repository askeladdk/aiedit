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
			foreach(DictionaryEntry de in unitTypes)
			{
				cmbTFUnit.Items.Add( (de.Value as TechnoType).Name );
			}
			cmbTFUnit.SelectedIndex = 0;

			RefreshTFList();
		}

		private void lstTF_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (lstTF.SelectedItems.Count == 0) return;
			TaskForce tf = lstTF.SelectedItems[0].Tag as TaskForce;
			if (ShowTF(tf))
			{
				lstTFUnits.Items[0].Selected = true;
			}
		}

		private void lstTF_AfterLabelEdit(object sender, LabelEditEventArgs e)
		{
			TaskForce tf = lstTF.Items[e.Item].Tag as TaskForce;
			tf.Name = e.Label;
			lstTF.BeginInvoke(new MethodInvoker(lstTF.Sort));
		}

		private void lstTFUnits_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (lstTFUnits.SelectedItems.Count == 0) return;
			TechnoType tt = lstTFUnits.SelectedItems[0].Tag as TechnoType;
			for(int i = 0; i < unitTypes.Count; i++)
			{
				if (unitTypes[i] == tt)
				{
					cmbTFUnit.SelectedIndex = i;
					return;
				}
			}
		}

		private void btnTFAddUnit_Click(object sender, EventArgs e)
		{
			TechnoType tt = unitTypes[cmbTFUnit.SelectedIndex] as TechnoType;
			int mod = activeTaskForce.ModUnit(tt, 1);
			int selected = SelectedTFUnitIndex();

			if (ShowTF(activeTaskForce))
			{
				if (mod > 0) selected = activeTaskForce.Units.Count - 1;
				lstTFUnits.Items[selected].Selected = true;
			}
		}

		private void btnTFDelUnit_Click(object sender, EventArgs e)
		{
			TechnoType tt = unitTypes[cmbTFUnit.SelectedIndex] as TechnoType;
			int mod = activeTaskForce.ModUnit(tt, -1);
			int selected = SelectedTFUnitIndex();

			if (ShowTF(activeTaskForce))
			{
				if(mod < 0) selected = Math.Min(selected, lstTFUnits.Items.Count - 1);
				lstTFUnits.Items[selected].Selected = true;
			}
		}

		private void saveAIToolStripMenuItem_Click(object sender, EventArgs e)
		{
			WriteAI("aimd_out.ini");
		}

		private void lstTFUnits_AfterLabelEdit(object sender, LabelEditEventArgs e)
		{
			try
			{
				int amount = int.Parse(e.Label);

				if(amount <= 0)
				{
					e.CancelEdit = true;
					return;
				}

				TechnoType tt = lstTFUnits.SelectedItems[0].Tag as TechnoType;
				int selected = SelectedTFUnitIndex();
				activeTaskForce.SetUnit(tt, amount);
				ShowTF(activeTaskForce);
				lstTFUnits.Items[selected].Selected = true;
			}
			catch(Exception)
			{
				e.CancelEdit = true;
			}
		}

		private void lstTFUnits_KeyDown(object sender, KeyEventArgs e)
		{
			if(e.KeyCode == Keys.Delete)
			{
				int selected = SelectedTFUnitIndex();
				TechnoType tt = lstTFUnits.Items[selected].Tag as TechnoType;
				activeTaskForce.DelUnit(tt);
				ShowTF(activeTaskForce);
			}
		}

		private void DelActiveTF()
		{
			DialogResult res = MessageBox.Show("Are you sure you want to delete this Task Force?",
				"Delete", MessageBoxButtons.YesNo);

			if (res == DialogResult.Yes)
			{
				int selected = lstTF.SelectedIndices[0];
				taskForces.Remove(activeTaskForce.ID);
				RefreshTFList();
				lstTF.Select();
			}
		}

		private void mnuDelTF_Click(object sender, EventArgs e)
		{
			DelActiveTF();
		}

		private void lstTF_KeyDown(object sender, KeyEventArgs e)
		{
			if(e.KeyCode == Keys.Delete)
			{
				DelActiveTF();
			}
		}

		private void mnuNewTF_Click(object sender, EventArgs e)
		{
			InputBox.InputResult res = InputBox.Show("New Task Force", "Enter name:");

			if(res.ReturnCode == DialogResult.OK)
			{
				string id = nextID();
				TaskForce tf = new TaskForce(id, res.Text, DEFAULT_GROUP);
				taskForces.Add(id, tf);
				RefreshTFList(false);

				foreach(ListViewItem itm in lstTF.Items)
				{
					if (itm.Tag == tf)
					{
						itm.Selected = true;
						lstTF.Select();
						return;
					}
				}
			}
		}
	}
}
