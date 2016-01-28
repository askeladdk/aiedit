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

		public TaskForce SelectedTaskForce()
		{
			return olvTF.SelectedObject as TaskForce;
		}

		public ScriptType SelectedScriptType()
		{
			return olvST.SelectedObject as ScriptType;
		}

		public TeamType SelectedTeamType()
		{
			return olvTT.SelectedObject as TeamType;
		}

		public TriggerType SelectedTriggerType()
		{
			return olvTr.SelectedObject as TriggerType;
		}

		private void mnuLoadRA_Click(object sender, EventArgs e)
		{
			
		}

		private void UpdateTFUnit(int mod)
		{
			TechnoType tt = cmbTFUnit.SelectedItem as TechnoType;
			TaskForce tf = SelectedTaskForce();

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
			saveFileDialog1.FileName = "*.ini";
			saveFileDialog1.Title = "Save AI";
			if (saveFileDialog1.ShowDialog() != DialogResult.OK) return;
			WriteAI(saveFileDialog1.FileName);
			this.Text = "C&C AI Editor - " + saveFileDialog1.FileName;
		}

		private void STActionMoveUp()
		{
			ScriptType st = SelectedScriptType();
			int idx = st.MoveUp(olvSTActions.SelectedIndex);
			olvSTActions.SetObjects(st.Actions);
			olvSTActions.SelectedIndex = idx;
		}

		private void STActionMoveDown()
		{
			ScriptType st = SelectedScriptType();
			ScriptAction a = olvSTActions.SelectedObject as ScriptAction;
			int idx = st.MoveDown(olvSTActions.SelectedIndex);
			olvSTActions.SetObjects(st.Actions);
			olvSTActions.SelectedIndex = idx;
		}

		private void STActionNew()
		{
			ScriptAction sa = new ScriptAction(actionTypes[0], 0);
			ScriptType st = SelectedScriptType();
			int idx = olvSTActions.SelectedIndex + 1;
			st.Insert(sa, idx);
			olvSTActions.SetObjects(st.Actions);
			olvSTActions.SelectedIndex = idx;
		}

		private void STActionDelete()
		{
			ScriptAction sa = olvSTActions.SelectedObject as ScriptAction;
			ScriptType st = SelectedScriptType();
			int idx = olvSTActions.SelectedIndex;
			st.Remove(sa);
			olvSTActions.SetObjects(st.Actions);
			idx = Math.Min(idx, st.Count - 1);
			olvSTActions.SelectedIndex = idx;
		}


		/**
		 * Control Delegates.
		 **/


		private void mnuDelTF_Click(object sender, EventArgs e)
		{
			TaskForce tf = SelectedTaskForce();

			if (tf.Uses > 0)
			{
				MessageBox.Show("Cannot delete Task Force while it is in use.", "Delete Task Force");
				return;
			}

			DialogResult res = MessageBox.Show("Are you sure you want to delete this Task Force?",
				"Delete Task Force", MessageBoxButtons.YesNo);

			if (res == DialogResult.Yes)
			{
				int idx = Math.Min(olvTF.SelectedIndex, olvTF.Items.Count - 1);
				taskForces.Remove(tf);
				olvTF.BeginUpdate();
				olvTF.RemoveObject(tf);
				olvTF.EndUpdate();
				olvTF.SelectedIndex = idx;
			}
		}

		private void mnuNewTF_Click(object sender, EventArgs e)
		{
			InputBox.InputResult res = InputBox.Show("New Task Force", "Enter name:");

			if (res.ReturnCode == DialogResult.OK)
			{
				string id = nextID();
				TaskForce tf = new TaskForce(id, res.Text, groupTypes[0]);
				taskForces.Add(tf);
				olvTF.BeginUpdate();
				olvTF.AddObject(tf);
				olvTF.EndUpdate();
				olvTF.SelectedObject = tf;
				olvTF.EnsureVisible();
			}
		}

		private void olvTF_SelectedIndexChanged(object sender, EventArgs e)
		{
			TaskForce tf = SelectedTaskForce();
			if (tf == null) return;
			olvTFUnits.SetObjects(tf);
			olvTFUnits.SelectedIndex = 0;
			cmbTFGroup.SelectedItem = tf.Group;
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
				foreach (TechnoType entry in unitTypes) cmb.Items.Add(entry);
				cmb.Bounds = e.CellBounds;
				cmb.SelectedIndex = idx;
				e.Control = cmb;
			}
		}

		private void olvTFUnits_CellEditFinishing(object sender, BrightIdeasSoftware.CellEditEventArgs e)
		{
			if(!e.Cancel && e.SubItemIndex == 1)
			{
				ComboBox cmb = e.Control as ComboBox;
				TaskForce tf = SelectedTaskForce();
				TaskForceEntry tfe = e.RowObject as TaskForceEntry;
				TechnoType unit = cmb.SelectedItem as TechnoType;

				TaskForceEntry exists = tf.SingleOrDefault(s => s.Unit == unit);

				if (exists != null && exists != tfe)
				{
					tf.Remove(tfe.Unit);
					exists.Count = exists.Count + tfe.Count;
					olvTFUnits.SetObjects(tf);
				}
				else
				{
					tfe.Unit = unit;
					olvTFUnits.RefreshItem(e.ListViewItem);
				}
			}
		}

		private void olvTFUnits_CellEditFinished(object sender, BrightIdeasSoftware.CellEditEventArgs e)
		{
			TaskForce tf = SelectedTaskForce();
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
			switch(e.KeyCode)
			{
				case Keys.Insert:
					mnuNewTF_Click(sender, e);
					break;
				case Keys.Delete:
					mnuDelTF_Click(sender, e);
					break;
			}
		}

		private void olvTFUnits_KeyDown(object sender, KeyEventArgs e)
		{
			if(e.KeyCode == Keys.Delete)
			{
				TaskForce tf = SelectedTaskForce();
				TaskForceEntry tfe = olvTFUnits.SelectedObject as TaskForceEntry;
				tf.Remove(tfe.Unit);
				olvTFUnits.SetObjects(tf);
				e.Handled = true;
			}
		}

		private void cmbTFGroup_SelectionChangeCommitted(object sender, EventArgs e)
		{
			TaskForce tf = SelectedTaskForce();
			tf.Group = cmbTFGroup.SelectedItem as AITypeListEntry;
		}

		private void olvST_SelectedIndexChanged(object sender, EventArgs e)
		{
			ScriptType st = SelectedScriptType();
			if (st == null) return;
			olvSTActions.SetObjects(st);
			olvSTActions.SelectedIndex = 0;
		}

		private void olvSTActions_FormatRow(object sender, BrightIdeasSoftware.FormatRowEventArgs e)
		{
			// numbers every row
			e.Item.SubItems[0].Text = e.DisplayIndex.ToString();
		}

		private void olvST_CellEditFinished(object sender, BrightIdeasSoftware.CellEditEventArgs e)
		{
			olvST.Sort();
		}


		private void UpdateSTActionDescription()
		{
			ScriptAction action = olvSTActions.SelectedObject as ScriptAction;
			if (action != null) txtSTActionDesc.Text = action.Action.Description;
		}

		private void olvSTActions_CellEditStarting(object sender, BrightIdeasSoftware.CellEditEventArgs e)
		{
			ScriptAction action = e.RowObject as ScriptAction;

			// action
			if (e.SubItemIndex == 1)
			{
				uint idx = action.Action.Code;

				ComboBox cmb = new ComboBox();
				cmb.FlatStyle = FlatStyle.Flat;
				cmb.DropDownStyle = ComboBoxStyle.DropDownList;
				cmb.Sorted = true;
				foreach (IActionType entry in actionTypes) cmb.Items.Add(entry);
				cmb.SelectedItem = action.Action;
				cmb.Bounds = e.CellBounds;
				e.Control = cmb;
			}
			// parameter
			else if(e.SubItemIndex == 2)
			{
				if(action.Action.ParamType == ScriptParamType.Number)
				{
					NumericUpDown nud = new NumericUpDown();
					nud.Minimum = 0;
					nud.Value = action.Param;
					nud.Bounds = e.CellBounds;
					e.Control = nud;
				}
				else
				{
					ComboBox cmb = new ComboBox();
					cmb.FlatStyle = FlatStyle.Flat;
					cmb.DropDownStyle = ComboBoxStyle.DropDownList;
					cmb.Sorted = true;
					foreach (object obj in action.Action.List) cmb.Items.Add(obj);
					cmb.SelectedItem = action.ParamEntry;
					cmb.Bounds = e.CellBounds;
					e.Control = cmb;
				}
			}
			// offset
			else if(e.SubItemIndex == 3)
			{
				if (action.Action.ParamType != ScriptParamType.TechnoType)
				{
					e.Cancel = true;
					return;
				}

				ComboBox cmb = new ComboBox();
				cmb.FlatStyle = FlatStyle.Flat;
				cmb.DropDownStyle = ComboBoxStyle.DropDownList;
				foreach (string s in ScriptAction.OffsetDescriptions()) cmb.Items.Add(s);
				cmb.SelectedIndex = (int)action.Offset;
				cmb.Bounds = e.CellBounds;
				e.Control = cmb;
			}
		}

		private void olvSTActions_CellEditFinishing(object sender, BrightIdeasSoftware.CellEditEventArgs e)
		{
			if (e.Cancel) return;

			ScriptAction action = e.RowObject as ScriptAction;

			// action
			if (e.SubItemIndex == 1)
			{
				ComboBox cmb = e.Control as ComboBox;
				e.NewValue = cmb.SelectedItem;
			}
			// parameter
			else if(e.SubItemIndex == 2)
			{
				if(action.Action.ParamType == ScriptParamType.Number)
				{
					NumericUpDown nud = e.Control as NumericUpDown;
					action.Param = (uint)nud.Value;
				}
				else if(action.Action.ParamType == ScriptParamType.AIObject)
				{
					//action.Param = 
				}
				else
				{
					ComboBox cmb = e.Control as ComboBox;
					action.Param = (cmb.SelectedItem as IParamListEntry).ParamListIndex;
				}

				e.Cancel = true;
				olvSTActions.RefreshItem(e.ListViewItem);
			}
			// offset
			else if(e.SubItemIndex == 3)
			{
				ComboBox cmb = e.Control as ComboBox;
				action.Offset = (uint)cmb.SelectedIndex;
				e.Cancel = true;
				olvSTActions.RefreshItem(e.ListViewItem);
			}
		}

		private void olvSTActions_SelectedIndexChanged(object sender, EventArgs e)
		{
			UpdateSTActionDescription();
		}


		private void olvSTActions_CellEditFinished(object sender, BrightIdeasSoftware.CellEditEventArgs e)
		{
			UpdateSTActionDescription();
		}

		private void mnuNewST_Click(object sender, EventArgs e)
		{
			InputBox.InputResult res = InputBox.Show("New Script Type", "Enter name:");

			if (res.ReturnCode == DialogResult.OK)
			{
				string id = nextID();
				ScriptType st = new ScriptType(id, res.Text);
				scriptTypes.Add(st);
				olvST.BeginUpdate();
				olvST.AddObject(st);
				olvST.EndUpdate();
				olvST.SelectedObject = st;
				olvST.EnsureVisible();
			}
		}

		private void mnuDelST_Click(object sender, EventArgs e)
		{
			ScriptType st = SelectedScriptType();

			if (st.Uses > 0)
			{
				MessageBox.Show("Cannot delete Script Type while it is in use.", "Delete Script Type");
				return;
			}

			DialogResult res = MessageBox.Show("Are you sure you want to delete this Script Type?",
				"Delete Script Type", MessageBoxButtons.YesNo);

			if (res == DialogResult.Yes)
			{
				int idx = Math.Min(olvST.SelectedIndex, olvST.Items.Count - 1);
				scriptTypes.Remove(st);
				olvST.BeginUpdate();
				olvST.RemoveObject(st);
				olvST.EndUpdate();
				olvST.SelectedIndex = idx;
			}
		}

		private void olvTFUnits_SelectedIndexChanged(object sender, EventArgs e)
		{
			TaskForceEntry tfe = olvTFUnits.SelectedObject as TaskForceEntry;
			if(tfe != null) cmbTFUnit.SelectedItem = tfe.Unit;
		}

		private void mnuSTActionUp_Click(object sender, EventArgs e)
		{
			STActionMoveUp();
		}
		

		private void mnuSTActionDown_Click(object sender, EventArgs e)
		{
			STActionMoveDown();
		}

		private void mnuSTActionNew_Click(object sender, EventArgs e)
		{
			STActionNew();
		}


		private void mnuSTActionDelete_Click(object sender, EventArgs e)
		{
			STActionDelete();
		}

		private void olvSTActions_KeyDown(object sender, KeyEventArgs e)
		{
			switch(e.KeyCode)
			{
				case Keys.PageUp:
					STActionMoveUp();
					e.Handled = true;
					break;
				case Keys.PageDown:
					STActionMoveDown();
					e.Handled = true;
					break;
				case Keys.Insert:
					STActionNew();
					e.Handled = true;
					break;
				case Keys.Delete:
					STActionDelete();
					e.Handled = true;
					break;
			}
		}

		private void olvTT_SelectedIndexChanged(object sender, EventArgs e)
		{
			TeamType tt = SelectedTeamType();
			olvTTSettings.PrimarySortColumn = olvColTTSort;
			olvTTSettings.PrimarySortOrder = SortOrder.Ascending;
			olvTTSettings.SecondarySortColumn = olvColTTName;
			olvTTSettings.SecondarySortOrder = SortOrder.Ascending;
			olvTTSettings.Sort();
			olvTTSettings.SetObjects(tt);
			olvTTSettings.SelectedIndex = 0;
		}

		private void olvTTSettings_FormatRow(object sender, BrightIdeasSoftware.FormatRowEventArgs e)
		{
			//e.Item.SubItems[0].Text = (e.Model as TeamTypeEntry).Option.Name;
		}

		private void olvTTSettings_CellEditStarting(object sender, BrightIdeasSoftware.CellEditEventArgs e)
		{
			int idx = olvTTSettings.SelectedIndex;
			TeamTypeOption option = (e.RowObject as TeamTypeEntry).Option;

			if(option.List != null)// (option is TeamTypeOptionAIObject) || (option is TeamTypeOptionList) )
			{
				ComboBox cmb = new ComboBox();
				cmb.FlatStyle = FlatStyle.Flat;
				cmb.DropDownStyle = ComboBoxStyle.DropDownList;
				cmb.Sorted = option is TeamTypeOptionAIObject;
				foreach(object item in option.List) cmb.Items.Add(item);
				cmb.SelectedItem = e.Value;
				cmb.Bounds = e.CellBounds;
				e.Control = cmb;
			}
		}

		private void olvTTSettings_CellEditFinishing(object sender, BrightIdeasSoftware.CellEditEventArgs e)
		{
			int idx = olvTTSettings.SelectedIndex;
			TeamTypeOption option = (e.RowObject as TeamTypeEntry).Option; //teamTypeOptions[idx];

			if(option.List != null)// ((option is TeamTypeOptionAIObject) || (option is TeamTypeOptionList))
			{
				e.NewValue = (e.Control as ComboBox).SelectedItem;
			}
		}

		private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
		{
			/*
			switch(tabControl1.SelectedIndex)
			{
				case 0:
					olvTF.RefreshObjects(taskForces.Items);
					break;
				case 1:
					olvST.RefreshObjects(scriptTypes.Items);
					break;
				case 2:
					olvTT.RefreshObjects(teamTypes.Items);
					break;
				case 3:
					olvTr.RefreshObjects(triggerTypes.Items);
					break;
			}
			 */
		}

		private void mnuTTNew_Click(object sender, EventArgs e)
		{
			InputBox.InputResult res = InputBox.Show("New Team", "Enter name:");

			if (res.ReturnCode == DialogResult.OK)
			{
				string id = nextID();
				List<TeamTypeEntry> entries = new List<TeamTypeEntry>();
				foreach (TeamTypeOption option in teamTypeOptions) entries.Add(option.DefaultValue);
				TeamType tt = new TeamType(id, res.Text, entries);
				teamTypes.Add(tt);
				olvTT.BeginUpdate();
				olvTT.AddObject(tt);
				olvTT.EndUpdate();
				olvTT.SelectedObject = tt;
				olvTT.EnsureVisible();
			}
		}

		private void mnuTTDelete_Click(object sender, EventArgs e)
		{
			TeamType tt = SelectedTeamType();

			if (tt.Uses > 0)
			{
				MessageBox.Show("Cannot delete Team while it is in use.", "Delete Team");
				return;
			}

			DialogResult res = MessageBox.Show("Are you sure you want to delete this Team?",
				"Delete Trigger", MessageBoxButtons.YesNo);

			if (res == DialogResult.Yes)
			{
				int idx = Math.Min(olvTT.SelectedIndex, olvTT.Items.Count - 1);
				teamTypes.Remove(tt);
				olvTT.BeginUpdate();
				olvTT.RemoveObject(tt);
				olvTT.EndUpdate();
				olvTT.SelectedIndex = idx;
			}
		}

		private void olvTr_SelectedIndexChanged(object sender, EventArgs e)
		{
			TriggerType tr = SelectedTriggerType();
			olvTrSettings.PrimarySortColumn = olvColTrSort;
			olvTrSettings.PrimarySortOrder = SortOrder.Ascending;
			olvTrSettings.Sort();
			olvTrSettings.SetObjects(tr);
			olvTrSettings.SelectedIndex = 0;
		}

		private void olvTrSettings_CellEditStarting(object sender, BrightIdeasSoftware.CellEditEventArgs e)
		{
			int idx = olvTrSettings.SelectedIndex;
			TriggerTypeOption option = (e.RowObject as TriggerTypeEntry).Option;

			if (option.List != null)
			{
				ComboBox cmb = new ComboBox();
				cmb.FlatStyle = FlatStyle.Flat;
				cmb.DropDownStyle = ComboBoxStyle.DropDownList;
				cmb.Sorted = option is TriggerTypeOptionAIObject;

				if (option is TriggerTypeOptionAIObject) cmb.Items.Add(noneTeam);
				foreach (object item in option.List) cmb.Items.Add(item);

				cmb.SelectedItem = e.Value;
				cmb.Bounds = e.CellBounds;
				e.Control = cmb;
			}
		}

		private void olvTrSettings_CellEditFinishing(object sender, BrightIdeasSoftware.CellEditEventArgs e)
		{
			int idx = olvTrSettings.SelectedIndex;
			TriggerTypeOption option = (e.RowObject as TriggerTypeEntry).Option;

			if (option.List != null)
			{
				e.NewValue = (e.Control as ComboBox).SelectedItem;
			}
		}

		private void mnuNewTr_Click(object sender, EventArgs e)
		{
			InputBox.InputResult res = InputBox.Show("New Trigger", "Enter name:");

			if (res.ReturnCode == DialogResult.OK)
			{
				string id = nextID();
				TriggerType tr = new TriggerType(id, res.Text, triggerTypeOptions);
				triggerTypes.Add(tr);
				olvTr.BeginUpdate();
				olvTr.AddObject(tr);
				olvTr.EndUpdate();
				olvTr.SelectedObject = tr;
				olvTr.EnsureVisible();
			}
		}

		private void mnuDeleteTr_Click(object sender, EventArgs e)
		{
			TriggerType tr = SelectedTriggerType();
			
			DialogResult res = MessageBox.Show("Are you sure you want to delete this Trigger?",
				"Delete Trigger", MessageBoxButtons.YesNo);

			if (res == DialogResult.Yes)
			{
				int idx = Math.Min(olvTr.SelectedIndex, olvTr.Items.Count - 1);
				triggerTypes.Remove(tr);
				olvTr.BeginUpdate();
				olvTr.RemoveObject(tr);
				olvTr.EndUpdate();
				olvTr.SelectedIndex = idx;
			}
		}

		private void olvTr_KeyDown(object sender, KeyEventArgs e)
		{
			switch(e.KeyCode)
			{
				case Keys.Insert:
					mnuNewTr_Click(sender, e);
					break;
				case Keys.Delete:
					mnuDeleteTr_Click(sender, e);
					break;
			}
		}

		string OpenFileDialog(string title, string filename)
		{
			openFileDialog1.Title = title;
			openFileDialog1.FileName = filename;

			if (openFileDialog1.ShowDialog() == DialogResult.OK)
			{
				return openFileDialog1.FileName;
			}

			return null;
		}

		void LoadAI(string rulesfile, string aifile)
		{
			logger.Clear();
			LoadData(rulesfile, aifile);

			olvTFUnits.Items.Clear();
			olvSTActions.Items.Clear();
			olvTTSettings.Items.Clear();
			olvTrSettings.Items.Clear();

			// cmb tf unit
			cmbTFUnit.Items.Clear();
			foreach (TechnoType entry in unitTypes) cmbTFUnit.Items.Add(entry);
			cmbTFUnit.SelectedIndex = 0;

			// cmb tf group
			cmbTFGroup.Items.Clear();
			foreach (AITypeListEntry gt in groupTypes) cmbTFGroup.Items.Add(gt);
			cmbTFGroup.SelectedIndex = 0;

			olvTF.Sort(olvColTFName, SortOrder.Ascending);
			olvTF.SetObjects(taskForces.Items);

			olvST.Sort(olvColSTName, SortOrder.Ascending);
			olvST.SetObjects(scriptTypes.Items);

			olvTT.Sort(olvColTTName, SortOrder.Ascending);
			olvTT.SetObjects(teamTypes.Items);

			olvTr.Sort(olvColTrName, SortOrder.Ascending);
			olvTr.SetObjects(triggerTypes.Items);

			olvLog.Items.Clear();
			foreach (string s in logger) olvLog.AddObject(s);

			if(logger.Count > 0)
			{
				MessageBox.Show("Encountered " + logger.Count + " error(s) while loading. See the Error Log tab for details.");
			}
		}

		private void mnuLoad_Click(object sender, EventArgs e)
		{
			string rulesfile = OpenFileDialog("Select Rules", "rules*.ini");
			if (rulesfile == null) return;
			string aifile = OpenFileDialog("Select AI", "ai*.ini");
			if (aifile == null) return;

			LoadAI(rulesfile, aifile);
			this.Text = "C&C AI Editor - " + aifile;
		}

		private void mnuNew_Click(object sender, EventArgs e)
		{
			string rulesfile = OpenFileDialog("Select Rules", "rules*.ini");
			if (rulesfile == null) return;

			LoadAI(rulesfile, "config/default.ini");
			this.Text = "C&C AI Editor";
		}

		private void olvST_KeyDown(object sender, KeyEventArgs e)
		{
			switch(e.KeyCode)
			{
				case Keys.Insert:
					mnuNewST_Click(sender, e);
					break;
				case Keys.Delete:
					mnuDelST_Click(sender, e);
					break;
			}
		}

		private void olvTT_KeyDown(object sender, KeyEventArgs e)
		{
			switch(e.KeyCode)
			{
				case Keys.Insert:
					mnuTTNew_Click(sender, e);
					break;
				case Keys.Delete:
					mnuTTDelete_Click(sender, e);
					break;
			}
		}


		private void mnuInfoTF_Click(object sender, EventArgs e)
		{
			TaskForce tf = SelectedTaskForce();
			var tts = FilterTeamTypes(tf);
			object tt = UseInfoBox.Show("Used by Teams", tts);
			if(tt != null)
			{
				tabControl1.SelectedIndex = 2;
				olvTT.SelectedObject = tt;
				olvTT.EnsureVisible();
			}
		}

		private void mnuInfoST_Click(object sender, EventArgs e)
		{
			ScriptType st = SelectedScriptType();
			var tts = FilterTeamTypes(st);
			object tt = UseInfoBox.Show("Used by Teams", tts);
			if (tt != null)
			{
				tabControl1.SelectedIndex = 2;
				olvTT.SelectedObject = tt;
				olvTT.EnsureVisible();
			}
		}

		private void mnuInfoTT_Click(object sender, EventArgs e)
		{
			TeamType tt = SelectedTeamType();
			var trs = from tre in triggerTypes.Items
					  where tre.HasTeamType(tt)
					  select tre;
			object tr = UseInfoBox.Show("Used by Triggers", trs);
			if (tr != null)
			{
				tabControl1.SelectedIndex = 3;
				olvTr.SelectedObject = tr;
				olvTr.EnsureVisible();
			}
		}

		private void mnuExit_Click(object sender, EventArgs e)
		{
			Application.Exit();
		}

		private void mnuAbout_Click(object sender, EventArgs e)
		{
			string text = "C&C AI Editor v" + Application.ProductVersion + "\nDeveloped by Askeladd";
			string title = "About";
			MessageBox.Show(text, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
		}

		private void olvLog_FormatRow(object sender, BrightIdeasSoftware.FormatRowEventArgs e)
		{
			e.Item.SubItems[0].Text = e.Model.ToString();
		}

		public void CopyAIObject<T>(string title, T selected, AITable<T> aitable, BrightIdeasSoftware.ObjectListView olv)
			where T:class, IAIObject
		{
			if (selected == null) return;
			InputBox.InputResult res = InputBox.Show(title, "Enter name:", selected.Name);
			if (res.ReturnCode == DialogResult.OK)
			{
				T newai = selected.Copy(nextID(), res.Text) as T;
				aitable.Add(newai);
				olv.BeginUpdate();
				olv.AddObject(newai);
				olv.EndUpdate();
				olv.SelectedObject = newai;
				olv.EnsureVisible();
			}
		}

		private void mnuCopyTF_Click(object sender, EventArgs e)
		{
			CopyAIObject("Copy Task Force", SelectedTaskForce(), taskForces, olvTF);
		}

		private void mnuCopyST_Click(object sender, EventArgs e)
		{
			CopyAIObject("Copy Script", SelectedScriptType(), scriptTypes, olvST);
		}

		private void mnuCopyTT_Click(object sender, EventArgs e)
		{
			CopyAIObject("Copy Team", SelectedTeamType(), teamTypes, olvTT);
		}

		private void mnuCopyTr_Click(object sender, EventArgs e)
		{
			CopyAIObject("Copy Trigger", SelectedTriggerType(), triggerTypes, olvTr);
		}

		public void JumpToAIObject(IAIObject obj)
		{
			if (obj is TaskForce)
			{
				olvTF.SelectedObject = obj;
				olvTF.EnsureVisible();
				tabControl1.SelectedIndex = 0;
			}
			else if (obj is ScriptType)
			{
				olvST.SelectedObject = obj;
				olvST.EnsureVisible();
				tabControl1.SelectedIndex = 1;
			}
			else if(obj is TeamType)
			{
				olvTT.SelectedObject = obj;
				olvTT.EnsureVisible();
				tabControl1.SelectedIndex = 2;
			}
		}

		private void olvTTSettings_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			TeamTypeEntry entry = olvTTSettings.SelectedObject as TeamTypeEntry;
			JumpToAIObject(entry.Value as IAIObject);
		}

		private void olvTrSettings_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			TriggerTypeEntry entry = olvTrSettings.SelectedObject as TriggerTypeEntry;
			JumpToAIObject(entry.Value as IAIObject);
		}
	}
}
