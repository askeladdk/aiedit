using System;
using System.Collections;
using System.Windows.Forms;
using System.IO;

namespace AIEdit
{
    /// <summary>
    /// 
    /// </summary>
	public class TechnoTableOld : AITable<TechnoType>
	{
        private string typelist;

		public TechnoTableOld(string typelist)
		{
            this.typelist = typelist;
		}

		public void AddNames(ArrayList al)
		{
            foreach (TechnoType t in Table.Values) al.Add(t.Name);
		}

        public override string TypeList { get { return typelist; } }

        public override void ParseSection(IniParser ip)
        {
            string id = ip.Section;
            string name, cost;
            TechnoType tt = (TechnoType)GetByID(id);

            if (tt == null)
            {
                ip.Skip();
                return;
            }

            ip.Parse();
            name = (string)ip.Table["Name"];

            if (name == null || name.Length == 0)
			{
				name = "[" + id + "]";
			}
			else
			{
				name = name + " [" + id + "]";
			}

            // Save properties.
            tt.ID = id;
            tt.Name = name;
            cost = (string)ip.Table["Cost"];
            tt.Cost = cost == null ? 0 : int.Parse(cost);
            tt.Owner = (string)ip.Table["Owner"];

            MapName(name, id);
        }
	}
}
