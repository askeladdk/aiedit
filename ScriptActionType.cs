using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace AIEdit
{
    class ScriptActionTypeOld
    {

        private string name, desc;
        private ComboBoxStyle style;
        private ArrayList list;

        public ScriptActionTypeOld()
        {
            desc = "";
        }

        public string Name { get { return name; } set { name = value; } }
        public string Description { get { return desc; } set { desc = value; } }
        public ComboBoxStyle Style { get { return style; } set { style = value; } }
        public ArrayList List { get { return list; } set { list = value; } }
    }

	public class ScriptActionType
	{
		private string name, desc;
		private ComboBoxStyle style;
		private OrderedDictionary list;

		public string Name { get { return name; } }
		public string Description { get { return desc; } }
		public ComboBoxStyle Style { get { return style; } }
		public OrderedDictionary List { get { return list; } }

		public ScriptActionType(string name, string desc, ComboBoxStyle style, OrderedDictionary list)
		{
			this.name = name;
			this.desc = desc;
			this.style = style;
			this.list = list;
		}
	}
}
