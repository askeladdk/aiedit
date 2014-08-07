using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AIEdit
{
    public partial class InputBox : Form
    {
        private DialogResult result;
        private string resulttext;

        public class InputResult
        {
            private DialogResult dr;
            private string text;

            public InputResult(DialogResult dr, string text)
            {
                this.dr = dr;
                this.text = text;
            }

            public DialogResult ReturnCode { get { return dr; } }
            public string Text { get { return text; } }
        };

        public InputBox()
        {
            InitializeComponent();
            result = DialogResult.Cancel;
            resulttext = "";
        }

        public static InputResult Show(string title, string description)
        {
            InputBox input = new InputBox();
            input.Text = title;
            input.textBox2.Text = description;
            input.ShowDialog();
            return new InputResult(input.ResultCode, input.ResultText);
        }

        private DialogResult ResultCode { get { return result; } }
        private string ResultText { get { return resulttext; } }

        private void btnOK_Click(object sender, EventArgs e)
        {
            result = DialogResult.OK;
            resulttext = textBox1.Text;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}