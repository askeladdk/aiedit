using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Collections;
using System.Collections.Specialized;

namespace AIEdit
{
    /// <summary>
    /// This is not an AITable because the AITriggerTypes section is read slightly
    /// differently.
    /// </summary>
    class TrTable
    {
        private OrderedDictionary table;
        private Hashtable names;

        private string MakeId(uint id)
        {
            return "0" + id.ToString("X") + "-G";
        }

        public TrTable()
        {
            table = new OrderedDictionary();
            names = new Hashtable();
        }

        public TriggerType NewTr(uint id, string name)
        {
            TriggerType tr = new TriggerType();
            tr.ID = MakeId(id);
            tr.Name = name; // tf.ID;
            //if (tr.Name == null) tr.Name = tr.ID;
            table.Add(tr.ID, tr);
            names.Add(tr.Name, tr);
            return tr;
        }

        public TriggerType Copy(int index, uint id)
        {
            TriggerType old = (TriggerType)Table[index];
            TriggerType newtt = new TriggerType(old, MakeId(id));
            newtt.Name = newtt.Name + " COPY";
            table[newtt.ID] = newtt;
            names[newtt.Name] = newtt;
            return newtt;
        }

        public void UpdateName(TriggerType t, string newname)
        {
            if (!names.ContainsKey(t.Name)) return;
            names.Remove(t.Name);
            names[newname] = t;
            t.Name = newname;
        }

        public void Remove(string id)
        {
            table.Remove(id);
            names.Remove(id);
        }

        public void Load(string file)
        {
            StreamReader stream = new StreamReader(file);
            IniParser ip = new IniParser(stream);
            IDictionaryEnumerator e;
            TriggerType tr;

            while (ip.Next())
            {
                if (ip.Section.CompareTo("AITriggerTypes") == 0)
                {
                    ip.Parse();
                    e = ip.Table.GetEnumerator();
                    while (e.MoveNext())
                    {
                        tr = new TriggerType();
                        tr.Parse((string)e.Key, (string)e.Value);
                        table.Add(tr.ID, tr);
                        names[tr.Name] = tr;
                    }
                }
                else
                {
                    ip.Skip();
                }
            }

            stream.Close();
        }

        public void Write(StreamWriter stream)
        {
            stream.WriteLine("[AITriggerTypes]");
            foreach (TriggerType tt in table.Values) tt.Write(stream);
        }

        public string[] GetNames()
        {
            int i = 0;
            string[] a = new string[table.Count];
            foreach (TriggerType tr in table.Values) a[i++] = tr.Name;
            return a;
        }

        public TriggerType GetByID(string id)
        {
            if (!table.Contains(id)) return null;
            return (TriggerType)table[id];
        }

        public TriggerType GetByName(string name)
        {
            if (!names.Contains(name)) return null;
            return (TriggerType)names[name];
        }

        public OrderedDictionary Table { get { return table; } }
    }
}
