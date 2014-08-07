using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace AIEdit
{
    class ScriptActionType
    {

        private string name, desc;
        private ComboBoxStyle style;
        private ArrayList list;

        public ScriptActionType()
        {
            desc = "";
        }

        public string Name { get { return name; } set { name = value; } }
        public string Description { get { return desc; } set { desc = value; } }
        public ComboBoxStyle Style { get { return style; } set { style = value; } }
        public ArrayList List { get { return list; } set { list = value; } }
    }
}
