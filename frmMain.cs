using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.IO;

/*
 * *************************
 * Warning: This code sucks!
 * *************************
 */

namespace AIEdit
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class frmMain : System.Windows.Forms.Form
	{
		private Rules rules;
		private TFTable taskforces;
        private STTable scripttypes;
        private TTTable teamtypes;
        private TrTable triggertypes;
        private TaskForce selectedtf;
        private ScriptType selectedst;
        private TeamType selectedtt;
        private TriggerType selectedtr;
        private uint id_counter;
        private StringTable settings;
        private bool ai_opened;

        private static uint ID_BASE = 0x01000000;
        private static uint ID_MAX = 0xFFFFFFFF;

		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabTaskForces;
		private System.Windows.Forms.TabPage tabTeamTypes;
		private System.Windows.Forms.TabPage tabScriptTypes;
		private System.Windows.Forms.TabPage tabTriggerTypes;
		private System.Windows.Forms.MainMenu mainMenu1;
		private System.Windows.Forms.StatusBarPanel panelStatus;
		private System.Windows.Forms.MenuItem mnuFile;
		private System.Windows.Forms.MenuItem mnuExit;
		private System.Windows.Forms.MenuItem menuItem3;
		private System.Windows.Forms.MenuItem mnuLoadRules;
		private System.Windows.Forms.MenuItem mnuLoadAI;
		private System.Windows.Forms.MenuItem mnuSaveAI;
		private System.Windows.Forms.ListView lstTFUnits;
		private System.Windows.Forms.ComboBox cmbTFUnits;
		private System.Windows.Forms.NumericUpDown numTFCount;
		private System.Windows.Forms.ListBox lstTFInfo;
		private System.Windows.Forms.Button btnTFAdd;
		private System.Windows.Forms.ColumnHeader clmTFCount;
		private System.Windows.Forms.Button btnTFModify;
		private System.Windows.Forms.GroupBox grpTF;
		private System.Windows.Forms.ColumnHeader clmTFName;
        private System.Windows.Forms.Button btnTFRemoveUnit;
        private System.Windows.Forms.ListBox lstTF;
        private MenuItem menuItem1;
        private OpenFileDialog openFileDialog1;
        private SaveFileDialog saveFileDialog1;
        private ListBox lstST;
        private GroupBox grpST;
        private ListView lstSTActions;
        private ComboBox cmbSTAction;
        private ComboBox cmbSTParam;
        private ColumnHeader clmSTAction;
        private ColumnHeader clmSTParam;
        private ColumnHeader clmSTNum;
        private TextBox txtSTDesc;
        private ColumnHeader clmSTA;
        private ColumnHeader clmSTP;
        private Button btnSTARemove;
        private Button btnSTAModify;
        private Button btnSTAAdd;
        private Label label1;
        private ComboBox cmbSTAOffset;
        private ListBox lstTT;
        private GroupBox grpTT;
        private Label label3;
        private ComboBox cmbTTScript;
        private ComboBox cmbTTTaskForce;
        private Label label4;
        private Label label5;
        private ComboBox cmbTTMind;
        private Label label6;
        private ComboBox cmbTTGroup;
        private Label label7;
        private ComboBox cmbTTVeteran;
        private CheckedListBox chkTTOptions;
        private NumericUpDown numTTMax;
        private NumericUpDown numTTPriority;
        private Label label9;
        private Label label10;
        private ListBox lstTr;
        private GroupBox grpTr;
        private Label label8;
        private Label label2;
        private ComboBox cmbTrOwner;
        private ComboBox cmbTrTeamType;
        private NumericUpDown numTrTechLevel;
        private Label label11;
        private Label label12;
        private ComboBox cmbTrCondition;
        private NumericUpDown numTrAmount;
        private Label label14;
        private ComboBox cmbTrTechType;
        private NumericUpDown numTrProbMax;
        private NumericUpDown numTrProbMin;
        private NumericUpDown numTrProb;
        private Label label17;
        private Label label16;
        private Label label15;
        private Label label18;
        private ComboBox cmbTrSide;
        private Label label19;
        private ComboBox cmbTrTeamType2;
        private CheckedListBox chkTrOptions;
        private ComboBox cmbTrOperator;
        private Label label13;
        private Label lblTTScript;
        private Label lblTTTaskForce;
        private Label lblTrTeam;
        private Label lblTrTeam2;
        private MenuItem menuItem4;
        private MenuItem mnuClear;
        private Button btnTFCopy;
        private TextBox txtTFName;
        private Button btnTFRename;
        private Button btnTFNew;
        private ComboBox cmbTFGroup;
        private TextBox txtTFID;
        private Button btnTFRemove;
        private Button btnSTCopy;
        private TextBox txtSTName;
        private Button btnSTRename;
        private TextBox txtSTID;
        private Button btnSTNew;
        private Button btnSTRemove;
        private TextBox txtTTName;
        private Button btnTTCopy;
        private TextBox txtTTID;
        private Button btnTTRemove;
        private Button btnTTRename;
        private Button btnTTNew;
        private TextBox txtTrName;
        private Button btnTrCopy;
        private TextBox txtTrID;
        private Button btnTrRename;
        private Button btnTrRemove;
        private Button btnTrNew;
        private MenuItem mnuHelp;
        private MenuItem mnuInfo;
        private MenuItem mnuHelp2;
        private MenuItem menuItem2;
        private Button btnSTAInsert;
        private Button btnSTAUp;
        private Button btnSTADown;
        private Label label20;
        private NumericUpDown numTTTechLevel;
		private ComboBox cmbTTHouse;
		private Label label21;
		private MenuItem mnuLoadRulesRA2;
		private MenuItem mnuLoadRulesTS;
        private IContainer components;

		public frmMain()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//

            ai_opened = false;

            openFileDialog1.Filter = "Ini files (*.ini)|*.ini";
            openFileDialog1.FileName = "";
            saveFileDialog1.Filter = "Ini files (*.ini)|*.ini";
            openFileDialog1.FileName = "";
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabTaskForces = new System.Windows.Forms.TabPage();
			this.btnTFCopy = new System.Windows.Forms.Button();
			this.txtTFName = new System.Windows.Forms.TextBox();
			this.btnTFRename = new System.Windows.Forms.Button();
			this.btnTFNew = new System.Windows.Forms.Button();
			this.txtTFID = new System.Windows.Forms.TextBox();
			this.btnTFRemove = new System.Windows.Forms.Button();
			this.lstTF = new System.Windows.Forms.ListBox();
			this.grpTF = new System.Windows.Forms.GroupBox();
			this.numTFCount = new System.Windows.Forms.NumericUpDown();
			this.cmbTFUnits = new System.Windows.Forms.ComboBox();
			this.lstTFUnits = new System.Windows.Forms.ListView();
			this.clmTFName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.clmTFCount = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.btnTFModify = new System.Windows.Forms.Button();
			this.cmbTFGroup = new System.Windows.Forms.ComboBox();
			this.btnTFRemoveUnit = new System.Windows.Forms.Button();
			this.lstTFInfo = new System.Windows.Forms.ListBox();
			this.btnTFAdd = new System.Windows.Forms.Button();
			this.tabScriptTypes = new System.Windows.Forms.TabPage();
			this.btnSTCopy = new System.Windows.Forms.Button();
			this.txtSTName = new System.Windows.Forms.TextBox();
			this.btnSTRename = new System.Windows.Forms.Button();
			this.txtSTID = new System.Windows.Forms.TextBox();
			this.btnSTNew = new System.Windows.Forms.Button();
			this.btnSTRemove = new System.Windows.Forms.Button();
			this.grpST = new System.Windows.Forms.GroupBox();
			this.btnSTAUp = new System.Windows.Forms.Button();
			this.btnSTADown = new System.Windows.Forms.Button();
			this.btnSTAInsert = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.cmbSTAOffset = new System.Windows.Forms.ComboBox();
			this.btnSTAAdd = new System.Windows.Forms.Button();
			this.btnSTAModify = new System.Windows.Forms.Button();
			this.btnSTARemove = new System.Windows.Forms.Button();
			this.txtSTDesc = new System.Windows.Forms.TextBox();
			this.cmbSTParam = new System.Windows.Forms.ComboBox();
			this.cmbSTAction = new System.Windows.Forms.ComboBox();
			this.lstSTActions = new System.Windows.Forms.ListView();
			this.clmSTA = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.clmSTP = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.clmSTNum = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.clmSTAction = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.clmSTParam = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.lstST = new System.Windows.Forms.ListBox();
			this.tabTeamTypes = new System.Windows.Forms.TabPage();
			this.txtTTName = new System.Windows.Forms.TextBox();
			this.btnTTCopy = new System.Windows.Forms.Button();
			this.grpTT = new System.Windows.Forms.GroupBox();
			this.cmbTTHouse = new System.Windows.Forms.ComboBox();
			this.label21 = new System.Windows.Forms.Label();
			this.numTTTechLevel = new System.Windows.Forms.NumericUpDown();
			this.label20 = new System.Windows.Forms.Label();
			this.lblTTTaskForce = new System.Windows.Forms.Label();
			this.lblTTScript = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.numTTMax = new System.Windows.Forms.NumericUpDown();
			this.numTTPriority = new System.Windows.Forms.NumericUpDown();
			this.chkTTOptions = new System.Windows.Forms.CheckedListBox();
			this.label7 = new System.Windows.Forms.Label();
			this.cmbTTVeteran = new System.Windows.Forms.ComboBox();
			this.label6 = new System.Windows.Forms.Label();
			this.cmbTTGroup = new System.Windows.Forms.ComboBox();
			this.label5 = new System.Windows.Forms.Label();
			this.cmbTTMind = new System.Windows.Forms.ComboBox();
			this.cmbTTTaskForce = new System.Windows.Forms.ComboBox();
			this.label4 = new System.Windows.Forms.Label();
			this.cmbTTScript = new System.Windows.Forms.ComboBox();
			this.label3 = new System.Windows.Forms.Label();
			this.txtTTID = new System.Windows.Forms.TextBox();
			this.lstTT = new System.Windows.Forms.ListBox();
			this.btnTTRemove = new System.Windows.Forms.Button();
			this.btnTTRename = new System.Windows.Forms.Button();
			this.btnTTNew = new System.Windows.Forms.Button();
			this.tabTriggerTypes = new System.Windows.Forms.TabPage();
			this.txtTrName = new System.Windows.Forms.TextBox();
			this.btnTrCopy = new System.Windows.Forms.Button();
			this.txtTrID = new System.Windows.Forms.TextBox();
			this.btnTrRename = new System.Windows.Forms.Button();
			this.btnTrRemove = new System.Windows.Forms.Button();
			this.btnTrNew = new System.Windows.Forms.Button();
			this.grpTr = new System.Windows.Forms.GroupBox();
			this.lblTrTeam2 = new System.Windows.Forms.Label();
			this.lblTrTeam = new System.Windows.Forms.Label();
			this.label13 = new System.Windows.Forms.Label();
			this.cmbTrOperator = new System.Windows.Forms.ComboBox();
			this.chkTrOptions = new System.Windows.Forms.CheckedListBox();
			this.label19 = new System.Windows.Forms.Label();
			this.cmbTrTeamType2 = new System.Windows.Forms.ComboBox();
			this.label18 = new System.Windows.Forms.Label();
			this.cmbTrSide = new System.Windows.Forms.ComboBox();
			this.label17 = new System.Windows.Forms.Label();
			this.label16 = new System.Windows.Forms.Label();
			this.label15 = new System.Windows.Forms.Label();
			this.numTrProbMax = new System.Windows.Forms.NumericUpDown();
			this.numTrProbMin = new System.Windows.Forms.NumericUpDown();
			this.numTrProb = new System.Windows.Forms.NumericUpDown();
			this.label14 = new System.Windows.Forms.Label();
			this.cmbTrTechType = new System.Windows.Forms.ComboBox();
			this.numTrAmount = new System.Windows.Forms.NumericUpDown();
			this.label12 = new System.Windows.Forms.Label();
			this.cmbTrCondition = new System.Windows.Forms.ComboBox();
			this.label11 = new System.Windows.Forms.Label();
			this.numTrTechLevel = new System.Windows.Forms.NumericUpDown();
			this.label8 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.cmbTrOwner = new System.Windows.Forms.ComboBox();
			this.cmbTrTeamType = new System.Windows.Forms.ComboBox();
			this.lstTr = new System.Windows.Forms.ListBox();
			this.mainMenu1 = new System.Windows.Forms.MainMenu(this.components);
			this.mnuFile = new System.Windows.Forms.MenuItem();
			this.mnuLoadRules = new System.Windows.Forms.MenuItem();
			this.mnuLoadAI = new System.Windows.Forms.MenuItem();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.mnuSaveAI = new System.Windows.Forms.MenuItem();
			this.menuItem4 = new System.Windows.Forms.MenuItem();
			this.mnuClear = new System.Windows.Forms.MenuItem();
			this.menuItem3 = new System.Windows.Forms.MenuItem();
			this.mnuExit = new System.Windows.Forms.MenuItem();
			this.mnuHelp = new System.Windows.Forms.MenuItem();
			this.mnuHelp2 = new System.Windows.Forms.MenuItem();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			this.mnuInfo = new System.Windows.Forms.MenuItem();
			this.panelStatus = new System.Windows.Forms.StatusBarPanel();
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
			this.mnuLoadRulesRA2 = new System.Windows.Forms.MenuItem();
			this.mnuLoadRulesTS = new System.Windows.Forms.MenuItem();
			this.tabControl1.SuspendLayout();
			this.tabTaskForces.SuspendLayout();
			this.grpTF.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numTFCount)).BeginInit();
			this.tabScriptTypes.SuspendLayout();
			this.grpST.SuspendLayout();
			this.tabTeamTypes.SuspendLayout();
			this.grpTT.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numTTTechLevel)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numTTMax)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numTTPriority)).BeginInit();
			this.tabTriggerTypes.SuspendLayout();
			this.grpTr.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numTrProbMax)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numTrProbMin)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numTrProb)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numTrAmount)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numTrTechLevel)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.panelStatus)).BeginInit();
			this.SuspendLayout();
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tabTaskForces);
			this.tabControl1.Controls.Add(this.tabScriptTypes);
			this.tabControl1.Controls.Add(this.tabTeamTypes);
			this.tabControl1.Controls.Add(this.tabTriggerTypes);
			this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl1.Enabled = false;
			this.tabControl1.Location = new System.Drawing.Point(0, 0);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(788, 501);
			this.tabControl1.TabIndex = 0;
			// 
			// tabTaskForces
			// 
			this.tabTaskForces.Controls.Add(this.btnTFCopy);
			this.tabTaskForces.Controls.Add(this.txtTFName);
			this.tabTaskForces.Controls.Add(this.btnTFRename);
			this.tabTaskForces.Controls.Add(this.btnTFNew);
			this.tabTaskForces.Controls.Add(this.txtTFID);
			this.tabTaskForces.Controls.Add(this.btnTFRemove);
			this.tabTaskForces.Controls.Add(this.lstTF);
			this.tabTaskForces.Controls.Add(this.grpTF);
			this.tabTaskForces.Location = new System.Drawing.Point(4, 22);
			this.tabTaskForces.Name = "tabTaskForces";
			this.tabTaskForces.Size = new System.Drawing.Size(780, 475);
			this.tabTaskForces.TabIndex = 0;
			this.tabTaskForces.Text = "TaskForces";
			// 
			// btnTFCopy
			// 
			this.btnTFCopy.Enabled = false;
			this.btnTFCopy.Location = new System.Drawing.Point(200, 440);
			this.btnTFCopy.Name = "btnTFCopy";
			this.btnTFCopy.Size = new System.Drawing.Size(64, 23);
			this.btnTFCopy.TabIndex = 25;
			this.btnTFCopy.Text = "Copy";
			this.btnTFCopy.UseVisualStyleBackColor = true;
			this.btnTFCopy.Click += new System.EventHandler(this.btnTFCopy_Click);
			// 
			// txtTFName
			// 
			this.txtTFName.Location = new System.Drawing.Point(8, 388);
			this.txtTFName.Name = "txtTFName";
			this.txtTFName.Size = new System.Drawing.Size(256, 20);
			this.txtTFName.TabIndex = 19;
			// 
			// btnTFRename
			// 
			this.btnTFRename.Enabled = false;
			this.btnTFRename.Location = new System.Drawing.Point(272, 388);
			this.btnTFRename.Name = "btnTFRename";
			this.btnTFRename.Size = new System.Drawing.Size(64, 21);
			this.btnTFRename.TabIndex = 24;
			this.btnTFRename.Text = "Rename";
			this.btnTFRename.UseVisualStyleBackColor = true;
			this.btnTFRename.Click += new System.EventHandler(this.btnTFSetName_Click);
			// 
			// btnTFNew
			// 
			this.btnTFNew.Location = new System.Drawing.Point(128, 440);
			this.btnTFNew.Name = "btnTFNew";
			this.btnTFNew.Size = new System.Drawing.Size(64, 23);
			this.btnTFNew.TabIndex = 21;
			this.btnTFNew.Text = "New";
			this.btnTFNew.Click += new System.EventHandler(this.btnTFNew_Click);
			// 
			// txtTFID
			// 
			this.txtTFID.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtTFID.Location = new System.Drawing.Point(8, 414);
			this.txtTFID.Name = "txtTFID";
			this.txtTFID.ReadOnly = true;
			this.txtTFID.Size = new System.Drawing.Size(328, 21);
			this.txtTFID.TabIndex = 20;
			this.txtTFID.TabStop = false;
			// 
			// btnTFRemove
			// 
			this.btnTFRemove.Enabled = false;
			this.btnTFRemove.Location = new System.Drawing.Point(272, 440);
			this.btnTFRemove.Name = "btnTFRemove";
			this.btnTFRemove.Size = new System.Drawing.Size(64, 23);
			this.btnTFRemove.TabIndex = 22;
			this.btnTFRemove.Text = "Remove";
			this.btnTFRemove.Click += new System.EventHandler(this.btnTFRemove_Click_1);
			// 
			// lstTF
			// 
			this.lstTF.Location = new System.Drawing.Point(8, 16);
			this.lstTF.Name = "lstTF";
			this.lstTF.Size = new System.Drawing.Size(328, 368);
			this.lstTF.TabIndex = 15;
			this.lstTF.SelectedIndexChanged += new System.EventHandler(this.lstTF_SelectedIndexChanged);
			// 
			// grpTF
			// 
			this.grpTF.Controls.Add(this.numTFCount);
			this.grpTF.Controls.Add(this.cmbTFUnits);
			this.grpTF.Controls.Add(this.lstTFUnits);
			this.grpTF.Controls.Add(this.btnTFModify);
			this.grpTF.Controls.Add(this.cmbTFGroup);
			this.grpTF.Controls.Add(this.btnTFRemoveUnit);
			this.grpTF.Controls.Add(this.lstTFInfo);
			this.grpTF.Controls.Add(this.btnTFAdd);
			this.grpTF.Enabled = false;
			this.grpTF.Location = new System.Drawing.Point(344, 16);
			this.grpTF.Name = "grpTF";
			this.grpTF.Size = new System.Drawing.Size(429, 448);
			this.grpTF.TabIndex = 9;
			this.grpTF.TabStop = false;
			this.grpTF.Text = "Units";
			// 
			// numTFCount
			// 
			this.numTFCount.Location = new System.Drawing.Point(357, 345);
			this.numTFCount.Name = "numTFCount";
			this.numTFCount.Size = new System.Drawing.Size(64, 20);
			this.numTFCount.TabIndex = 4;
			// 
			// cmbTFUnits
			// 
			this.cmbTFUnits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbTFUnits.Location = new System.Drawing.Point(8, 344);
			this.cmbTFUnits.Name = "cmbTFUnits";
			this.cmbTFUnits.Size = new System.Drawing.Size(345, 21);
			this.cmbTFUnits.TabIndex = 0;
			this.cmbTFUnits.SelectedIndexChanged += new System.EventHandler(this.cmbTFUnits_SelectedIndexChanged);
			// 
			// lstTFUnits
			// 
			this.lstTFUnits.AutoArrange = false;
			this.lstTFUnits.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.clmTFName,
            this.clmTFCount});
			this.lstTFUnits.FullRowSelect = true;
			this.lstTFUnits.HideSelection = false;
			this.lstTFUnits.Location = new System.Drawing.Point(6, 16);
			this.lstTFUnits.MultiSelect = false;
			this.lstTFUnits.Name = "lstTFUnits";
			this.lstTFUnits.Size = new System.Drawing.Size(415, 320);
			this.lstTFUnits.TabIndex = 2;
			this.lstTFUnits.UseCompatibleStateImageBehavior = false;
			this.lstTFUnits.View = System.Windows.Forms.View.Details;
			this.lstTFUnits.SelectedIndexChanged += new System.EventHandler(this.lstTFUnits_SelectedIndexChanged);
			// 
			// clmTFName
			// 
			this.clmTFName.Text = "Name";
			this.clmTFName.Width = 310;
			// 
			// clmTFCount
			// 
			this.clmTFCount.Text = "Count";
			this.clmTFCount.Width = 94;
			// 
			// btnTFModify
			// 
			this.btnTFModify.Location = new System.Drawing.Point(360, 416);
			this.btnTFModify.Name = "btnTFModify";
			this.btnTFModify.Size = new System.Drawing.Size(64, 24);
			this.btnTFModify.TabIndex = 7;
			this.btnTFModify.Text = "A&pply";
			this.btnTFModify.Click += new System.EventHandler(this.btnTFModify_Click);
			// 
			// cmbTFGroup
			// 
			this.cmbTFGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbTFGroup.Enabled = false;
			this.cmbTFGroup.Location = new System.Drawing.Point(8, 416);
			this.cmbTFGroup.Name = "cmbTFGroup";
			this.cmbTFGroup.Size = new System.Drawing.Size(208, 21);
			this.cmbTFGroup.TabIndex = 23;
			this.cmbTFGroup.SelectedIndexChanged += new System.EventHandler(this.cmbTFGroup_SelectedIndexChanged_1);
			// 
			// btnTFRemoveUnit
			// 
			this.btnTFRemoveUnit.Location = new System.Drawing.Point(290, 416);
			this.btnTFRemoveUnit.Name = "btnTFRemoveUnit";
			this.btnTFRemoveUnit.Size = new System.Drawing.Size(64, 24);
			this.btnTFRemoveUnit.TabIndex = 8;
			this.btnTFRemoveUnit.Text = "&Remove";
			this.btnTFRemoveUnit.Click += new System.EventHandler(this.btnTFRemove_Click);
			// 
			// lstTFInfo
			// 
			this.lstTFInfo.Location = new System.Drawing.Point(8, 368);
			this.lstTFInfo.Name = "lstTFInfo";
			this.lstTFInfo.ScrollAlwaysVisible = true;
			this.lstTFInfo.SelectionMode = System.Windows.Forms.SelectionMode.None;
			this.lstTFInfo.Size = new System.Drawing.Size(415, 43);
			this.lstTFInfo.TabIndex = 5;
			// 
			// btnTFAdd
			// 
			this.btnTFAdd.Location = new System.Drawing.Point(220, 416);
			this.btnTFAdd.Name = "btnTFAdd";
			this.btnTFAdd.Size = new System.Drawing.Size(64, 24);
			this.btnTFAdd.TabIndex = 6;
			this.btnTFAdd.Text = "&Add";
			this.btnTFAdd.Click += new System.EventHandler(this.btnTFAdd_Click);
			// 
			// tabScriptTypes
			// 
			this.tabScriptTypes.Controls.Add(this.btnSTCopy);
			this.tabScriptTypes.Controls.Add(this.txtSTName);
			this.tabScriptTypes.Controls.Add(this.btnSTRename);
			this.tabScriptTypes.Controls.Add(this.txtSTID);
			this.tabScriptTypes.Controls.Add(this.btnSTNew);
			this.tabScriptTypes.Controls.Add(this.btnSTRemove);
			this.tabScriptTypes.Controls.Add(this.grpST);
			this.tabScriptTypes.Controls.Add(this.lstST);
			this.tabScriptTypes.Location = new System.Drawing.Point(4, 22);
			this.tabScriptTypes.Name = "tabScriptTypes";
			this.tabScriptTypes.Size = new System.Drawing.Size(780, 475);
			this.tabScriptTypes.TabIndex = 2;
			this.tabScriptTypes.Text = "ScriptTypes";
			// 
			// btnSTCopy
			// 
			this.btnSTCopy.Enabled = false;
			this.btnSTCopy.Location = new System.Drawing.Point(200, 440);
			this.btnSTCopy.Name = "btnSTCopy";
			this.btnSTCopy.Size = new System.Drawing.Size(64, 23);
			this.btnSTCopy.TabIndex = 13;
			this.btnSTCopy.Text = "Copy";
			this.btnSTCopy.UseVisualStyleBackColor = true;
			this.btnSTCopy.Click += new System.EventHandler(this.btnSTCopy_Click);
			// 
			// txtSTName
			// 
			this.txtSTName.Location = new System.Drawing.Point(8, 388);
			this.txtSTName.Name = "txtSTName";
			this.txtSTName.Size = new System.Drawing.Size(256, 20);
			this.txtSTName.TabIndex = 8;
			// 
			// btnSTRename
			// 
			this.btnSTRename.Enabled = false;
			this.btnSTRename.Location = new System.Drawing.Point(272, 388);
			this.btnSTRename.Name = "btnSTRename";
			this.btnSTRename.Size = new System.Drawing.Size(64, 21);
			this.btnSTRename.TabIndex = 12;
			this.btnSTRename.Text = "Rename";
			this.btnSTRename.UseVisualStyleBackColor = true;
			this.btnSTRename.Click += new System.EventHandler(this.btnSTSetName_Click);
			// 
			// txtSTID
			// 
			this.txtSTID.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtSTID.Location = new System.Drawing.Point(8, 414);
			this.txtSTID.Name = "txtSTID";
			this.txtSTID.ReadOnly = true;
			this.txtSTID.Size = new System.Drawing.Size(328, 21);
			this.txtSTID.TabIndex = 9;
			// 
			// btnSTNew
			// 
			this.btnSTNew.Location = new System.Drawing.Point(128, 440);
			this.btnSTNew.Name = "btnSTNew";
			this.btnSTNew.Size = new System.Drawing.Size(64, 23);
			this.btnSTNew.TabIndex = 11;
			this.btnSTNew.Text = "New";
			this.btnSTNew.UseVisualStyleBackColor = true;
			this.btnSTNew.Click += new System.EventHandler(this.btnSTNew_Click);
			// 
			// btnSTRemove
			// 
			this.btnSTRemove.Enabled = false;
			this.btnSTRemove.Location = new System.Drawing.Point(272, 440);
			this.btnSTRemove.Name = "btnSTRemove";
			this.btnSTRemove.Size = new System.Drawing.Size(64, 23);
			this.btnSTRemove.TabIndex = 10;
			this.btnSTRemove.Text = "Remove";
			this.btnSTRemove.UseVisualStyleBackColor = true;
			this.btnSTRemove.Click += new System.EventHandler(this.btnSTRemove_Click_1);
			// 
			// grpST
			// 
			this.grpST.Controls.Add(this.btnSTAUp);
			this.grpST.Controls.Add(this.btnSTADown);
			this.grpST.Controls.Add(this.btnSTAInsert);
			this.grpST.Controls.Add(this.label1);
			this.grpST.Controls.Add(this.cmbSTAOffset);
			this.grpST.Controls.Add(this.btnSTAAdd);
			this.grpST.Controls.Add(this.btnSTAModify);
			this.grpST.Controls.Add(this.btnSTARemove);
			this.grpST.Controls.Add(this.txtSTDesc);
			this.grpST.Controls.Add(this.cmbSTParam);
			this.grpST.Controls.Add(this.cmbSTAction);
			this.grpST.Controls.Add(this.lstSTActions);
			this.grpST.Enabled = false;
			this.grpST.Location = new System.Drawing.Point(344, 16);
			this.grpST.Name = "grpST";
			this.grpST.Size = new System.Drawing.Size(429, 456);
			this.grpST.TabIndex = 3;
			this.grpST.TabStop = false;
			this.grpST.Text = "Actions";
			// 
			// btnSTAUp
			// 
			this.btnSTAUp.Location = new System.Drawing.Point(14, 339);
			this.btnSTAUp.Name = "btnSTAUp";
			this.btnSTAUp.Size = new System.Drawing.Size(64, 23);
			this.btnSTAUp.TabIndex = 26;
			this.btnSTAUp.Text = "Up";
			this.btnSTAUp.UseVisualStyleBackColor = true;
			this.btnSTAUp.Click += new System.EventHandler(this.btnSTAUp_Click);
			// 
			// btnSTADown
			// 
			this.btnSTADown.Location = new System.Drawing.Point(84, 339);
			this.btnSTADown.Name = "btnSTADown";
			this.btnSTADown.Size = new System.Drawing.Size(64, 23);
			this.btnSTADown.TabIndex = 25;
			this.btnSTADown.Text = "Down";
			this.btnSTADown.UseVisualStyleBackColor = true;
			this.btnSTADown.Click += new System.EventHandler(this.btnSTADown_Click);
			// 
			// btnSTAInsert
			// 
			this.btnSTAInsert.Location = new System.Drawing.Point(154, 339);
			this.btnSTAInsert.Name = "btnSTAInsert";
			this.btnSTAInsert.Size = new System.Drawing.Size(64, 23);
			this.btnSTAInsert.TabIndex = 24;
			this.btnSTAInsert.Text = "Insert";
			this.btnSTAInsert.UseVisualStyleBackColor = true;
			this.btnSTAInsert.Click += new System.EventHandler(this.btnSTAInsert_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(16, 316);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(180, 13);
			this.label1.TabIndex = 23;
			this.label1.Text = "Offset (only used with BuildingTypes)";
			// 
			// cmbSTAOffset
			// 
			this.cmbSTAOffset.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbSTAOffset.FormattingEnabled = true;
			this.cmbSTAOffset.Location = new System.Drawing.Point(208, 312);
			this.cmbSTAOffset.Name = "cmbSTAOffset";
			this.cmbSTAOffset.Size = new System.Drawing.Size(217, 21);
			this.cmbSTAOffset.TabIndex = 22;
			this.cmbSTAOffset.SelectedIndexChanged += new System.EventHandler(this.cmbSTAOffset_SelectedIndexChanged);
			// 
			// btnSTAAdd
			// 
			this.btnSTAAdd.Location = new System.Drawing.Point(224, 339);
			this.btnSTAAdd.Name = "btnSTAAdd";
			this.btnSTAAdd.Size = new System.Drawing.Size(64, 23);
			this.btnSTAAdd.TabIndex = 21;
			this.btnSTAAdd.Text = "&Add";
			this.btnSTAAdd.UseVisualStyleBackColor = true;
			this.btnSTAAdd.Click += new System.EventHandler(this.btnSTAdd_Click);
			// 
			// btnSTAModify
			// 
			this.btnSTAModify.Location = new System.Drawing.Point(363, 339);
			this.btnSTAModify.Name = "btnSTAModify";
			this.btnSTAModify.Size = new System.Drawing.Size(64, 23);
			this.btnSTAModify.TabIndex = 20;
			this.btnSTAModify.Text = "A&pply";
			this.btnSTAModify.UseVisualStyleBackColor = true;
			this.btnSTAModify.Click += new System.EventHandler(this.btnSTModify_Click);
			// 
			// btnSTARemove
			// 
			this.btnSTARemove.Location = new System.Drawing.Point(294, 339);
			this.btnSTARemove.Name = "btnSTARemove";
			this.btnSTARemove.Size = new System.Drawing.Size(64, 23);
			this.btnSTARemove.TabIndex = 19;
			this.btnSTARemove.Text = "&Remove";
			this.btnSTARemove.UseVisualStyleBackColor = true;
			this.btnSTARemove.Click += new System.EventHandler(this.btnSTRemove_Click);
			// 
			// txtSTDesc
			// 
			this.txtSTDesc.Location = new System.Drawing.Point(8, 368);
			this.txtSTDesc.Multiline = true;
			this.txtSTDesc.Name = "txtSTDesc";
			this.txtSTDesc.ReadOnly = true;
			this.txtSTDesc.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.txtSTDesc.Size = new System.Drawing.Size(417, 80);
			this.txtSTDesc.TabIndex = 3;
			// 
			// cmbSTParam
			// 
			this.cmbSTParam.FormattingEnabled = true;
			this.cmbSTParam.Location = new System.Drawing.Point(8, 283);
			this.cmbSTParam.Name = "cmbSTParam";
			this.cmbSTParam.Size = new System.Drawing.Size(417, 21);
			this.cmbSTParam.TabIndex = 2;
			// 
			// cmbSTAction
			// 
			this.cmbSTAction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbSTAction.FormattingEnabled = true;
			this.cmbSTAction.Location = new System.Drawing.Point(8, 256);
			this.cmbSTAction.Name = "cmbSTAction";
			this.cmbSTAction.Size = new System.Drawing.Size(417, 21);
			this.cmbSTAction.TabIndex = 1;
			this.cmbSTAction.SelectedIndexChanged += new System.EventHandler(this.cmbSTAction_SelectedIndexChanged);
			// 
			// lstSTActions
			// 
			this.lstSTActions.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.clmSTA,
            this.clmSTP,
            this.clmSTNum,
            this.clmSTAction,
            this.clmSTParam});
			this.lstSTActions.FullRowSelect = true;
			this.lstSTActions.HideSelection = false;
			this.lstSTActions.Location = new System.Drawing.Point(6, 19);
			this.lstSTActions.MultiSelect = false;
			this.lstSTActions.Name = "lstSTActions";
			this.lstSTActions.Size = new System.Drawing.Size(418, 229);
			this.lstSTActions.TabIndex = 0;
			this.lstSTActions.UseCompatibleStateImageBehavior = false;
			this.lstSTActions.View = System.Windows.Forms.View.Details;
			this.lstSTActions.SelectedIndexChanged += new System.EventHandler(this.lstSTActions_SelectedIndexChanged);
			// 
			// clmSTA
			// 
			this.clmSTA.Text = "A";
			this.clmSTA.Width = 0;
			// 
			// clmSTP
			// 
			this.clmSTP.Text = "P";
			this.clmSTP.Width = 0;
			// 
			// clmSTNum
			// 
			this.clmSTNum.Text = "#";
			this.clmSTNum.Width = 27;
			// 
			// clmSTAction
			// 
			this.clmSTAction.Text = "Action";
			this.clmSTAction.Width = 192;
			// 
			// clmSTParam
			// 
			this.clmSTParam.Text = "Parameter";
			this.clmSTParam.Width = 185;
			// 
			// lstST
			// 
			this.lstST.FormattingEnabled = true;
			this.lstST.Location = new System.Drawing.Point(8, 16);
			this.lstST.Name = "lstST";
			this.lstST.Size = new System.Drawing.Size(328, 368);
			this.lstST.TabIndex = 0;
			this.lstST.SelectedIndexChanged += new System.EventHandler(this.lstST_SelectedIndexChanged);
			// 
			// tabTeamTypes
			// 
			this.tabTeamTypes.Controls.Add(this.txtTTName);
			this.tabTeamTypes.Controls.Add(this.btnTTCopy);
			this.tabTeamTypes.Controls.Add(this.grpTT);
			this.tabTeamTypes.Controls.Add(this.txtTTID);
			this.tabTeamTypes.Controls.Add(this.lstTT);
			this.tabTeamTypes.Controls.Add(this.btnTTRemove);
			this.tabTeamTypes.Controls.Add(this.btnTTRename);
			this.tabTeamTypes.Controls.Add(this.btnTTNew);
			this.tabTeamTypes.Location = new System.Drawing.Point(4, 22);
			this.tabTeamTypes.Name = "tabTeamTypes";
			this.tabTeamTypes.Size = new System.Drawing.Size(780, 475);
			this.tabTeamTypes.TabIndex = 1;
			this.tabTeamTypes.Text = "TeamTypes";
			// 
			// txtTTName
			// 
			this.txtTTName.Location = new System.Drawing.Point(8, 388);
			this.txtTTName.Name = "txtTTName";
			this.txtTTName.Size = new System.Drawing.Size(256, 20);
			this.txtTTName.TabIndex = 35;
			// 
			// btnTTCopy
			// 
			this.btnTTCopy.Enabled = false;
			this.btnTTCopy.Location = new System.Drawing.Point(200, 440);
			this.btnTTCopy.Name = "btnTTCopy";
			this.btnTTCopy.Size = new System.Drawing.Size(64, 23);
			this.btnTTCopy.TabIndex = 39;
			this.btnTTCopy.Text = "Copy";
			this.btnTTCopy.UseVisualStyleBackColor = true;
			this.btnTTCopy.Click += new System.EventHandler(this.btnTTCopy_Click);
			// 
			// grpTT
			// 
			this.grpTT.Controls.Add(this.cmbTTHouse);
			this.grpTT.Controls.Add(this.label21);
			this.grpTT.Controls.Add(this.numTTTechLevel);
			this.grpTT.Controls.Add(this.label20);
			this.grpTT.Controls.Add(this.lblTTTaskForce);
			this.grpTT.Controls.Add(this.lblTTScript);
			this.grpTT.Controls.Add(this.label10);
			this.grpTT.Controls.Add(this.label9);
			this.grpTT.Controls.Add(this.numTTMax);
			this.grpTT.Controls.Add(this.numTTPriority);
			this.grpTT.Controls.Add(this.chkTTOptions);
			this.grpTT.Controls.Add(this.label7);
			this.grpTT.Controls.Add(this.cmbTTVeteran);
			this.grpTT.Controls.Add(this.label6);
			this.grpTT.Controls.Add(this.cmbTTGroup);
			this.grpTT.Controls.Add(this.label5);
			this.grpTT.Controls.Add(this.cmbTTMind);
			this.grpTT.Controls.Add(this.cmbTTTaskForce);
			this.grpTT.Controls.Add(this.label4);
			this.grpTT.Controls.Add(this.cmbTTScript);
			this.grpTT.Controls.Add(this.label3);
			this.grpTT.Enabled = false;
			this.grpTT.Location = new System.Drawing.Point(344, 16);
			this.grpTT.Name = "grpTT";
			this.grpTT.Size = new System.Drawing.Size(429, 456);
			this.grpTT.TabIndex = 1;
			this.grpTT.TabStop = false;
			this.grpTT.Text = "Settings";
			// 
			// cmbTTHouse
			// 
			this.cmbTTHouse.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbTTHouse.FormattingEnabled = true;
			this.cmbTTHouse.Location = new System.Drawing.Point(128, 152);
			this.cmbTTHouse.Name = "cmbTTHouse";
			this.cmbTTHouse.Size = new System.Drawing.Size(296, 21);
			this.cmbTTHouse.TabIndex = 37;
			this.cmbTTHouse.SelectedIndexChanged += new System.EventHandler(this.cmbTTHouse_SelectedIndexChanged);
			// 
			// label21
			// 
			this.label21.AutoSize = true;
			this.label21.Location = new System.Drawing.Point(12, 156);
			this.label21.Name = "label21";
			this.label21.Size = new System.Drawing.Size(38, 13);
			this.label21.TabIndex = 36;
			this.label21.Text = "House";
			// 
			// numTTTechLevel
			// 
			this.numTTTechLevel.Location = new System.Drawing.Point(128, 224);
			this.numTTTechLevel.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
			this.numTTTechLevel.Name = "numTTTechLevel";
			this.numTTTechLevel.Size = new System.Drawing.Size(296, 20);
			this.numTTTechLevel.TabIndex = 35;
			this.numTTTechLevel.ValueChanged += new System.EventHandler(this.numTTTechLevel_ValueChanged);
			// 
			// label20
			// 
			this.label20.AutoSize = true;
			this.label20.Location = new System.Drawing.Point(12, 228);
			this.label20.Name = "label20";
			this.label20.Size = new System.Drawing.Size(61, 13);
			this.label20.TabIndex = 34;
			this.label20.Text = "Tech Level";
			// 
			// lblTTTaskForce
			// 
			this.lblTTTaskForce.AutoSize = true;
			this.lblTTTaskForce.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblTTTaskForce.Location = new System.Drawing.Point(124, 48);
			this.lblTTTaskForce.Name = "lblTTTaskForce";
			this.lblTTTaskForce.Size = new System.Drawing.Size(49, 15);
			this.lblTTTaskForce.TabIndex = 33;
			this.lblTTTaskForce.Text = "<none>";
			// 
			// lblTTScript
			// 
			this.lblTTScript.AutoSize = true;
			this.lblTTScript.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblTTScript.Location = new System.Drawing.Point(124, 21);
			this.lblTTScript.Name = "lblTTScript";
			this.lblTTScript.Size = new System.Drawing.Size(49, 15);
			this.lblTTScript.TabIndex = 32;
			this.lblTTScript.Text = "<none>";
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Location = new System.Drawing.Point(12, 204);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(27, 13);
			this.label10.TabIndex = 31;
			this.label10.Text = "Max";
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Location = new System.Drawing.Point(12, 180);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(38, 13);
			this.label9.TabIndex = 30;
			this.label9.Text = "Priority";
			// 
			// numTTMax
			// 
			this.numTTMax.Location = new System.Drawing.Point(128, 200);
			this.numTTMax.Name = "numTTMax";
			this.numTTMax.Size = new System.Drawing.Size(296, 20);
			this.numTTMax.TabIndex = 29;
			this.numTTMax.ValueChanged += new System.EventHandler(this.numTTMax_ValueChanged);
			// 
			// numTTPriority
			// 
			this.numTTPriority.Location = new System.Drawing.Point(128, 176);
			this.numTTPriority.Name = "numTTPriority";
			this.numTTPriority.Size = new System.Drawing.Size(296, 20);
			this.numTTPriority.TabIndex = 28;
			this.numTTPriority.ValueChanged += new System.EventHandler(this.numTTPriority_ValueChanged);
			// 
			// chkTTOptions
			// 
			this.chkTTOptions.CheckOnClick = true;
			this.chkTTOptions.ColumnWidth = 175;
			this.chkTTOptions.FormattingEnabled = true;
			this.chkTTOptions.HorizontalScrollbar = true;
			this.chkTTOptions.Location = new System.Drawing.Point(14, 248);
			this.chkTTOptions.MultiColumn = true;
			this.chkTTOptions.Name = "chkTTOptions";
			this.chkTTOptions.Size = new System.Drawing.Size(409, 199);
			this.chkTTOptions.TabIndex = 27;
			this.chkTTOptions.SelectedValueChanged += new System.EventHandler(this.chkTTOptions_SelectedValueChanged);
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(12, 128);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(55, 13);
			this.label7.TabIndex = 11;
			this.label7.Text = "Veterancy";
			// 
			// cmbTTVeteran
			// 
			this.cmbTTVeteran.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbTTVeteran.FormattingEnabled = true;
			this.cmbTTVeteran.Location = new System.Drawing.Point(127, 125);
			this.cmbTTVeteran.Name = "cmbTTVeteran";
			this.cmbTTVeteran.Size = new System.Drawing.Size(296, 21);
			this.cmbTTVeteran.TabIndex = 10;
			this.cmbTTVeteran.SelectedIndexChanged += new System.EventHandler(this.cmbTTVeteran_SelectedIndexChanged);
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(12, 103);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(36, 13);
			this.label6.TabIndex = 9;
			this.label6.Text = "Group";
			// 
			// cmbTTGroup
			// 
			this.cmbTTGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbTTGroup.FormattingEnabled = true;
			this.cmbTTGroup.Location = new System.Drawing.Point(127, 100);
			this.cmbTTGroup.Name = "cmbTTGroup";
			this.cmbTTGroup.Size = new System.Drawing.Size(296, 21);
			this.cmbTTGroup.TabIndex = 8;
			this.cmbTTGroup.SelectedIndexChanged += new System.EventHandler(this.cmbTTGroup_SelectedIndexChanged);
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(12, 77);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(110, 13);
			this.label5.TabIndex = 7;
			this.label5.Text = "Mind Control Decision";
			// 
			// cmbTTMind
			// 
			this.cmbTTMind.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbTTMind.FormattingEnabled = true;
			this.cmbTTMind.Location = new System.Drawing.Point(127, 73);
			this.cmbTTMind.Name = "cmbTTMind";
			this.cmbTTMind.Size = new System.Drawing.Size(296, 21);
			this.cmbTTMind.TabIndex = 6;
			this.cmbTTMind.SelectedIndexChanged += new System.EventHandler(this.cmbTTMind_SelectedIndexChanged);
			// 
			// cmbTTTaskForce
			// 
			this.cmbTTTaskForce.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbTTTaskForce.FormattingEnabled = true;
			this.cmbTTTaskForce.Location = new System.Drawing.Point(207, 46);
			this.cmbTTTaskForce.Name = "cmbTTTaskForce";
			this.cmbTTTaskForce.Size = new System.Drawing.Size(216, 21);
			this.cmbTTTaskForce.TabIndex = 5;
			this.cmbTTTaskForce.SelectedIndexChanged += new System.EventHandler(this.cmbTTTaskForce_SelectedIndexChanged);
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(12, 49);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(58, 13);
			this.label4.TabIndex = 4;
			this.label4.Text = "TaskForce";
			// 
			// cmbTTScript
			// 
			this.cmbTTScript.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbTTScript.FormattingEnabled = true;
			this.cmbTTScript.Location = new System.Drawing.Point(207, 19);
			this.cmbTTScript.Name = "cmbTTScript";
			this.cmbTTScript.Size = new System.Drawing.Size(216, 21);
			this.cmbTTScript.TabIndex = 3;
			this.cmbTTScript.SelectedIndexChanged += new System.EventHandler(this.cmbTTScript_SelectedIndexChanged);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(12, 23);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(34, 13);
			this.label3.TabIndex = 2;
			this.label3.Text = "Script";
			// 
			// txtTTID
			// 
			this.txtTTID.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtTTID.Location = new System.Drawing.Point(8, 414);
			this.txtTTID.Name = "txtTTID";
			this.txtTTID.ReadOnly = true;
			this.txtTTID.Size = new System.Drawing.Size(328, 21);
			this.txtTTID.TabIndex = 34;
			this.txtTTID.TabStop = false;
			// 
			// lstTT
			// 
			this.lstTT.FormattingEnabled = true;
			this.lstTT.Location = new System.Drawing.Point(8, 16);
			this.lstTT.Name = "lstTT";
			this.lstTT.Size = new System.Drawing.Size(328, 368);
			this.lstTT.TabIndex = 0;
			this.lstTT.SelectedIndexChanged += new System.EventHandler(this.lstTT_SelectedIndexChanged);
			// 
			// btnTTRemove
			// 
			this.btnTTRemove.Enabled = false;
			this.btnTTRemove.Location = new System.Drawing.Point(272, 440);
			this.btnTTRemove.Name = "btnTTRemove";
			this.btnTTRemove.Size = new System.Drawing.Size(64, 23);
			this.btnTTRemove.TabIndex = 36;
			this.btnTTRemove.Text = "Remove";
			this.btnTTRemove.UseVisualStyleBackColor = true;
			this.btnTTRemove.Click += new System.EventHandler(this.btnTTRemove_Click);
			// 
			// btnTTRename
			// 
			this.btnTTRename.Enabled = false;
			this.btnTTRename.Location = new System.Drawing.Point(272, 388);
			this.btnTTRename.Name = "btnTTRename";
			this.btnTTRename.Size = new System.Drawing.Size(64, 21);
			this.btnTTRename.TabIndex = 37;
			this.btnTTRename.Text = "Rename";
			this.btnTTRename.UseVisualStyleBackColor = true;
			this.btnTTRename.Click += new System.EventHandler(this.btnTTRename_Click);
			// 
			// btnTTNew
			// 
			this.btnTTNew.Location = new System.Drawing.Point(128, 440);
			this.btnTTNew.Name = "btnTTNew";
			this.btnTTNew.Size = new System.Drawing.Size(64, 23);
			this.btnTTNew.TabIndex = 38;
			this.btnTTNew.Text = "New";
			this.btnTTNew.UseVisualStyleBackColor = true;
			this.btnTTNew.Click += new System.EventHandler(this.btnTTNew_Click);
			// 
			// tabTriggerTypes
			// 
			this.tabTriggerTypes.Controls.Add(this.txtTrName);
			this.tabTriggerTypes.Controls.Add(this.btnTrCopy);
			this.tabTriggerTypes.Controls.Add(this.txtTrID);
			this.tabTriggerTypes.Controls.Add(this.btnTrRename);
			this.tabTriggerTypes.Controls.Add(this.btnTrRemove);
			this.tabTriggerTypes.Controls.Add(this.btnTrNew);
			this.tabTriggerTypes.Controls.Add(this.grpTr);
			this.tabTriggerTypes.Controls.Add(this.lstTr);
			this.tabTriggerTypes.Location = new System.Drawing.Point(4, 22);
			this.tabTriggerTypes.Name = "tabTriggerTypes";
			this.tabTriggerTypes.Size = new System.Drawing.Size(780, 475);
			this.tabTriggerTypes.TabIndex = 3;
			this.tabTriggerTypes.Text = "TriggerTypes";
			// 
			// txtTrName
			// 
			this.txtTrName.Location = new System.Drawing.Point(8, 388);
			this.txtTrName.Name = "txtTrName";
			this.txtTrName.Size = new System.Drawing.Size(256, 20);
			this.txtTrName.TabIndex = 15;
			// 
			// btnTrCopy
			// 
			this.btnTrCopy.Enabled = false;
			this.btnTrCopy.Location = new System.Drawing.Point(200, 440);
			this.btnTrCopy.Name = "btnTrCopy";
			this.btnTrCopy.Size = new System.Drawing.Size(64, 23);
			this.btnTrCopy.TabIndex = 20;
			this.btnTrCopy.Text = "Copy";
			this.btnTrCopy.UseVisualStyleBackColor = true;
			this.btnTrCopy.Click += new System.EventHandler(this.btnTrCopy_Click);
			// 
			// txtTrID
			// 
			this.txtTrID.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtTrID.Location = new System.Drawing.Point(8, 414);
			this.txtTrID.Name = "txtTrID";
			this.txtTrID.ReadOnly = true;
			this.txtTrID.Size = new System.Drawing.Size(328, 21);
			this.txtTrID.TabIndex = 16;
			// 
			// btnTrRename
			// 
			this.btnTrRename.Enabled = false;
			this.btnTrRename.Location = new System.Drawing.Point(272, 388);
			this.btnTrRename.Name = "btnTrRename";
			this.btnTrRename.Size = new System.Drawing.Size(64, 21);
			this.btnTrRename.TabIndex = 17;
			this.btnTrRename.Text = "Rename";
			this.btnTrRename.UseVisualStyleBackColor = true;
			this.btnTrRename.Click += new System.EventHandler(this.btnTrRename_Click);
			// 
			// btnTrRemove
			// 
			this.btnTrRemove.Enabled = false;
			this.btnTrRemove.Location = new System.Drawing.Point(272, 440);
			this.btnTrRemove.Name = "btnTrRemove";
			this.btnTrRemove.Size = new System.Drawing.Size(64, 23);
			this.btnTrRemove.TabIndex = 19;
			this.btnTrRemove.Text = "Remove";
			this.btnTrRemove.UseVisualStyleBackColor = true;
			this.btnTrRemove.Click += new System.EventHandler(this.btnTrRemove_Click);
			// 
			// btnTrNew
			// 
			this.btnTrNew.Location = new System.Drawing.Point(128, 440);
			this.btnTrNew.Name = "btnTrNew";
			this.btnTrNew.Size = new System.Drawing.Size(64, 23);
			this.btnTrNew.TabIndex = 18;
			this.btnTrNew.Text = "New";
			this.btnTrNew.UseVisualStyleBackColor = true;
			this.btnTrNew.Click += new System.EventHandler(this.btnTrNew_Click);
			// 
			// grpTr
			// 
			this.grpTr.Controls.Add(this.lblTrTeam2);
			this.grpTr.Controls.Add(this.lblTrTeam);
			this.grpTr.Controls.Add(this.label13);
			this.grpTr.Controls.Add(this.cmbTrOperator);
			this.grpTr.Controls.Add(this.chkTrOptions);
			this.grpTr.Controls.Add(this.label19);
			this.grpTr.Controls.Add(this.cmbTrTeamType2);
			this.grpTr.Controls.Add(this.label18);
			this.grpTr.Controls.Add(this.cmbTrSide);
			this.grpTr.Controls.Add(this.label17);
			this.grpTr.Controls.Add(this.label16);
			this.grpTr.Controls.Add(this.label15);
			this.grpTr.Controls.Add(this.numTrProbMax);
			this.grpTr.Controls.Add(this.numTrProbMin);
			this.grpTr.Controls.Add(this.numTrProb);
			this.grpTr.Controls.Add(this.label14);
			this.grpTr.Controls.Add(this.cmbTrTechType);
			this.grpTr.Controls.Add(this.numTrAmount);
			this.grpTr.Controls.Add(this.label12);
			this.grpTr.Controls.Add(this.cmbTrCondition);
			this.grpTr.Controls.Add(this.label11);
			this.grpTr.Controls.Add(this.numTrTechLevel);
			this.grpTr.Controls.Add(this.label8);
			this.grpTr.Controls.Add(this.label2);
			this.grpTr.Controls.Add(this.cmbTrOwner);
			this.grpTr.Controls.Add(this.cmbTrTeamType);
			this.grpTr.Enabled = false;
			this.grpTr.Location = new System.Drawing.Point(344, 16);
			this.grpTr.Name = "grpTr";
			this.grpTr.Size = new System.Drawing.Size(429, 448);
			this.grpTr.TabIndex = 13;
			this.grpTr.TabStop = false;
			this.grpTr.Text = "Settings";
			// 
			// lblTrTeam2
			// 
			this.lblTrTeam2.AutoSize = true;
			this.lblTrTeam2.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblTrTeam2.Location = new System.Drawing.Point(124, 48);
			this.lblTrTeam2.Name = "lblTrTeam2";
			this.lblTrTeam2.Size = new System.Drawing.Size(49, 15);
			this.lblTrTeam2.TabIndex = 26;
			this.lblTrTeam2.Text = "<none>";
			// 
			// lblTrTeam
			// 
			this.lblTrTeam.AutoSize = true;
			this.lblTrTeam.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblTrTeam.Location = new System.Drawing.Point(124, 21);
			this.lblTrTeam.Name = "lblTrTeam";
			this.lblTrTeam.Size = new System.Drawing.Size(49, 15);
			this.lblTrTeam.TabIndex = 25;
			this.lblTrTeam.Text = "<none>";
			// 
			// label13
			// 
			this.label13.AutoSize = true;
			this.label13.Location = new System.Drawing.Point(12, 183);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(43, 13);
			this.label13.TabIndex = 24;
			this.label13.Text = "Amount";
			// 
			// cmbTrOperator
			// 
			this.cmbTrOperator.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbTrOperator.FormattingEnabled = true;
			this.cmbTrOperator.Location = new System.Drawing.Point(127, 180);
			this.cmbTrOperator.Name = "cmbTrOperator";
			this.cmbTrOperator.Size = new System.Drawing.Size(150, 21);
			this.cmbTrOperator.TabIndex = 23;
			this.cmbTrOperator.SelectedIndexChanged += new System.EventHandler(this.cmbTrOperator_SelectedIndexChanged);
			// 
			// chkTrOptions
			// 
			this.chkTrOptions.CheckOnClick = true;
			this.chkTrOptions.FormattingEnabled = true;
			this.chkTrOptions.Location = new System.Drawing.Point(127, 312);
			this.chkTrOptions.Name = "chkTrOptions";
			this.chkTrOptions.Size = new System.Drawing.Size(296, 124);
			this.chkTrOptions.TabIndex = 22;
			this.chkTrOptions.SelectedValueChanged += new System.EventHandler(this.chkTrOptions_SelectedValueChanged);
			// 
			// label19
			// 
			this.label19.AutoSize = true;
			this.label19.Location = new System.Drawing.Point(13, 49);
			this.label19.Name = "label19";
			this.label19.Size = new System.Drawing.Size(98, 13);
			this.label19.TabIndex = 21;
			this.label19.Text = "Support TeamType";
			// 
			// cmbTrTeamType2
			// 
			this.cmbTrTeamType2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbTrTeamType2.FormattingEnabled = true;
			this.cmbTrTeamType2.Location = new System.Drawing.Point(207, 46);
			this.cmbTrTeamType2.Name = "cmbTrTeamType2";
			this.cmbTrTeamType2.Size = new System.Drawing.Size(216, 21);
			this.cmbTrTeamType2.TabIndex = 20;
			this.cmbTrTeamType2.SelectedIndexChanged += new System.EventHandler(this.cmbTrTeamType2_SelectedIndexChanged);
			// 
			// label18
			// 
			this.label18.AutoSize = true;
			this.label18.Location = new System.Drawing.Point(12, 103);
			this.label18.Name = "label18";
			this.label18.Size = new System.Drawing.Size(38, 13);
			this.label18.TabIndex = 19;
			this.label18.Text = "Owner";
			// 
			// cmbTrSide
			// 
			this.cmbTrSide.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbTrSide.FormattingEnabled = true;
			this.cmbTrSide.Location = new System.Drawing.Point(128, 73);
			this.cmbTrSide.Name = "cmbTrSide";
			this.cmbTrSide.Size = new System.Drawing.Size(296, 21);
			this.cmbTrSide.TabIndex = 18;
			this.cmbTrSide.SelectedIndexChanged += new System.EventHandler(this.cmbTrSide_SelectedIndexChanged);
			// 
			// label17
			// 
			this.label17.AutoSize = true;
			this.label17.Location = new System.Drawing.Point(12, 288);
			this.label17.Name = "label17";
			this.label17.Size = new System.Drawing.Size(102, 13);
			this.label17.TabIndex = 17;
			this.label17.Text = "Maximum Probability";
			// 
			// label16
			// 
			this.label16.AutoSize = true;
			this.label16.Location = new System.Drawing.Point(12, 262);
			this.label16.Name = "label16";
			this.label16.Size = new System.Drawing.Size(99, 13);
			this.label16.TabIndex = 16;
			this.label16.Text = "Minimum Probability";
			// 
			// label15
			// 
			this.label15.AutoSize = true;
			this.label15.Location = new System.Drawing.Point(12, 236);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(104, 13);
			this.label15.TabIndex = 15;
			this.label15.Text = "Weighted Probability";
			// 
			// numTrProbMax
			// 
			this.numTrProbMax.Location = new System.Drawing.Point(127, 286);
			this.numTrProbMax.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
			this.numTrProbMax.Name = "numTrProbMax";
			this.numTrProbMax.Size = new System.Drawing.Size(296, 20);
			this.numTrProbMax.TabIndex = 14;
			this.numTrProbMax.ValueChanged += new System.EventHandler(this.numTrProbMax_ValueChanged);
			// 
			// numTrProbMin
			// 
			this.numTrProbMin.Location = new System.Drawing.Point(127, 260);
			this.numTrProbMin.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
			this.numTrProbMin.Name = "numTrProbMin";
			this.numTrProbMin.Size = new System.Drawing.Size(296, 20);
			this.numTrProbMin.TabIndex = 13;
			this.numTrProbMin.ValueChanged += new System.EventHandler(this.numTrProbMin_ValueChanged);
			// 
			// numTrProb
			// 
			this.numTrProb.Location = new System.Drawing.Point(127, 234);
			this.numTrProb.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
			this.numTrProb.Name = "numTrProb";
			this.numTrProb.Size = new System.Drawing.Size(296, 20);
			this.numTrProb.TabIndex = 12;
			this.numTrProb.ValueChanged += new System.EventHandler(this.numTrProb_ValueChanged);
			// 
			// label14
			// 
			this.label14.AutoSize = true;
			this.label14.Location = new System.Drawing.Point(12, 210);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(59, 13);
			this.label14.TabIndex = 11;
			this.label14.Text = "Tech Type";
			// 
			// cmbTrTechType
			// 
			this.cmbTrTechType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbTrTechType.FormattingEnabled = true;
			this.cmbTrTechType.Location = new System.Drawing.Point(127, 207);
			this.cmbTrTechType.Name = "cmbTrTechType";
			this.cmbTrTechType.Size = new System.Drawing.Size(296, 21);
			this.cmbTrTechType.TabIndex = 10;
			this.cmbTrTechType.SelectedIndexChanged += new System.EventHandler(this.cmbTrTechType_SelectedIndexChanged);
			// 
			// numTrAmount
			// 
			this.numTrAmount.Location = new System.Drawing.Point(283, 181);
			this.numTrAmount.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
			this.numTrAmount.Name = "numTrAmount";
			this.numTrAmount.Size = new System.Drawing.Size(140, 20);
			this.numTrAmount.TabIndex = 8;
			this.numTrAmount.ValueChanged += new System.EventHandler(this.numTrAmount_ValueChanged);
			// 
			// label12
			// 
			this.label12.AutoSize = true;
			this.label12.Location = new System.Drawing.Point(12, 156);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(78, 13);
			this.label12.TabIndex = 7;
			this.label12.Text = "Trigger when...";
			// 
			// cmbTrCondition
			// 
			this.cmbTrCondition.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbTrCondition.FormattingEnabled = true;
			this.cmbTrCondition.Location = new System.Drawing.Point(127, 153);
			this.cmbTrCondition.Name = "cmbTrCondition";
			this.cmbTrCondition.Size = new System.Drawing.Size(296, 21);
			this.cmbTrCondition.TabIndex = 6;
			this.cmbTrCondition.SelectedIndexChanged += new System.EventHandler(this.cmbTrCondition_SelectedIndexChanged);
			// 
			// label11
			// 
			this.label11.AutoSize = true;
			this.label11.Location = new System.Drawing.Point(12, 129);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(61, 13);
			this.label11.TabIndex = 5;
			this.label11.Text = "Tech Level";
			// 
			// numTrTechLevel
			// 
			this.numTrTechLevel.Location = new System.Drawing.Point(127, 127);
			this.numTrTechLevel.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
			this.numTrTechLevel.Name = "numTrTechLevel";
			this.numTrTechLevel.Size = new System.Drawing.Size(296, 20);
			this.numTrTechLevel.TabIndex = 4;
			this.numTrTechLevel.ValueChanged += new System.EventHandler(this.numTrTechLevel_ValueChanged);
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(12, 76);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(28, 13);
			this.label8.TabIndex = 3;
			this.label8.Text = "Side";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(12, 24);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(58, 13);
			this.label2.TabIndex = 2;
			this.label2.Text = "TeamType";
			// 
			// cmbTrOwner
			// 
			this.cmbTrOwner.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbTrOwner.FormattingEnabled = true;
			this.cmbTrOwner.Location = new System.Drawing.Point(127, 100);
			this.cmbTrOwner.Name = "cmbTrOwner";
			this.cmbTrOwner.Size = new System.Drawing.Size(296, 21);
			this.cmbTrOwner.TabIndex = 1;
			this.cmbTrOwner.SelectedIndexChanged += new System.EventHandler(this.cmbTrOwner_SelectedIndexChanged);
			// 
			// cmbTrTeamType
			// 
			this.cmbTrTeamType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbTrTeamType.FormattingEnabled = true;
			this.cmbTrTeamType.Location = new System.Drawing.Point(207, 19);
			this.cmbTrTeamType.Name = "cmbTrTeamType";
			this.cmbTrTeamType.Size = new System.Drawing.Size(216, 21);
			this.cmbTrTeamType.TabIndex = 0;
			this.cmbTrTeamType.SelectedIndexChanged += new System.EventHandler(this.cmbTrTeamType_SelectedIndexChanged);
			// 
			// lstTr
			// 
			this.lstTr.FormattingEnabled = true;
			this.lstTr.Location = new System.Drawing.Point(8, 16);
			this.lstTr.Name = "lstTr";
			this.lstTr.Size = new System.Drawing.Size(328, 368);
			this.lstTr.TabIndex = 0;
			this.lstTr.SelectedIndexChanged += new System.EventHandler(this.lstTr_SelectedIndexChanged);
			// 
			// mainMenu1
			// 
			this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuFile,
            this.mnuHelp});
			// 
			// mnuFile
			// 
			this.mnuFile.Index = 0;
			this.mnuFile.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuLoadRules,
            this.mnuLoadAI,
            this.menuItem1,
            this.mnuSaveAI,
            this.menuItem4,
            this.mnuClear,
            this.menuItem3,
            this.mnuExit});
			this.mnuFile.Text = "&File";
			// 
			// mnuLoadRules
			// 
			this.mnuLoadRules.Index = 0;
			this.mnuLoadRules.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuLoadRulesRA2,
            this.mnuLoadRulesTS});
			this.mnuLoadRules.Text = "Load &Rules";
			// 
			// mnuLoadAI
			// 
			this.mnuLoadAI.Enabled = false;
			this.mnuLoadAI.Index = 1;
			this.mnuLoadAI.Shortcut = System.Windows.Forms.Shortcut.CtrlA;
			this.mnuLoadAI.Text = "Load &AI";
			this.mnuLoadAI.Click += new System.EventHandler(this.mnuLoadAI_Click);
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 2;
			this.menuItem1.Text = "-";
			// 
			// mnuSaveAI
			// 
			this.mnuSaveAI.Index = 3;
			this.mnuSaveAI.Shortcut = System.Windows.Forms.Shortcut.CtrlS;
			this.mnuSaveAI.Text = "&Save AI";
			this.mnuSaveAI.Click += new System.EventHandler(this.mnuSaveAI_Click);
			// 
			// menuItem4
			// 
			this.menuItem4.Index = 4;
			this.menuItem4.Text = "-";
			// 
			// mnuClear
			// 
			this.mnuClear.Index = 5;
			this.mnuClear.Text = "&Clear all data";
			this.mnuClear.Click += new System.EventHandler(this.mnuClear_Click);
			// 
			// menuItem3
			// 
			this.menuItem3.Index = 6;
			this.menuItem3.Text = "-";
			// 
			// mnuExit
			// 
			this.mnuExit.Index = 7;
			this.mnuExit.Text = "E&xit";
			this.mnuExit.Click += new System.EventHandler(this.mnuExit_Click);
			// 
			// mnuHelp
			// 
			this.mnuHelp.Index = 1;
			this.mnuHelp.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuHelp2,
            this.menuItem2,
            this.mnuInfo});
			this.mnuHelp.Text = "&Help";
			// 
			// mnuHelp2
			// 
			this.mnuHelp2.Index = 0;
			this.mnuHelp2.Shortcut = System.Windows.Forms.Shortcut.F1;
			this.mnuHelp2.Text = "&Help";
			this.mnuHelp2.Click += new System.EventHandler(this.mnuHelp2_Click);
			// 
			// menuItem2
			// 
			this.menuItem2.Index = 1;
			this.menuItem2.Text = "-";
			// 
			// mnuInfo
			// 
			this.mnuInfo.Index = 2;
			this.mnuInfo.Text = "&About";
			this.mnuInfo.Click += new System.EventHandler(this.mnuInfo_Click);
			// 
			// panelStatus
			// 
			this.panelStatus.Name = "panelStatus";
			this.panelStatus.Text = "Status";
			// 
			// openFileDialog1
			// 
			this.openFileDialog1.FileName = "openFileDialog1";
			// 
			// mnuLoadRulesRA2
			// 
			this.mnuLoadRulesRA2.Index = 0;
			this.mnuLoadRulesRA2.Shortcut = System.Windows.Forms.Shortcut.CtrlR;
			this.mnuLoadRulesRA2.Text = "Red Alert 2";
			this.mnuLoadRulesRA2.Click += new System.EventHandler(this.mnuLoadRulesRA2_Click);
			// 
			// mnuLoadRulesTS
			// 
			this.mnuLoadRulesTS.Index = 1;
			this.mnuLoadRulesTS.Shortcut = System.Windows.Forms.Shortcut.CtrlT;
			this.mnuLoadRulesTS.Text = "Tiberium Sun";
			this.mnuLoadRulesTS.Click += new System.EventHandler(this.mnuLoadRulesTS_Click);
			// 
			// frmMain
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(788, 501);
			this.Controls.Add(this.tabControl1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
			this.MaximizeBox = false;
			this.Menu = this.mainMenu1;
			this.Name = "frmMain";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "AIEdit";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
			this.tabControl1.ResumeLayout(false);
			this.tabTaskForces.ResumeLayout(false);
			this.tabTaskForces.PerformLayout();
			this.grpTF.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.numTFCount)).EndInit();
			this.tabScriptTypes.ResumeLayout(false);
			this.tabScriptTypes.PerformLayout();
			this.grpST.ResumeLayout(false);
			this.grpST.PerformLayout();
			this.tabTeamTypes.ResumeLayout(false);
			this.tabTeamTypes.PerformLayout();
			this.grpTT.ResumeLayout(false);
			this.grpTT.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.numTTTechLevel)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numTTMax)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numTTPriority)).EndInit();
			this.tabTriggerTypes.ResumeLayout(false);
			this.tabTriggerTypes.PerformLayout();
			this.grpTr.ResumeLayout(false);
			this.grpTr.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.numTrProbMax)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numTrProbMin)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numTrProb)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numTrAmount)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numTrTechLevel)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.panelStatus)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new frmMain());
		}

        private uint GetLastID(string file)
        {
            StreamReader stream = new StreamReader(file);
            IniParser ip = new IniParser(stream);
            uint n = ID_BASE;

            while (ip.Next())
            {
                if (ip.Section.CompareTo("AIEdit") != 0) ip.Skip();
                else
                {
                    ip.Parse();
                    n = uint.Parse((string)ip.Table["Index"]) & ID_MAX;
                    break;
                }
            }

            stream.Close();
            return n;
        }

        private uint NextId()
        {
            return ++id_counter;
        }



        private void LoadStrings(string string_file)
		{
			rules = new Rules();
            taskforces = new TFTable();
            scripttypes = new STTable();
            teamtypes = new TTTable();
            triggertypes = new TrTable();
            id_counter = ID_BASE;

            settings = new StringTable();

            if (!settings.Load(string_file))
            {
                MessageBox.Show("Failed to open configuration file!", "Error",
					MessageBoxButtons.OK, MessageBoxIcon.Error);
                mnuLoadRules.Enabled = false;
                mnuSaveAI.Enabled = false;
                Application.Exit();
                return;
            }

            // Add scripttype action types.
			cmbSTAction.Items.Clear();
            foreach (ScriptActionType sat in settings.ActionTypes) cmbSTAction.Items.Add(sat.Name);
            cmbSTAction.SelectedIndex = 0;

            // TaskForce and TeamType groups.
			cmbTFGroup.Items.Clear();
			cmbTTGroup.Items.Clear();
            foreach (string s in settings.Group.ValuesB)
            {
                cmbTFGroup.Items.Add(s);
                cmbTTGroup.Items.Add(s);
            }

			cmbTFGroup.SelectedIndex = 0;
            cmbTTGroup.SelectedIndex = 0;
			
            // ScriptType offsets.
			cmbSTAOffset.Items.Clear();
            foreach (string s in settings["Offsets"]) cmbSTAOffset.Items.Add(s);
            cmbSTAOffset.SelectedIndex = 0;

            // Mind Control Decision
			cmbTTMind.Items.Clear();
            foreach (string s in settings["MCDecisions"]) cmbTTMind.Items.Add(s);
            cmbTTMind.SelectedIndex = 0;
            
            // Veteran Level
			cmbTTVeteran.Items.Clear();
            foreach (string s in settings["VeteranLevels"]) cmbTTVeteran.Items.Add(s);
            cmbTTVeteran.SelectedIndex = 1;

            // TeamType options.
			chkTTOptions.Items.Clear();
            foreach (string s in settings["TeamTypeOptions"]) chkTTOptions.Items.Add(s);

            // TriggerType options.
			chkTrOptions.Items.Clear();
            foreach (string s in settings["TriggerTypeOptions"]) chkTrOptions.Items.Add(s);
            
            // TriggerType sides.
			cmbTrSide.Items.Clear();
            foreach (string s in settings["Sides"]) cmbTrSide.Items.Add(s);

            // TriggerType conditions.
			//cmbTrCondition.Items.Clear();
            foreach (string s in settings["Conditions"]) cmbTrCondition.Items.Add(s);

            // TriggerType operators.
			cmbTrOperator.Items.Clear();
            foreach (string s in settings["Operators"]) cmbTrOperator.Items.Add(s);
		}

        /// <summary>
        /// Add information about the selected unit to the listbox.
        /// </summary>
        /// <param name="lst"></param>
        /// <param name="tt"></param>
		private void AddTechnoInfo(ListBox lst, TechnoType tt)
		{
			lst.Items.Clear();
            lstTFInfo.Items.Add("ID: " + tt.ID);
			lstTFInfo.Items.Add("Owner: " + tt.Owner);
			lstTFInfo.Items.Add("Cost: " + tt.Cost);
		}

        /// <summary>
        /// Load the taskforce properties in the gui.
        /// </summary>
        /// <param name="index"></param>
        private void ShowTF(int index, int selected)
        {
            // Unselect.
            if (index == -1)
            {
                txtTFID.Text = "";
                txtTFName.Text = "";
                cmbTFGroup.SelectedIndex = 0;
                lstTFUnits.Items.Clear();
                return;
            }

            selectedtf = (TaskForce)taskforces.Table[index];

            txtTFID.Text = selectedtf.ID;
            txtTFName.Text = selectedtf.Name;
            cmbTFGroup.Text = (string)settings.Group[selectedtf.Group];

            // Load units.
            lstTFUnits.Items.Clear();
            foreach (TaskForce.TFUnit unit in selectedtf.Units)
            {
                TechnoType tt = rules.GetByID(unit.ID);
                string[] a = { tt.Name, unit.Count.ToString() };
                lstTFUnits.Items.Add(new ListViewItem(a));
            }

            if (lstTFUnits.Items.Count == 0) return;
            if (selected >= lstTFUnits.Items.Count) selected = lstTFUnits.Items.Count - 1;
            lstTFUnits.Items[selected].Selected = true;
        }

        /// <summary>
        /// Show a ScriptType in the gui.
        /// </summary>
        /// <param name="index">Index from the ScriptTypes table.</param>
        /// <param name="selected">The selected action in the UI.</param>
        private void ShowST(int index, int selected)
        {
            // Unselect.
            if (index == -1)
            {
                txtSTID.Text = "";
                txtSTName.Text = "";
                lstSTActions.Items.Clear();
                return;
            }

            selectedst = (ScriptType)scripttypes.Table[index];
            
            txtSTName.Text = selectedst.Name;
            txtSTID.Text = selectedst.ID;

            // Show actions.
            lstSTActions.Items.Clear();
            foreach (ScriptType.ScriptAction sa in selectedst.Actions)
            {
                InsertSTAction(sa.Action, sa.Param, lstSTActions.Items.Count);
            }

            if (lstSTActions.Items.Count == 0) return;
            if (selected >= lstSTActions.Items.Count) selected = lstSTActions.Items.Count - 1;
            lstSTActions.Items[selected].Selected = true;
        }

        /// <summary>
        /// Show the taskforce settings in the gui
        /// </summary>
        /// <param name="index"></param>
        private void ShowTT(int index)
        {
            int n = 0;
            ScriptType st;
            TaskForce tf;
            if (index < 0) return;

            selectedtt = (TeamType)teamtypes.Table[index];

            txtTTName.Text = selectedtt.Name;
            txtTTID.Text = selectedtt.ID;
            
            //lblTTScript.Text = "<none>";
            //lblTTTaskForce.Text = "<none>";

            // teamtype scripts list
            st = scripttypes.GetByID(selectedtt.ScriptType);
            if (st == null) cmbTTScript.Text = "<none>";
            else cmbTTScript.Text = st.Name;

            // teamtype taskforces list
            tf = taskforces.GetByID(selectedtt.TaskForce);
            if (tf == null) cmbTTTaskForce.Text = "<none>";
            else cmbTTTaskForce.Text = tf.Name;

            cmbTTMind.SelectedIndex = selectedtt.MCDecision;
            cmbTTGroup.Text = settings.Group[selectedtt.Group];
			cmbTTHouse.SelectedItem = selectedtt.House;
            cmbTTVeteran.SelectedIndex = selectedtt.VeteranLevel - 1;
            numTTPriority.Value = (decimal)selectedtt.Priority;
            numTTMax.Value = (decimal)selectedtt.Max;
            numTTTechLevel.Value = (decimal)selectedtt.TechLevel;

            foreach(bool b in selectedtt.Settings)
            {
                chkTTOptions.SetItemChecked(n++, b);
            }
        }

        /// <summary>
        /// Show TriggerType.
        /// </summary>
        /// <param name="index"></param>
        private void ShowTr(int index)
        {
            TeamType tt;
            TechnoType techno;
            int i = 0;

            if (index == -1) return;
            selectedtr = (TriggerType)triggertypes.Table[index];

            // name/id
            txtTrName.Text = selectedtr.Name;
            txtTrID.Text = selectedtr.ID;

            // teamtype
            tt = (TeamType)teamtypes.Table[selectedtr.TeamType];
            if (tt == null) cmbTrTeamType.SelectedIndex = 0;
            else cmbTrTeamType.SelectedItem = tt.Name;
            
            // support teamtype
            tt = (TeamType)teamtypes.Table[selectedtr.TeamType2];
            if (tt == null) cmbTrTeamType2.SelectedIndex = 0;
            else cmbTrTeamType2.SelectedItem = tt.Name;

            // owner
            cmbTrOwner.SelectedItem = selectedtr.Owner;

            // side
            cmbTrSide.SelectedIndex = selectedtr.Side;

            // techlevel
            numTrTechLevel.Value = (decimal)selectedtr.TechLevel;

            // condition
            cmbTrCondition.SelectedIndex = selectedtr.Condition;

            // tech type
            techno = rules.GetByID(selectedtr.TechType);
            if (techno == null && cmbTrTechType.Items.Count > 0) cmbTrTechType.SelectedIndex = 0;
            else cmbTrTechType.SelectedItem = techno.Name;

            // operator
            cmbTrOperator.SelectedIndex = selectedtr.Operator;

            // amount
            numTrAmount.Value = (decimal)selectedtr.Amount;

            // probabilities
            numTrProb.Value = (decimal)selectedtr.Prob;
            numTrProbMin.Value = (decimal)selectedtr.ProbMin;
            numTrProbMax.Value = (decimal)selectedtr.ProbMax;

            // options
            foreach (bool b in selectedtr.Options) chkTrOptions.SetItemChecked(i++, b);
        }

        /// <summary>
        /// Insert a ScriptType action in the current ScriptType.
        /// </summary>
        /// <param name="action">The action index.</param>
        /// <param name="param">The action parameter.</param>
        /// <param name="index">The index at which the action must be inserted.</param>
        private void InsertSTAction(int action, int param, int index)
        {
            ScriptActionType sat = (ScriptActionType)settings.ActionTypes[action];

            string sparam = (sat.List == null || sat.List.Count == 0) ? param.ToString() : (string)sat.List[param];

            string[] subitems = { action.ToString(), param.ToString(), index.ToString(), sat.Name, sparam };
            
            lstSTActions.Items.Insert(index, new ListViewItem(subitems));
            lstSTActions.Items[index].Selected = true;

            // update the action indices.
            for (int i = index; i < lstSTActions.Items.Count; i++)
            {
                lstSTActions.Items[i].SubItems[2].Text = i.ToString();
            }
        }

        /// <summary>
        /// Reload the ScriptType, TaskForces and TriggerTypes listboxes and comboboxes.
        /// </summary>
        private void ReloadLists()
        {
            // Load scripttype lists
            settings["ScriptTypes"].Clear();
            cmbTTScript.Items.Clear();
            cmbTTScript.Items.Add("<none>");
            foreach (ScriptType st in scripttypes.Table.Values)
            {
                cmbTTScript.Items.Add(st.Name);
            }

            // Load taskforce lists.
            cmbTTTaskForce.Items.Clear();
            cmbTTTaskForce.Items.Add("<none>");
            foreach (TaskForce tf in taskforces.Table.Values)
            {
                cmbTTTaskForce.Items.Add(tf.Name);
            }

            // Load triggertype lists.
            cmbTrTeamType.Items.Clear();
            cmbTrTeamType2.Items.Clear();
            cmbTrTeamType.Items.Add("<none>");
            cmbTrTeamType2.Items.Add("<none>");
            settings["TeamTypes"].Clear();
            foreach (TeamType tt in teamtypes.Table.Values)
            {
                cmbTrTeamType.Items.Add(tt.Name);
                cmbTrTeamType2.Items.Add(tt.Name);
                settings["TeamTypes"].Add(tt.Name);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="lst"></param>
        /// <param name="table"></param>
        /// <returns></returns>
        private void RemoveAIObject<T>(ListBox lst, AITable<T> table) where T : IAIObject, new()
        {
            int index = lst.SelectedIndex;
            if (index == -1) return;
            T t = (T)table.Table[index];
            table.Remove(t.ID);
            lst.Items.RemoveAt(index);
            if (index >= lst.Items.Count) index--;
            if(lst.Items.Count > 0) lst.SelectedIndex = index;
        }

        /// <summary>
        /// Load AI.ini (AFTER LoadRules).
        /// </summary>
        /// <param name="file"></param>
        private void LoadAI(string file)
        {
            id_counter = GetLastID(file);

            taskforces.Load(file);
			lstTF.Items.Clear();
            lstTF.Items.AddRange(taskforces.GetNames(false));

            scripttypes.Load(file);
			lstST.Items.Clear();
            lstST.Items.AddRange(scripttypes.GetNames(false));

            teamtypes.Load(file);
			lstTT.Items.Clear();
            lstTT.Items.AddRange(teamtypes.GetNames(false));

            triggertypes.Load(file);
			lstTr.Items.Clear();
            lstTr.Items.AddRange(triggertypes.GetNames());

            cmbTrTechType.Items.Clear();
            cmbTrTechType.Items.Add("<none>");
            cmbTrTechType.Items.AddRange(rules.GetNames(Rules.Types.All, true));

            ai_opened = true;
        }

        /// <summary>
        /// Load rules.ini (BEFORE LoadAI).
        /// </summary>
        /// <param name="file"></param>
        private void LoadRules(string file)
        {
            rules.Load(openFileDialog1.FileName);

            settings["BuildingTypes"].AddRange(rules.GetNames(Rules.Types.Buildings, false));
            settings["Houses"] = rules.Houses;

            if (settings["BuildingTypes"].Count == 0)
            {
                MessageBox.Show("[BuildingTypes] is empty. Make sure you've opened the right file.", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            id_counter = ID_BASE;
        }

        /// <summary>
        /// Save AI.ini
        /// </summary>
        /// <param name="file"></param>
        private void SaveAI(string file)
        {
            StreamWriter stream = new StreamWriter(file, false, System.Text.Encoding.ASCII);

            stream.WriteLine("; File created with AIEdit.");
            stream.WriteLine("[AIEdit]");
            stream.WriteLine("Index=" + id_counter.ToString());
            stream.WriteLine();
            taskforces.Write(stream);
            //stream.WriteLine();
            scripttypes.Write(stream);
            //stream.WriteLine();
            teamtypes.Write(stream);
            //stream.WriteLine();
            triggertypes.Write(stream);
            stream.WriteLine();
            stream.WriteLine("[Digest]");
            stream.WriteLine("1=m+F1aOs8y7FvcCJjyEsh2JEPw4I=");

            stream.Close();

            this.Text = "AIEdit - " + file;
        }

        /// <summary>
        /// Reset all data and the UI.
        /// </summary>
        private void ResetAll()
        {
            // Task force.
            lstTF.Items.Clear();
            lstTFUnits.Items.Clear();
            cmbTFUnits.Items.Clear();
            grpTF.Enabled = false;
            cmbTFGroup.Enabled = false;
            btnTFNew.Enabled = false;
            btnTFCopy.Enabled = false;
            btnTFRename.Enabled = false;
            btnTFRemove.Enabled = false;
            btnTFNew.Enabled = true;

            // Script types.
            lstST.Items.Clear();
            lstSTActions.Items.Clear();
            grpST.Enabled = false;
            btnSTNew.Enabled = false;
            btnSTCopy.Enabled = false;
            btnSTRename.Enabled = false;
            btnSTRemove.Enabled = false;
            btnSTNew.Enabled = true;

            // Team types.
            lstTT.Items.Clear();
            cmbTTScript.Items.Clear();
            cmbTTTaskForce.Items.Clear();
            grpTT.Enabled = false;
            btnTTNew.Enabled = false;
            btnTTCopy.Enabled = false;
            btnTTRename.Enabled = false;
            btnTTRemove.Enabled = false;
            btnTTNew.Enabled = true;

            // Trigger types.
            lstTr.Items.Clear();
            cmbTrTeamType.Items.Clear();
            cmbTrTeamType2.Items.Clear();
            grpTr.Enabled = false;
            btnTrNew.Enabled = false;
            btnTrCopy.Enabled = false;
            btnTrRename.Enabled = false;
            btnTrRemove.Enabled = false;
            btnTrNew.Enabled = true;

            // Clear the string tables.
            settings["ScriptTypes"].Clear();
            settings["BuildingTypes"].Clear();
            settings["TeamTypes"].Clear();

            // Clear the rules and types.
            rules = new Rules();
            taskforces = new TFTable();
            scripttypes = new STTable();
            teamtypes = new TTTable();
            triggertypes = new TrTable();

            // Reset the menu
            mnuLoadRules.Enabled = true;
            mnuLoadAI.Enabled = false;
            mnuSaveAI.Enabled = false;
            tabControl1.Enabled = false;

            this.Text = "AIEdit";

            ai_opened = false;
        }


        /***********************************************************************
         * 
         * UI events.
         * 
         **********************************************************************/

		private void mnuExit_Click(object sender, System.EventArgs e)
		{
            Application.Exit();
		}

		private void cmbTFUnits_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			TechnoType tt = rules.GetByName(cmbTFUnits.Text);
			AddTechnoInfo(lstTFInfo, tt);
		}

		private void btnTFAdd_Click(object sender, System.EventArgs e)
		{
			if(numTFCount.Value == 0 || cmbTFUnits.Text.Length == 0) return;

            TechnoType type = rules.GetByName(cmbTFUnits.Text);
            int count = (int)numTFCount.Value;
            if( selectedtf.AddUnit(new TaskForce.TFUnit(type.ID, count)) )
            {
                ShowTF(lstTF.SelectedIndex, lstTFUnits.Items.Count);
            }
		}

		private void lstTFUnits_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			ListView.SelectedListViewItemCollection selected = lstTFUnits.SelectedItems;

			foreach(ListViewItem item in selected)
			{
				TechnoType tt = rules.GetByName( item.SubItems[0].Text );
				cmbTFUnits.Text = item.SubItems[0].Text;
				numTFCount.Value = int.Parse(item.SubItems[1].Text);
				AddTechnoInfo(lstTFInfo, tt);
			}
		}

		private void btnTFModify_Click(object sender, System.EventArgs e)
		{
            ListView.SelectedListViewItemCollection selected = lstTFUnits.SelectedItems;
            TechnoType newtype = rules.GetByName(cmbTFUnits.Text);
            int count = (int)numTFCount.Value;

            foreach (ListViewItem item in selected)
            {
                TechnoType seltype = rules.GetByName(item.SubItems[0].Text);
                TaskForce.TFUnit newunit = new TaskForce.TFUnit(newtype.ID, count);

                if ( selectedtf.ModifyUnit(seltype.ID, newunit) )
                {
                    ShowTF(lstTF.SelectedIndex, item.Index);
                }
            }
		}

        private void btnTFRemove_Click(object sender, System.EventArgs e)
        {
            ListView.SelectedListViewItemCollection selected = lstTFUnits.SelectedItems;

            foreach (ListViewItem item in selected)
            {
                TechnoType type = rules.GetByName(item.SubItems[0].Text);
                if (selectedtf.RemoveUnit(type.ID))
                {
                    ShowTF(lstTF.SelectedIndex, item.Index);
                }
            }
        }

        private void lstTF_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstTF.SelectedIndex == -1)
            {
                grpTF.Enabled = false;
                cmbTFGroup.Enabled = false;
                btnTFRemove.Enabled = false;
                btnTFRename.Enabled = false;
                btnTFCopy.Enabled = false;
                return;
            }
            cmbTFGroup.Enabled = true;
            btnTFRemove.Enabled = true;
            btnTFRename.Enabled = true;
            btnTFCopy.Enabled = true;
            grpTF.Enabled = true;
            ShowTF(lstTF.SelectedIndex, 0);
        }

        private void btnTFNew_Click(object sender, EventArgs e)
        {
            InputBox.InputResult res = InputBox.Show("Add TaskForce", "Enter the name of the TaskForce:");
            if (res.ReturnCode == DialogResult.Cancel) return;

            if (taskforces.GetByName(res.Text) == null)
            {
                TaskForce tf = taskforces.NewTF(NextId(), res.Text);
                lstTF.SelectedIndex = lstTF.Items.Add(tf.Name);
                ReloadLists();
            }
            else
            {
                MessageBox.Show("Name already exists.", "Rename", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbTFGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (selectedtf != null) selectedtf.Group = settings.Group[cmbTFGroup.Text];
        }

        private void mnuSaveAI_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "AI (ai*.ini)|ai*.ini";
            if (saveFileDialog1.ShowDialog() != DialogResult.OK) return;

            SaveAI(saveFileDialog1.FileName);
        }

        private void LoadRulesStrings(string string_file)
		{
			openFileDialog1.Filter = "Rules (rules*.ini)|rules*.ini";
            openFileDialog1.FileName = "";

            if (openFileDialog1.ShowDialog() != DialogResult.OK) return;

			LoadStrings(string_file);
            LoadRules(openFileDialog1.FileName);

            cmbTFUnits.Items.Clear();
            cmbTFUnits.Items.AddRange(rules.GetNames(Rules.Types.Units, true));

			cmbTTHouse.Items.Clear();
			cmbTrOwner.Items.Clear();
			cmbTTHouse.Items.Add("<none>");
			cmbTrOwner.Items.Add("<all>");

            foreach (string s in rules.Houses)
			{
				cmbTTHouse.Items.Add(s); // teamtype houses
				cmbTrOwner.Items.Add(s); // triggertype owner
			}

            mnuLoadAI.Enabled = true;
            mnuLoadRules.Enabled = false;
            tabControl1.Enabled = true;
		}
        
        private void mnuLoadRules_Click(object sender, EventArgs e)
        {
        }

        private void mnuLoadAI_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "AI (ai*.ini)|ai*.ini";
            openFileDialog1.FileName = "";
            if (openFileDialog1.ShowDialog() != DialogResult.OK) return;

            LoadAI(openFileDialog1.FileName);
            ReloadLists();

            mnuLoadAI.Enabled = false;
            mnuSaveAI.Enabled = true;

            this.Text = "AIEdit - " + openFileDialog1.FileName;
        }

        private void lstST_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstST.SelectedIndex == -1)
            {
                grpST.Enabled = false;
                btnSTRemove.Enabled = false;
                btnSTRename.Enabled = false;
                btnSTCopy.Enabled = false;
                return;
            }
            btnSTRemove.Enabled = true;
            btnSTRename.Enabled = true;
            btnSTCopy.Enabled = true;
            grpST.Enabled = true;

            ShowST(lstST.SelectedIndex, 0);
        }

        private void cmbSTAction_SelectedIndexChanged(object sender, EventArgs e)
        {
            ScriptActionType sat = (ScriptActionType)settings.ActionTypes[cmbSTAction.SelectedIndex];
            ArrayList list = sat.List;

            cmbSTParam.Items.Clear();
            cmbSTParam.DropDownStyle = sat.Style;
            txtSTDesc.Text = sat.Description;

            if (list == null) return;
            foreach (string s in list) cmbSTParam.Items.Add(s);

            if (cmbSTParam.Items.Count == 0) return;
            cmbSTParam.SelectedIndex = 0;
        }

        private void btnTFSetName_Click(object sender, EventArgs e)
        {
            string newname = txtTFName.Text;
            if (taskforces.GetByName(newname) == null)
            {
                taskforces.UpdateName(selectedtf, newname);
                int index = lstTF.SelectedIndex;
                lstTF.Items.RemoveAt(index);
                lstTF.Items.Insert(index, newname);
                lstTF.SelectedIndex = index;
                ReloadLists();
            }
            else
            {
                MessageBox.Show("Name already exists.", "Rename", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lstSTActions_SelectedIndexChanged(object sender, EventArgs e)
        {
            int action, index;

            foreach (ListViewItem item in lstSTActions.SelectedItems)
            {
                action = int.Parse(item.SubItems[0].Text);
                index = int.Parse(item.SubItems[2].Text);

                cmbSTAction.SelectedIndex = action;

                ScriptActionType sat = (ScriptActionType)settings.ActionTypes[action];

                // Show the correct param item if the actiontype has a list.
                if ( sat.List == null || sat.List.Count == 0 )
                {
                    cmbSTParam.Text = item.SubItems[1].Text;
                }
                // Or just show the param number.
                else
                {
                    cmbSTParam.SelectedIndex = int.Parse(item.SubItems[1].Text);
                }

                cmbSTAOffset.SelectedIndex = ((ScriptType.ScriptAction)selectedst.Actions[index]).Offset;
            }
        }

        private void btnSTAdd_Click(object sender, EventArgs e)
        {
            int action = cmbSTAction.SelectedIndex;
            int offset = int.Parse(cmbSTAOffset.Text.Split(new char[]{' '}, 2)[0]);
            int param;

            if (((ScriptActionType)settings.ActionTypes[action]).List == null)
            {
                param = int.Parse(cmbSTParam.Text);
            }
            else
            {
                param = cmbSTParam.SelectedIndex;
            }

            selectedst.AddAction(action, param + offset);
            InsertSTAction(action, param, lstSTActions.Items.Count);

            ReloadLists();
        }

        private void btnSTModify_Click(object sender, EventArgs e)
        {
            int offset = int.Parse(cmbSTAOffset.Text.Split(new char[]{' '}, 2)[0]);
            int action, param;

            foreach (int index in lstSTActions.SelectedIndices)
            {
                ListViewItem item = lstSTActions.Items[index];
                
                action = cmbSTAction.SelectedIndex;
                if (((ScriptActionType)settings.ActionTypes[action]).List == null)
                {
                    try { param = int.Parse(cmbSTParam.Text); }
                    catch { param = 0; }
                }
                else
                {
                    param = cmbSTParam.SelectedIndex;
                }

                ScriptType.ScriptAction sa = (ScriptType.ScriptAction)selectedst.Actions[index];
                sa.Action = action;
                sa.Param = param + offset;

                lstSTActions.Items.RemoveAt(index);
                InsertSTAction(action, param, index);
            }

            ReloadLists();
        }

        private void btnSTRemove_Click(object sender, EventArgs e)
        {
            int n = 0;

            foreach (int index in lstSTActions.SelectedIndices)
            {
                selectedst.Actions.RemoveAt(index);
                n = index;
            }

            ShowST(lstST.SelectedIndex, n);

            ReloadLists();
        }

        private void btnSTSetName_Click(object sender, EventArgs e)
        {
            string newname = txtSTName.Text;
            if (scripttypes.GetByName(newname) == null)
            {
                scripttypes.UpdateName(selectedst, newname);
                int index = lstST.SelectedIndex;
                lstST.Items.RemoveAt(index);
                lstST.Items.Insert(index, newname);
                lstST.SelectedIndex = index;
                ReloadLists();
            }
            else
            {
                MessageBox.Show("Name already exists.", "Rename", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSTNew_Click(object sender, EventArgs e)
        {
            InputBox.InputResult res = InputBox.Show("Add ScriptType", "Enter name:");
            if (res.ReturnCode == DialogResult.Cancel) return;

            if (scripttypes.GetByName(res.Text) == null)
            {
                ScriptType st = scripttypes.NewST(NextId(), res.Text);
                lstST.SelectedIndex = lstST.Items.Add(st.Name);
                ReloadLists();
            }
            else
            {
                MessageBox.Show("Name already exists.", "Rename", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSTRemove_Click_1(object sender, EventArgs e)
        {
            RemoveAIObject<ScriptType>(lstST, scripttypes);
            ReloadLists();
        }

        private void btnTFRemove_Click_1(object sender, EventArgs e)
        {
            RemoveAIObject<TaskForce>(lstTF, taskforces);
            ReloadLists();
        }

        private void lstTT_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstTT.SelectedIndex == -1)
            {
                btnTTRemove.Enabled = false;
                btnTTRename.Enabled = false;
                btnTTCopy.Enabled = false;
                grpTT.Enabled = false;
            }
            else
            {
                btnTTRemove.Enabled = true;
                btnTTRename.Enabled = true;
                btnTTCopy.Enabled = true;
                grpTT.Enabled = true;
                ShowTT(lstTT.SelectedIndex);
            }
        }

        private void cmbSTAOffset_SelectedIndexChanged(object sender, EventArgs e)
        {
            ScriptType.ScriptAction sa;

            foreach (int index in lstSTActions.SelectedIndices)
            {
                sa = (ScriptType.ScriptAction)selectedst.Actions[index];
                sa.Offset = cmbSTAOffset.SelectedIndex;
            }
        }

        private void cmbTTScript_SelectedIndexChanged(object sender, EventArgs e)
        {
            ScriptType st = scripttypes.GetByName((string)cmbTTScript.SelectedItem);
            if (st == null) selectedtt.ScriptType = "<none>";
            else selectedtt.ScriptType = st.ID;
            lblTTScript.Text = selectedtt.ScriptType;
        }

        private void cmbTTTaskForce_SelectedIndexChanged(object sender, EventArgs e)
        {
            TaskForce tf = taskforces.GetByName((string)cmbTTTaskForce.SelectedItem);
            if (tf == null) selectedtt.TaskForce = "<none>";
            else selectedtt.TaskForce = tf.ID;
            lblTTTaskForce.Text = selectedtt.TaskForce;
        }

        private void cmbTTMind_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (selectedtt == null) return;
            selectedtt.MCDecision = cmbTTMind.SelectedIndex;
        }

        private void cmbTTGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (selectedtt == null) return;
            selectedtt.Group = settings.Group[(string)cmbTTGroup.SelectedItem];
        }

        private void cmbTTVeteran_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (selectedtt == null) return;
            selectedtt.VeteranLevel = cmbTTVeteran.SelectedIndex + 1;
        }

        private void numTTPriority_ValueChanged(object sender, EventArgs e)
        {
            if (selectedtt == null) return;
            selectedtt.Priority = (int)numTTPriority.Value;
        }

        private void numTTMax_ValueChanged(object sender, EventArgs e)
        {
            if (selectedtt == null) return;
            selectedtt.Max = (int)numTTMax.Value;
        }

        private void numTTTechLevel_ValueChanged(object sender, EventArgs e)
        {
            if (selectedtt == null) return;
            selectedtt.TechLevel = (int)numTTTechLevel.Value;
        }

        private void chkTTOptions_SelectedValueChanged(object sender, EventArgs e)
        {
            if (selectedtt == null) return;
            int index = chkTTOptions.SelectedIndex;
            selectedtt.Settings[index] = chkTTOptions.GetItemChecked(index);
        }

        private void btnTTNew_Click(object sender, EventArgs e)
        {
            InputBox.InputResult res = InputBox.Show("New TeamType", "Enter name:");
            
            if (res.ReturnCode == DialogResult.Cancel) return;

            if (teamtypes.GetByName(res.Text) == null)
            {
                TeamType tt = teamtypes.NewTT(NextId(), res.Text);
                int index = lstTT.Items.Add(tt.Name);
                ReloadLists();
                lstTT.SelectedIndex = index;
            }
            else
            {
                MessageBox.Show("Name already exists.", "Rename", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnTTRename_Click(object sender, EventArgs e)
        {
            string newname = txtTTName.Text;
            if (teamtypes.GetByName(newname) == null)
            {
                teamtypes.UpdateName(selectedtt, newname);
                int index = lstTT.SelectedIndex;
                lstTT.Items.RemoveAt(index);
                lstTT.Items.Insert(index, newname);
                ReloadLists();
                lstTT.SelectedIndex = index;
            }
            else
            {
                MessageBox.Show("Name already exists.", "Rename", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnTTRemove_Click(object sender, EventArgs e)
        {
            RemoveAIObject<TeamType>(lstTT, teamtypes);
            ReloadLists();
        }

        private void lstTr_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstTr.SelectedIndex == -1)
            {
                btnTrRemove.Enabled = false;
                btnTrRename.Enabled = false;
                btnTrCopy.Enabled = false;
                grpTr.Enabled = false;
            }
            else
            {
                btnTrRemove.Enabled = true;
                btnTrRename.Enabled = true;
                btnTrCopy.Enabled = true;
                grpTr.Enabled = true;
                ShowTr(lstTr.SelectedIndex);
            }
        }

        private void btnTrNew_Click(object sender, EventArgs e)
        {
            InputBox.InputResult res = InputBox.Show("New TriggerType", "Enter name:");

            if (res.ReturnCode == DialogResult.Cancel) return;

            if (triggertypes.GetByName(res.Text) == null)
            {
                TriggerType tr = triggertypes.NewTr(NextId(), res.Text);
                lstTr.SelectedIndex = lstTr.Items.Add(tr.Name);
            }
            else
            {
                MessageBox.Show("Name already exists.", "Rename", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnTrRename_Click(object sender, EventArgs e)
        {
            string newname = txtTrName.Text;
            if (triggertypes.GetByName(newname) == null)
            {
                triggertypes.UpdateName(selectedtr, newname);
                int index = lstTr.SelectedIndex;
                lstTr.Items.RemoveAt(index);
                lstTr.Items.Insert(index, newname);
                lstTr.SelectedIndex = index;
            }
            else
            {
                MessageBox.Show("Name already exists.", "Rename", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnTrRemove_Click(object sender, EventArgs e)
        {
            int index = lstTr.SelectedIndex;
            if (index == -1) return;
            TriggerType t = (TriggerType)triggertypes.Table[index];
            triggertypes.Remove(t.ID);
            lstTr.Items.RemoveAt(index);
            if (index >= lstTr.Items.Count) index--;
            if (lstTr.Items.Count > 0) lstTr.SelectedIndex = index;
        }

        /// <summary>
        /// TriggerType team type.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbTrTeamType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbTrTeamType.SelectedIndex == 0)
            {
                selectedtr.TeamType = "<none>";
                lblTrTeam2.Text = "<none>";
                return;
            }

            TeamType tt = (TeamType)teamtypes.Table[cmbTrTeamType.SelectedIndex - 1];
            if (tt == null) selectedtr.TeamType = "<none>";
            else selectedtr.TeamType = tt.ID;
            lblTrTeam.Text = selectedtr.TeamType;
        }

        /// <summary>
        /// Trigger type support team type.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbTrTeamType2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbTrTeamType2.SelectedIndex == 0)
            {
                selectedtr.TeamType2 = "<none>";
                lblTrTeam2.Text = "<none>";
                return;
            }

            TeamType tt = (TeamType)teamtypes.Table[cmbTrTeamType2.SelectedIndex - 1];
            if (tt == null) selectedtr.TeamType2 = "<none>";
            else selectedtr.TeamType2 = tt.ID;
            lblTrTeam2.Text = selectedtr.TeamType2;
        }

        private void cmbTrOwner_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedtr.Owner = (string)cmbTrOwner.SelectedItem;
        }

        private void cmbTrSide_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedtr.Side = cmbTrSide.SelectedIndex;
        }

        private void numTrTechLevel_ValueChanged(object sender, EventArgs e)
        {
            selectedtr.TechLevel = (int)numTrTechLevel.Value;
        }

        private void cmbTrCondition_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedtr.Condition = cmbTrCondition.SelectedIndex;
        }

        private void cmbTrTechType_SelectedIndexChanged(object sender, EventArgs e)
        {
            TechnoType tt = (TechnoType)rules.GetByName((string)cmbTrTechType.SelectedItem);
            if (tt == null) selectedtr.TechType = "<none>";
            else selectedtr.TechType = tt.ID;
        }

        private void cmbTrOperator_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedtr.Operator = cmbTrOperator.SelectedIndex;
        }

        private void numTrAmount_ValueChanged(object sender, EventArgs e)
        {
            selectedtr.Amount = (int)numTrAmount.Value;
        }

        private void numTrProb_ValueChanged(object sender, EventArgs e)
        {
            selectedtr.Prob = (int)numTrProb.Value;
        }

        private void numTrProbMin_ValueChanged(object sender, EventArgs e)
        {
            selectedtr.ProbMin = (int)numTrProbMin.Value;
        }

        private void numTrProbMax_ValueChanged(object sender, EventArgs e)
        {
            selectedtr.ProbMax = (int)numTrProbMax.Value;
        }


        private void chkTrOptions_SelectedValueChanged(object sender, EventArgs e)
        {
            if (selectedtr == null) return;
            int index = chkTrOptions.SelectedIndex;
            selectedtr.Options[index] = chkTrOptions.GetItemChecked(index);
        }

        /// <summary>
        /// Copy task force.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTFCopy_Click(object sender, EventArgs e)
        {
            TaskForce a = taskforces.Copy(lstTF.SelectedIndex, NextId());
            lstTF.SelectedIndex = lstTF.Items.Add(a.Name);
            ReloadLists();
        }

        /// <summary>
        /// Copy scripttype.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSTCopy_Click(object sender, EventArgs e)
        {
            ScriptType a = scripttypes.Copy(lstST.SelectedIndex, NextId());
            lstST.SelectedIndex = lstST.Items.Add(a.Name);
            ReloadLists();
        }

        /// <summary>
        /// Copy teamtype.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTTCopy_Click(object sender, EventArgs e)
        {
            TeamType a = teamtypes.Copy(lstTT.SelectedIndex, NextId());
            lstTT.SelectedIndex = lstTT.Items.Add(a.Name);
            ReloadLists();
        }

        /// <summary>
        /// Copy triggertype.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTrCopy_Click(object sender, EventArgs e)
        {
            TriggerType a = triggertypes.Copy(lstTr.SelectedIndex, NextId());
            lstTr.SelectedIndex = lstTr.Items.Add(a.Name);
        }

        /// <summary>
        /// Clear all data and reset the UI to the begin state.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuClear_Click(object sender, EventArgs e)
        {
			DialogResult res = MessageBox.Show("Are you sure? Changes will not be saved.",
				"Clear all data", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            
            if (res == DialogResult.Yes) ResetAll();
        }

        /// <summary>
        /// Show program info.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuInfo_Click(object sender, EventArgs e)
        {
            string text = "AIEdit v" + Application.ProductVersion + "\nProgrammed by Askeladd";
            string title = "About";
            MessageBox.Show(text, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Open the help file.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuHelp2_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process ai = new System.Diagnostics.Process();
            ai.StartInfo.FileName = Application.StartupPath + "\\ai.htm";
            ai.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Maximized;
            
            try
            {
                ai.Start();
            }
            catch
            {
                MessageBox.Show("AI.htm not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Insert Script Type action.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSTAInsert_Click(object sender, EventArgs e)
        {
            int action = cmbSTAction.SelectedIndex;
            int param;
            int index = 0;

            foreach (int i in lstSTActions.SelectedIndices) index = i;

            if (((ScriptActionType)settings.ActionTypes[action]).List == null)
            {
                param = int.Parse(cmbSTParam.Text);
            }
            else
            {
                param = cmbSTParam.SelectedIndex;
            }

            selectedst.InsertAction(action, param, index);
            InsertSTAction(action, param, index);

            ReloadLists();
        }

        private void MoveSTAction(int index, int direction)
        {
            int index2 = index + direction;
            selectedst.SwapActions(index, index2);
            ShowST(lstST.SelectedIndex, index2);
        }

        private void btnSTAUp_Click(object sender, EventArgs e)
        {
            int index = 0;
            foreach (int i in lstSTActions.SelectedIndices) index = i;

            /*if (index > 0) selectedst.SwapActions(index, index - 1);

            // Swap the list items.
            ListViewItem item1 = lstSTActions.Items[index];
            ListViewItem item2 = lstSTActions.Items[index - 1];
            lstSTActions.Items[index] = item2;
            lstSTActions.Items[index - 1] = item1;
            // index number
            lstSTActions.Items[index].SubItems[1].Text = index.ToString();
            lstSTActions.Items[index - 1].SubItems[1].Text = (index - 1).ToString();*/

            if (index > 0) MoveSTAction(index, -1);
        }

        private void btnSTADown_Click(object sender, EventArgs e)
        {
            int index = lstSTActions.Items.Count - 1;
            foreach (int i in lstSTActions.SelectedIndices) index = i;

            if ( index < (lstSTActions.Items.Count - 1) ) MoveSTAction(index, 1);

            /*if ( index < (lstSTActions.Items.Count - 1) ) selectedst.SwapActions(index, index + 1);

            // Swap the list items.
            ListViewItem item1 = lstSTActions.Items[index];
            ListViewItem item2 = lstSTActions.Items[index + 1];
            lstSTActions.Items[index] = item2;
            lstSTActions.Items[index + 1] = item1;
            // index number
            lstSTActions.Items[index].SubItems[1].Text = index.ToString();
            lstSTActions.Items[index + 1].SubItems[1].Text = (index + 1).ToString();*/
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            /*if (!ai_opened) return;

            DialogResult res = MessageBox.Show("Exit without saving?", "Exit", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

            // Don't exit without saving.
            if (res == DialogResult.No)
            {
                saveFileDialog1.Filter = "AI files (ai*.ini)|ai*.ini";
                if (saveFileDialog1.ShowDialog() == DialogResult.Yes)
                {
                    SaveAI(saveFileDialog1.FileName);
                }
            }
            // Cancel exit.
            else if (res == DialogResult.Cancel)
            {
                e.Cancel = true;
            }*/

            DialogResult res = MessageBox.Show("Are you sure? Changes will not be saved.", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            
            e.Cancel = (res == DialogResult.No);
        }

		private void cmbTFGroup_SelectedIndexChanged_1(object sender, EventArgs e)
		{
			if (selectedtf == null) return;
			selectedtf.Group = settings.Group[(string)cmbTFGroup.SelectedItem];
		}

		private void cmbTTHouse_SelectedIndexChanged(object sender, EventArgs e)
		{
            if (selectedtt == null) return;
            selectedtt.House = (string)cmbTTHouse.SelectedItem;
		}

		private void mnuLoadRulesRA2_Click(object sender, EventArgs e)
		{
			LoadRulesStrings("config/ra2.ini");
		}

		private void mnuLoadRulesTS_Click(object sender, EventArgs e)
		{
			LoadRulesStrings("config/ts.ini");
		}
	}
}
