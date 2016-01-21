using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AIEdit
{
	public partial class UseInfoBox : Form
	{
		private object selectedObject = null;

		public static object Show(string title, IEnumerable objects)
		{
			UseInfoBox frm = new UseInfoBox();
			frm.Text = title;
			frm.olvUses.SetObjects(objects);
			frm.olvUses.Sort();
			frm.olvUses.SelectedIndex = 0;
			frm.ShowDialog();

			return frm.selectedObject;
		}

		public UseInfoBox()
		{
			InitializeComponent();
		}

		private void olvUses_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			selectedObject = olvUses.SelectedObject;
			Close();
		}

		private void btnClose_Click(object sender, EventArgs e)
		{
			selectedObject = null;
			Close();
		}
	}
}
