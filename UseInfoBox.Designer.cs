namespace AIEdit
{
	partial class UseInfoBox
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
			this.olvUses = new BrightIdeasSoftware.ObjectListView();
			this.olvColName = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
			this.olvColID = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
			this.btnClose = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.olvUses)).BeginInit();
			this.SuspendLayout();
			// 
			// olvUses
			// 
			this.olvUses.AllColumns.Add(this.olvColName);
			this.olvUses.AllColumns.Add(this.olvColID);
			this.olvUses.CellEditUseWholeCell = false;
			this.olvUses.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColName,
            this.olvColID});
			this.olvUses.Cursor = System.Windows.Forms.Cursors.Default;
			this.olvUses.FullRowSelect = true;
			this.olvUses.HighlightBackgroundColor = System.Drawing.Color.Empty;
			this.olvUses.HighlightForegroundColor = System.Drawing.Color.Empty;
			this.olvUses.Location = new System.Drawing.Point(12, 12);
			this.olvUses.MultiSelect = false;
			this.olvUses.Name = "olvUses";
			this.olvUses.ShowGroups = false;
			this.olvUses.Size = new System.Drawing.Size(336, 262);
			this.olvUses.Sorting = System.Windows.Forms.SortOrder.Ascending;
			this.olvUses.TabIndex = 0;
			this.olvUses.UseCompatibleStateImageBehavior = false;
			this.olvUses.View = System.Windows.Forms.View.Details;
			this.olvUses.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.olvUses_MouseDoubleClick);
			// 
			// olvColName
			// 
			this.olvColName.AspectName = "Name";
			this.olvColName.Text = "Name";
			this.olvColName.Width = 300;
			// 
			// olvColID
			// 
			this.olvColID.AspectName = "ID";
			this.olvColID.Text = "ID";
			this.olvColID.Width = 0;
			// 
			// btnClose
			// 
			this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnClose.Location = new System.Drawing.Point(273, 280);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(75, 23);
			this.btnClose.TabIndex = 1;
			this.btnClose.Text = "Close";
			this.btnClose.UseVisualStyleBackColor = true;
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// UseInfoBox
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnClose;
			this.ClientSize = new System.Drawing.Size(360, 308);
			this.Controls.Add(this.btnClose);
			this.Controls.Add(this.olvUses);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.Name = "UseInfoBox";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Uses";
			((System.ComponentModel.ISupportInitialize)(this.olvUses)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private BrightIdeasSoftware.ObjectListView olvUses;
		private BrightIdeasSoftware.OLVColumn olvColName;
		private BrightIdeasSoftware.OLVColumn olvColID;
		private System.Windows.Forms.Button btnClose;
	}
}