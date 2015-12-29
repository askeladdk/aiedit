using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.IO;

namespace AIEdit
{
    class STTable : AITableOld<ScriptType>
    {
        public STTable()
        {
        }

        private string MakeId(uint id)
        {
            return "0" + id.ToString("X") + "-G";
        }

        public ScriptType NewST(uint id, string name)
        {
            ScriptType st = new ScriptType();
            st.ID = MakeId(id); // +"C-G";
            st.Name = name;
            Add(st.ID, st);
            return st;
        }

        public ScriptType Copy(int index, uint id)
        {
            ScriptType old = (ScriptType)Table[index];
            ScriptType newtt = new ScriptType(old, MakeId(id));
            newtt.Name = newtt.Name + " COPY";
            Add(newtt.ID, newtt);
            return newtt;
        }

        public void Write(StreamWriter stream)
        {
            int n = 0;
            stream.WriteLine("[ScriptTypes]");
            foreach (string id in Table.Keys)
            {
                stream.WriteLine(n.ToString() + "=" + id);
                n++;
            }

            stream.WriteLine();

            foreach (ScriptType st in Table.Values)
            {
                st.Write(stream);
            }
        }

        public override string TypeList { get { return "ScriptTypes"; } }

        public override void ParseSection(IniParser ip)
        {
            string id = ip.Section;
            ScriptType st = GetByID(id);
            IDictionaryEnumerator e;
            string key;
            string[] split;
            int action, param;

            if (st == null)
            {
                ip.Skip();
                return;
            }

            ip.Parse();

            st.ID = id;
            st.Name = (string)ip.Table["Name"];
            if (st.Name == null) st.Name = st.ID;

            e = ip.Table.GetEnumerator();
            while (e.MoveNext())
            {
                key = (string)e.Key;
                if (key.CompareTo("Name") == 0) continue;
                
                split = ((string)e.Value).Split(',');
                action = int.Parse(split[0]);
                param = int.Parse(split[1]);

                st.AddAction(action, param);
            }

            //st.Actions.Sort();
            MapName(st.Name, st.ID);
        }
    }
}
