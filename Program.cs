using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace AIEdit
{
	class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.Run(new frmMainNew());
		}
	}
}
