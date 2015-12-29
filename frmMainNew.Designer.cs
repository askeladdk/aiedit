namespace AIEdit
{
	partial class frmMainNew
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMainNew));
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.loadRulesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuLoadRA = new System.Windows.Forms.ToolStripMenuItem();
			this.tiberianSunToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.loadAIToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
			this.saveAIToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
			this.clearAllDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
			this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPageTF = new System.Windows.Forms.TabPage();
			this.olvTF = new BrightIdeasSoftware.ObjectListView();
			this.olvTFName = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
			this.olvTFID = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
			this.olvTFUses = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
			this.mnuCtxTF = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.mnuNewTF = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuDelTF = new System.Windows.Forms.ToolStripMenuItem();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.olvTFUnits = new BrightIdeasSoftware.ObjectListView();
			this.olvColTFUnitCount = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
			this.olvColTFName = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
			this.olvColTFCost = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
			this.btnTFDelUnit = new System.Windows.Forms.Button();
			this.btnTFAddUnit = new System.Windows.Forms.Button();
			this.cmbTFUnit = new System.Windows.Forms.ComboBox();
			this.txtTFTotalCost = new System.Windows.Forms.TextBox();
			this.cmbTFGroup = new System.Windows.Forms.ComboBox();
			this.tabPageST = new System.Windows.Forms.TabPage();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.cmbParam = new System.Windows.Forms.ComboBox();
			this.cmbAction = new System.Windows.Forms.ComboBox();
			this.lstActions = new System.Windows.Forms.ListView();
			this.colActionNr = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.colActionDesc = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.colActionParam = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.lstST = new System.Windows.Forms.ListView();
			this.colSTName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.colSTID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.colSTUses = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.tabPageTT = new System.Windows.Forms.TabPage();
			this.tabPageTr = new System.Windows.Forms.TabPage();
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
			this.menuStrip1.SuspendLayout();
			this.tabControl1.SuspendLayout();
			this.tabPageTF.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.olvTF)).BeginInit();
			this.mnuCtxTF.SuspendLayout();
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.olvTFUnits)).BeginInit();
			this.tabPageST.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(780, 24);
			this.menuStrip1.TabIndex = 0;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// fileToolStripMenuItem
			// 
			this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadRulesToolStripMenuItem,
            this.loadAIToolStripMenuItem,
            this.toolStripMenuItem1,
            this.saveAIToolStripMenuItem,
            this.toolStripMenuItem2,
            this.clearAllDataToolStripMenuItem,
            this.toolStripMenuItem3,
            this.exitToolStripMenuItem});
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
			this.fileToolStripMenuItem.Text = "&File";
			// 
			// loadRulesToolStripMenuItem
			// 
			this.loadRulesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuLoadRA,
            this.tiberianSunToolStripMenuItem});
			this.loadRulesToolStripMenuItem.Name = "loadRulesToolStripMenuItem";
			this.loadRulesToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
			this.loadRulesToolStripMenuItem.Text = "Load &Rules";
			// 
			// mnuLoadRA
			// 
			this.mnuLoadRA.Name = "mnuLoadRA";
			this.mnuLoadRA.Size = new System.Drawing.Size(140, 22);
			this.mnuLoadRA.Text = "Red Alert 2";
			this.mnuLoadRA.Click += new System.EventHandler(this.mnuLoadRA_Click);
			// 
			// tiberianSunToolStripMenuItem
			// 
			this.tiberianSunToolStripMenuItem.Name = "tiberianSunToolStripMenuItem";
			this.tiberianSunToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
			this.tiberianSunToolStripMenuItem.Text = "Tiberian Sun";
			// 
			// loadAIToolStripMenuItem
			// 
			this.loadAIToolStripMenuItem.Name = "loadAIToolStripMenuItem";
			this.loadAIToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
			this.loadAIToolStripMenuItem.Text = "Load &AI";
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(142, 6);
			// 
			// saveAIToolStripMenuItem
			// 
			this.saveAIToolStripMenuItem.Name = "saveAIToolStripMenuItem";
			this.saveAIToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
			this.saveAIToolStripMenuItem.Text = "&Save AI";
			this.saveAIToolStripMenuItem.Click += new System.EventHandler(this.saveAIToolStripMenuItem_Click);
			// 
			// toolStripMenuItem2
			// 
			this.toolStripMenuItem2.Name = "toolStripMenuItem2";
			this.toolStripMenuItem2.Size = new System.Drawing.Size(142, 6);
			// 
			// clearAllDataToolStripMenuItem
			// 
			this.clearAllDataToolStripMenuItem.Name = "clearAllDataToolStripMenuItem";
			this.clearAllDataToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
			this.clearAllDataToolStripMenuItem.Text = "&Clear All Data";
			// 
			// toolStripMenuItem3
			// 
			this.toolStripMenuItem3.Name = "toolStripMenuItem3";
			this.toolStripMenuItem3.Size = new System.Drawing.Size(142, 6);
			// 
			// exitToolStripMenuItem
			// 
			this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
			this.exitToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
			this.exitToolStripMenuItem.Text = "E&xit";
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tabPageTF);
			this.tabControl1.Controls.Add(this.tabPageST);
			this.tabControl1.Controls.Add(this.tabPageTT);
			this.tabControl1.Controls.Add(this.tabPageTr);
			this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl1.Location = new System.Drawing.Point(0, 24);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(780, 534);
			this.tabControl1.TabIndex = 1;
			this.tabControl1.TabStop = false;
			// 
			// tabPageTF
			// 
			this.tabPageTF.Controls.Add(this.olvTF);
			this.tabPageTF.Controls.Add(this.groupBox1);
			this.tabPageTF.Location = new System.Drawing.Point(4, 22);
			this.tabPageTF.Name = "tabPageTF";
			this.tabPageTF.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageTF.Size = new System.Drawing.Size(772, 508);
			this.tabPageTF.TabIndex = 0;
			this.tabPageTF.Text = "Task Forces";
			this.tabPageTF.UseVisualStyleBackColor = true;
			// 
			// olvTF
			// 
			this.olvTF.AllColumns.Add(this.olvTFName);
			this.olvTF.AllColumns.Add(this.olvTFID);
			this.olvTF.AllColumns.Add(this.olvTFUses);
			this.olvTF.CellEditActivation = BrightIdeasSoftware.ObjectListView.CellEditActivateMode.DoubleClick;
			this.olvTF.CellEditUseWholeCell = false;
			this.olvTF.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvTFName,
            this.olvTFID,
            this.olvTFUses});
			this.olvTF.ContextMenuStrip = this.mnuCtxTF;
			this.olvTF.Cursor = System.Windows.Forms.Cursors.Default;
			this.olvTF.FullRowSelect = true;
			this.olvTF.HideSelection = false;
			this.olvTF.HighlightBackgroundColor = System.Drawing.Color.Empty;
			this.olvTF.HighlightForegroundColor = System.Drawing.Color.Empty;
			this.olvTF.Location = new System.Drawing.Point(6, 6);
			this.olvTF.MultiSelect = false;
			this.olvTF.Name = "olvTF";
			this.olvTF.ShowGroups = false;
			this.olvTF.Size = new System.Drawing.Size(320, 494);
			this.olvTF.Sorting = System.Windows.Forms.SortOrder.Ascending;
			this.olvTF.TabIndex = 6;
			this.olvTF.UseCompatibleStateImageBehavior = false;
			this.olvTF.View = System.Windows.Forms.View.Details;
			this.olvTF.CellEditFinished += new BrightIdeasSoftware.CellEditEventHandler(this.olvTF_CellEditFinished);
			this.olvTF.SelectedIndexChanged += new System.EventHandler(this.olvTF_SelectedIndexChanged);
			this.olvTF.KeyDown += new System.Windows.Forms.KeyEventHandler(this.olvTF_KeyDown);
			// 
			// olvTFName
			// 
			this.olvTFName.AspectName = "Name";
			this.olvTFName.Hideable = false;
			this.olvTFName.Text = "Name";
			this.olvTFName.Width = 250;
			// 
			// olvTFID
			// 
			this.olvTFID.AspectName = "ID";
			this.olvTFID.Hideable = false;
			this.olvTFID.IsEditable = false;
			this.olvTFID.Text = "ID";
			this.olvTFID.Width = 0;
			// 
			// olvTFUses
			// 
			this.olvTFUses.AspectName = "Uses";
			this.olvTFUses.Hideable = false;
			this.olvTFUses.IsEditable = false;
			this.olvTFUses.Text = "Uses";
			this.olvTFUses.Width = 40;
			// 
			// mnuCtxTF
			// 
			this.mnuCtxTF.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuNewTF,
            this.mnuDelTF});
			this.mnuCtxTF.Name = "mnuCtxTF";
			this.mnuCtxTF.Size = new System.Drawing.Size(108, 48);
			// 
			// mnuNewTF
			// 
			this.mnuNewTF.Name = "mnuNewTF";
			this.mnuNewTF.Size = new System.Drawing.Size(107, 22);
			this.mnuNewTF.Text = "New";
			this.mnuNewTF.Click += new System.EventHandler(this.mnuNewTF_Click);
			// 
			// mnuDelTF
			// 
			this.mnuDelTF.Name = "mnuDelTF";
			this.mnuDelTF.Size = new System.Drawing.Size(107, 22);
			this.mnuDelTF.Text = "Delete";
			this.mnuDelTF.Click += new System.EventHandler(this.mnuDelTF_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.olvTFUnits);
			this.groupBox1.Controls.Add(this.btnTFDelUnit);
			this.groupBox1.Controls.Add(this.btnTFAddUnit);
			this.groupBox1.Controls.Add(this.cmbTFUnit);
			this.groupBox1.Controls.Add(this.txtTFTotalCost);
			this.groupBox1.Controls.Add(this.cmbTFGroup);
			this.groupBox1.Location = new System.Drawing.Point(333, 6);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(432, 494);
			this.groupBox1.TabIndex = 1;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Units";
			// 
			// olvTFUnits
			// 
			this.olvTFUnits.AllColumns.Add(this.olvColTFUnitCount);
			this.olvTFUnits.AllColumns.Add(this.olvColTFName);
			this.olvTFUnits.AllColumns.Add(this.olvColTFCost);
			this.olvTFUnits.CellEditActivation = BrightIdeasSoftware.ObjectListView.CellEditActivateMode.SingleClickAlways;
			this.olvTFUnits.CellEditUseWholeCell = false;
			this.olvTFUnits.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColTFUnitCount,
            this.olvColTFName,
            this.olvColTFCost});
			this.olvTFUnits.Cursor = System.Windows.Forms.Cursors.Default;
			this.olvTFUnits.FullRowSelect = true;
			this.olvTFUnits.HideSelection = false;
			this.olvTFUnits.HighlightBackgroundColor = System.Drawing.Color.Empty;
			this.olvTFUnits.HighlightForegroundColor = System.Drawing.Color.Empty;
			this.olvTFUnits.Location = new System.Drawing.Point(6, 19);
			this.olvTFUnits.MultiSelect = false;
			this.olvTFUnits.Name = "olvTFUnits";
			this.olvTFUnits.ShowGroups = false;
			this.olvTFUnits.Size = new System.Drawing.Size(418, 439);
			this.olvTFUnits.TabIndex = 6;
			this.olvTFUnits.UseCompatibleStateImageBehavior = false;
			this.olvTFUnits.View = System.Windows.Forms.View.Details;
			this.olvTFUnits.CellEditFinished += new BrightIdeasSoftware.CellEditEventHandler(this.olvTFUnits_CellEditFinished);
			this.olvTFUnits.CellEditFinishing += new BrightIdeasSoftware.CellEditEventHandler(this.olvTFUnits_CellEditFinishing);
			this.olvTFUnits.CellEditStarting += new BrightIdeasSoftware.CellEditEventHandler(this.olvTFUnits_CellEditStarting);
			this.olvTFUnits.KeyDown += new System.Windows.Forms.KeyEventHandler(this.olvTFUnits_KeyDown);
			// 
			// olvColTFUnitCount
			// 
			this.olvColTFUnitCount.AspectName = "Count";
			this.olvColTFUnitCount.Hideable = false;
			this.olvColTFUnitCount.Sortable = false;
			this.olvColTFUnitCount.Text = "Count";
			// 
			// olvColTFName
			// 
			this.olvColTFName.AspectName = "Name";
			this.olvColTFName.Hideable = false;
			this.olvColTFName.Sortable = false;
			this.olvColTFName.Text = "Unit";
			this.olvColTFName.Width = 280;
			// 
			// olvColTFCost
			// 
			this.olvColTFCost.AspectName = "Cost";
			this.olvColTFCost.Hideable = false;
			this.olvColTFCost.IsEditable = false;
			this.olvColTFCost.Searchable = false;
			this.olvColTFCost.Sortable = false;
			this.olvColTFCost.Text = "Cost";
			// 
			// btnTFDelUnit
			// 
			this.btnTFDelUnit.Location = new System.Drawing.Point(270, 464);
			this.btnTFDelUnit.Name = "btnTFDelUnit";
			this.btnTFDelUnit.Size = new System.Drawing.Size(48, 24);
			this.btnTFDelUnit.TabIndex = 5;
			this.btnTFDelUnit.Text = "-";
			this.btnTFDelUnit.UseVisualStyleBackColor = true;
			this.btnTFDelUnit.Click += new System.EventHandler(this.btnTFDelUnit_Click);
			// 
			// btnTFAddUnit
			// 
			this.btnTFAddUnit.Location = new System.Drawing.Point(216, 464);
			this.btnTFAddUnit.Name = "btnTFAddUnit";
			this.btnTFAddUnit.Size = new System.Drawing.Size(48, 24);
			this.btnTFAddUnit.TabIndex = 4;
			this.btnTFAddUnit.Text = "+";
			this.btnTFAddUnit.UseVisualStyleBackColor = true;
			this.btnTFAddUnit.Click += new System.EventHandler(this.btnTFAddUnit_Click);
			// 
			// cmbTFUnit
			// 
			this.cmbTFUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbTFUnit.FormattingEnabled = true;
			this.cmbTFUnit.Location = new System.Drawing.Point(6, 466);
			this.cmbTFUnit.Name = "cmbTFUnit";
			this.cmbTFUnit.Size = new System.Drawing.Size(204, 21);
			this.cmbTFUnit.TabIndex = 3;
			// 
			// txtTFTotalCost
			// 
			this.txtTFTotalCost.Location = new System.Drawing.Point(324, 467);
			this.txtTFTotalCost.Name = "txtTFTotalCost";
			this.txtTFTotalCost.ReadOnly = true;
			this.txtTFTotalCost.Size = new System.Drawing.Size(100, 20);
			this.txtTFTotalCost.TabIndex = 2;
			this.txtTFTotalCost.TabStop = false;
			this.txtTFTotalCost.Text = "Total Cost: 0";
			// 
			// cmbTFGroup
			// 
			this.cmbTFGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbTFGroup.FormattingEnabled = true;
			this.cmbTFGroup.Location = new System.Drawing.Point(6, 464);
			this.cmbTFGroup.Name = "cmbTFGroup";
			this.cmbTFGroup.Size = new System.Drawing.Size(40, 21);
			this.cmbTFGroup.TabIndex = 1;
			this.cmbTFGroup.Visible = false;
			// 
			// tabPageST
			// 
			this.tabPageST.Controls.Add(this.groupBox2);
			this.tabPageST.Controls.Add(this.lstST);
			this.tabPageST.Location = new System.Drawing.Point(4, 22);
			this.tabPageST.Name = "tabPageST";
			this.tabPageST.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageST.Size = new System.Drawing.Size(772, 508);
			this.tabPageST.TabIndex = 1;
			this.tabPageST.Text = "Script Types";
			this.tabPageST.UseVisualStyleBackColor = true;
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.cmbParam);
			this.groupBox2.Controls.Add(this.cmbAction);
			this.groupBox2.Controls.Add(this.lstActions);
			this.groupBox2.Location = new System.Drawing.Point(333, 6);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(432, 494);
			this.groupBox2.TabIndex = 1;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Actions";
			// 
			// cmbParam
			// 
			this.cmbParam.FormattingEnabled = true;
			this.cmbParam.Location = new System.Drawing.Point(6, 310);
			this.cmbParam.Name = "cmbParam";
			this.cmbParam.Size = new System.Drawing.Size(420, 21);
			this.cmbParam.TabIndex = 2;
			// 
			// cmbAction
			// 
			this.cmbAction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbAction.FormattingEnabled = true;
			this.cmbAction.Location = new System.Drawing.Point(6, 283);
			this.cmbAction.Name = "cmbAction";
			this.cmbAction.Size = new System.Drawing.Size(420, 21);
			this.cmbAction.TabIndex = 1;
			// 
			// lstActions
			// 
			this.lstActions.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colActionNr,
            this.colActionDesc,
            this.colActionParam});
			this.lstActions.FullRowSelect = true;
			this.lstActions.HideSelection = false;
			this.lstActions.Location = new System.Drawing.Point(6, 19);
			this.lstActions.MultiSelect = false;
			this.lstActions.Name = "lstActions";
			this.lstActions.Size = new System.Drawing.Size(420, 258);
			this.lstActions.TabIndex = 0;
			this.lstActions.UseCompatibleStateImageBehavior = false;
			this.lstActions.View = System.Windows.Forms.View.Details;
			// 
			// colActionNr
			// 
			this.colActionNr.Text = "#";
			this.colActionNr.Width = 25;
			// 
			// colActionDesc
			// 
			this.colActionDesc.Text = "Action";
			this.colActionDesc.Width = 200;
			// 
			// colActionParam
			// 
			this.colActionParam.Text = "Parameter";
			this.colActionParam.Width = 180;
			// 
			// lstST
			// 
			this.lstST.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colSTName,
            this.colSTID,
            this.colSTUses});
			this.lstST.FullRowSelect = true;
			this.lstST.HideSelection = false;
			this.lstST.LabelEdit = true;
			this.lstST.Location = new System.Drawing.Point(6, 6);
			this.lstST.MultiSelect = false;
			this.lstST.Name = "lstST";
			this.lstST.Size = new System.Drawing.Size(320, 494);
			this.lstST.TabIndex = 0;
			this.lstST.UseCompatibleStateImageBehavior = false;
			this.lstST.View = System.Windows.Forms.View.Details;
			// 
			// colSTName
			// 
			this.colSTName.Text = "Name";
			this.colSTName.Width = 250;
			// 
			// colSTID
			// 
			this.colSTID.Text = "ID";
			this.colSTID.Width = 0;
			// 
			// colSTUses
			// 
			this.colSTUses.Text = "Uses";
			this.colSTUses.Width = 45;
			// 
			// tabPageTT
			// 
			this.tabPageTT.Location = new System.Drawing.Point(4, 22);
			this.tabPageTT.Name = "tabPageTT";
			this.tabPageTT.Size = new System.Drawing.Size(772, 508);
			this.tabPageTT.TabIndex = 2;
			this.tabPageTT.Text = "Team Types";
			this.tabPageTT.UseVisualStyleBackColor = true;
			// 
			// tabPageTr
			// 
			this.tabPageTr.Location = new System.Drawing.Point(4, 22);
			this.tabPageTr.Name = "tabPageTr";
			this.tabPageTr.Size = new System.Drawing.Size(772, 508);
			this.tabPageTr.TabIndex = 3;
			this.tabPageTr.Text = "Trigger Types";
			this.tabPageTr.UseVisualStyleBackColor = true;
			// 
			// openFileDialog1
			// 
			this.openFileDialog1.FileName = "openFileDialog1";
			// 
			// frmMainNew
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(780, 558);
			this.Controls.Add(this.tabControl1);
			this.Controls.Add(this.menuStrip1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MainMenuStrip = this.menuStrip1;
			this.MaximizeBox = false;
			this.Name = "frmMainNew";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "AI Editor";
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.tabControl1.ResumeLayout(false);
			this.tabPageTF.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.olvTF)).EndInit();
			this.mnuCtxTF.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.olvTFUnits)).EndInit();
			this.tabPageST.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPageTF;
		private System.Windows.Forms.TabPage tabPageST;
		private System.Windows.Forms.ToolStripMenuItem loadRulesToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem loadAIToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem saveAIToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
		private System.Windows.Forms.ToolStripMenuItem clearAllDataToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
		private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem mnuLoadRA;
		private System.Windows.Forms.ToolStripMenuItem tiberianSunToolStripMenuItem;
		private System.Windows.Forms.OpenFileDialog openFileDialog1;
		private System.Windows.Forms.SaveFileDialog saveFileDialog1;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.TabPage tabPageTT;
		private System.Windows.Forms.TabPage tabPageTr;
		private System.Windows.Forms.ComboBox cmbTFGroup;
		private System.Windows.Forms.TextBox txtTFTotalCost;
		private System.Windows.Forms.ComboBox cmbTFUnit;
		private System.Windows.Forms.Button btnTFAddUnit;
		private System.Windows.Forms.Button btnTFDelUnit;
		private System.Windows.Forms.ContextMenuStrip mnuCtxTF;
		private System.Windows.Forms.ToolStripMenuItem mnuNewTF;
		private System.Windows.Forms.ToolStripMenuItem mnuDelTF;
		private System.Windows.Forms.ListView lstST;
		private System.Windows.Forms.ColumnHeader colSTName;
		private System.Windows.Forms.ColumnHeader colSTID;
		private System.Windows.Forms.ColumnHeader colSTUses;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.ListView lstActions;
		private System.Windows.Forms.ColumnHeader colActionNr;
		private System.Windows.Forms.ColumnHeader colActionDesc;
		private System.Windows.Forms.ColumnHeader colActionParam;
		private System.Windows.Forms.ComboBox cmbAction;
		private System.Windows.Forms.ComboBox cmbParam;
		private BrightIdeasSoftware.ObjectListView olvTF;
		private BrightIdeasSoftware.OLVColumn olvTFName;
		private BrightIdeasSoftware.OLVColumn olvTFID;
		private BrightIdeasSoftware.OLVColumn olvTFUses;
		private BrightIdeasSoftware.ObjectListView olvTFUnits;
		private BrightIdeasSoftware.OLVColumn olvColTFUnitCount;
		private BrightIdeasSoftware.OLVColumn olvColTFName;
		private BrightIdeasSoftware.OLVColumn olvColTFCost;
	}
}