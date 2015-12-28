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
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.btnTFDelUnit = new System.Windows.Forms.Button();
			this.btnTFAddUnit = new System.Windows.Forms.Button();
			this.cmbTFUnit = new System.Windows.Forms.ComboBox();
			this.txtTFTotalCost = new System.Windows.Forms.TextBox();
			this.cmbTFGroup = new System.Windows.Forms.ComboBox();
			this.lstTFUnits = new System.Windows.Forms.ListView();
			this.colTFUnitCount = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.colTFUnit = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.colTFUnitCost = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.lstTF = new System.Windows.Forms.ListView();
			this.colTFName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.colTFID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.colTFUses = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.mnuCtxTF = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.mnuNewTF = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuDelTF = new System.Windows.Forms.ToolStripMenuItem();
			this.tabPageST = new System.Windows.Forms.TabPage();
			this.tabPageTT = new System.Windows.Forms.TabPage();
			this.tabPageTr = new System.Windows.Forms.TabPage();
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
			this.menuStrip1.SuspendLayout();
			this.tabControl1.SuspendLayout();
			this.tabPageTF.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.mnuCtxTF.SuspendLayout();
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
			this.loadRulesToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.loadRulesToolStripMenuItem.Text = "Load &Rules";
			// 
			// mnuLoadRA
			// 
			this.mnuLoadRA.Name = "mnuLoadRA";
			this.mnuLoadRA.Size = new System.Drawing.Size(152, 22);
			this.mnuLoadRA.Text = "Red Alert 2";
			this.mnuLoadRA.Click += new System.EventHandler(this.mnuLoadRA_Click);
			// 
			// tiberianSunToolStripMenuItem
			// 
			this.tiberianSunToolStripMenuItem.Name = "tiberianSunToolStripMenuItem";
			this.tiberianSunToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.tiberianSunToolStripMenuItem.Text = "Tiberian Sun";
			// 
			// loadAIToolStripMenuItem
			// 
			this.loadAIToolStripMenuItem.Name = "loadAIToolStripMenuItem";
			this.loadAIToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.loadAIToolStripMenuItem.Text = "Load &AI";
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(149, 6);
			// 
			// saveAIToolStripMenuItem
			// 
			this.saveAIToolStripMenuItem.Name = "saveAIToolStripMenuItem";
			this.saveAIToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.saveAIToolStripMenuItem.Text = "&Save AI";
			this.saveAIToolStripMenuItem.Click += new System.EventHandler(this.saveAIToolStripMenuItem_Click);
			// 
			// toolStripMenuItem2
			// 
			this.toolStripMenuItem2.Name = "toolStripMenuItem2";
			this.toolStripMenuItem2.Size = new System.Drawing.Size(149, 6);
			// 
			// clearAllDataToolStripMenuItem
			// 
			this.clearAllDataToolStripMenuItem.Name = "clearAllDataToolStripMenuItem";
			this.clearAllDataToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.clearAllDataToolStripMenuItem.Text = "&Clear All Data";
			// 
			// toolStripMenuItem3
			// 
			this.toolStripMenuItem3.Name = "toolStripMenuItem3";
			this.toolStripMenuItem3.Size = new System.Drawing.Size(149, 6);
			// 
			// exitToolStripMenuItem
			// 
			this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
			this.exitToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
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
			this.tabPageTF.Controls.Add(this.groupBox1);
			this.tabPageTF.Controls.Add(this.lstTF);
			this.tabPageTF.Location = new System.Drawing.Point(4, 22);
			this.tabPageTF.Name = "tabPageTF";
			this.tabPageTF.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageTF.Size = new System.Drawing.Size(772, 508);
			this.tabPageTF.TabIndex = 0;
			this.tabPageTF.Text = "Task Forces";
			this.tabPageTF.UseVisualStyleBackColor = true;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.btnTFDelUnit);
			this.groupBox1.Controls.Add(this.btnTFAddUnit);
			this.groupBox1.Controls.Add(this.cmbTFUnit);
			this.groupBox1.Controls.Add(this.txtTFTotalCost);
			this.groupBox1.Controls.Add(this.cmbTFGroup);
			this.groupBox1.Controls.Add(this.lstTFUnits);
			this.groupBox1.Location = new System.Drawing.Point(333, 6);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(431, 494);
			this.groupBox1.TabIndex = 1;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Units";
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
			this.txtTFTotalCost.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
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
			// lstTFUnits
			// 
			this.lstTFUnits.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colTFUnitCount,
            this.colTFUnit,
            this.colTFUnitCost});
			this.lstTFUnits.FullRowSelect = true;
			this.lstTFUnits.HideSelection = false;
			this.lstTFUnits.LabelEdit = true;
			this.lstTFUnits.Location = new System.Drawing.Point(6, 19);
			this.lstTFUnits.Name = "lstTFUnits";
			this.lstTFUnits.Size = new System.Drawing.Size(419, 442);
			this.lstTFUnits.TabIndex = 0;
			this.lstTFUnits.UseCompatibleStateImageBehavior = false;
			this.lstTFUnits.View = System.Windows.Forms.View.Details;
			this.lstTFUnits.AfterLabelEdit += new System.Windows.Forms.LabelEditEventHandler(this.lstTFUnits_AfterLabelEdit);
			this.lstTFUnits.SelectedIndexChanged += new System.EventHandler(this.lstTFUnits_SelectedIndexChanged);
			this.lstTFUnits.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lstTFUnits_KeyDown);
			// 
			// colTFUnitCount
			// 
			this.colTFUnitCount.Text = "Count";
			// 
			// colTFUnit
			// 
			this.colTFUnit.Text = "Unit";
			this.colTFUnit.Width = 200;
			// 
			// colTFUnitCost
			// 
			this.colTFUnitCost.Text = "Cost";
			// 
			// lstTF
			// 
			this.lstTF.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colTFName,
            this.colTFID,
            this.colTFUses});
			this.lstTF.ContextMenuStrip = this.mnuCtxTF;
			this.lstTF.FullRowSelect = true;
			this.lstTF.HideSelection = false;
			this.lstTF.LabelEdit = true;
			this.lstTF.Location = new System.Drawing.Point(6, 6);
			this.lstTF.MultiSelect = false;
			this.lstTF.Name = "lstTF";
			this.lstTF.Size = new System.Drawing.Size(320, 494);
			this.lstTF.Sorting = System.Windows.Forms.SortOrder.Ascending;
			this.lstTF.TabIndex = 0;
			this.lstTF.UseCompatibleStateImageBehavior = false;
			this.lstTF.View = System.Windows.Forms.View.Details;
			this.lstTF.AfterLabelEdit += new System.Windows.Forms.LabelEditEventHandler(this.lstTF_AfterLabelEdit);
			this.lstTF.SelectedIndexChanged += new System.EventHandler(this.lstTF_SelectedIndexChanged);
			this.lstTF.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lstTF_KeyDown);
			// 
			// colTFName
			// 
			this.colTFName.Text = "Name";
			this.colTFName.Width = 250;
			// 
			// colTFID
			// 
			this.colTFID.Text = "ID";
			this.colTFID.Width = 0;
			// 
			// colTFUses
			// 
			this.colTFUses.Text = "Uses";
			this.colTFUses.Width = 45;
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
			// tabPageST
			// 
			this.tabPageST.Location = new System.Drawing.Point(4, 22);
			this.tabPageST.Name = "tabPageST";
			this.tabPageST.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageST.Size = new System.Drawing.Size(772, 508);
			this.tabPageST.TabIndex = 1;
			this.tabPageST.Text = "Script Types";
			this.tabPageST.UseVisualStyleBackColor = true;
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
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.mnuCtxTF.ResumeLayout(false);
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
		private System.Windows.Forms.ListView lstTF;
		private System.Windows.Forms.ColumnHeader colTFID;
		private System.Windows.Forms.ColumnHeader colTFName;
		private System.Windows.Forms.ColumnHeader colTFUses;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.ListView lstTFUnits;
		private System.Windows.Forms.ColumnHeader colTFUnit;
		private System.Windows.Forms.ColumnHeader colTFUnitCount;
		private System.Windows.Forms.ColumnHeader colTFUnitCost;
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
	}
}