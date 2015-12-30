using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;

namespace AIEdit
{
	/// <summary>
	/// Summary description for AITable.
	/// </summary>
	public abstract class AITableOld<T> where T : IAIObjectOld//, new()
	{
		private Hashtable names;
        private OrderedDictionary table;

		public AITableOld()
		{
			names = new Hashtable();
            table = new OrderedDictionary();
		}

        public OrderedDictionary Table { get { return table; } }

		public void Add(string id, T t)
		{
            table[id] = t;
			names[t.Name] = id;
		}

		public void Remove(string id)
		{
            if (!table.Contains(id)) return;
            names.Remove(((T)table[id]).Name);
            table.Remove(id);
		}
        
        public void UpdateName(T t, string newname)
        {
            if (!names.ContainsKey(t.Name)) return;
            names.Remove(t.Name);
            names[newname] = t.ID;
            t.Name = newname;
        }


        public int GetIndex(string id)
        {
            int i = 0;
            foreach (string s in table.Keys) if (s.CompareTo(id) == 0) return i;
            return -1;
        }

        public T GetByID(string id)
		{
            if (!table.Contains(id)) return default(T);
            return (T)table[id];
		}

		public T GetByName(string name)
		{
            if (!names.ContainsKey(name)) return default(T);
			return (T)table[ names[name] ];
		}

        public string[] GetNames(bool sort)
        {
            ArrayList al = new ArrayList(table.Count);
            foreach (T t in table.Values) if (t.Name != null) al.Add(t.Name);
            if(sort) al.Sort();
            return (string[])al.ToArray(typeof(string));
        }

        public void Load(string file)
		{
			StreamReader stream = new StreamReader(file);
			IniParser ip = new IniParser(stream);
            T t;

            table.Clear();
            names.Clear();
			// Parse the section that contains the id list.
			while(ip.Next())
			{
				if(ip.Section != this.TypeList)
				{
					ip.Skip();
					continue;
				}

				ip.Parse();
                foreach (string id in ip.Table.Values)
                {
                    if (id == null || id.Length == 0) continue;
                    if (!table.Contains(id))
                    {
						t = default(T);//new T();
                        t.ID = id;
                        table.Add(id, t);
                    }
                }
				
				break;
			}

            stream.Close();
            stream = new StreamReader(file);
            ip = new IniParser(stream);

			// Parse each section that is in the list.
            while (ip.Next()) ParseSection(ip);

			stream.Close();

            // Check for invalid entries. 
            foreach (T t2 in table.Values)
            {
                if (t2.Name == null || t2.Name.Length == 0)
                {
                    t2.Name = t2.ID;// +" (INVALID ENTRY)";
                    names[t2.Name] = t2.ID;
                }
            }
		}

        public int Count { get { return table.Count; } }
        public IDictionaryEnumerator GetEnumerator() { return table.GetEnumerator(); }

        protected void MapName(string name, string id) { names[name] = id; }
        

		public abstract void ParseSection(IniParser ip);
		public abstract string TypeList { get; }
	}





	public class AITable<T> where T : IAIObject
	{
		private List<T> items;
		private string name;

		public string Name { get { return name; } }

		public int Count { get { return items.Count; } }
		public List<T> Items { get { return items; } }

		public T this[int index]
		{
			get
			{
				return items[index];
			}
		}

		public T this[string id]
		{
			get
			{
				foreach (T entry in items)
				{
					if (entry.ID == id) return entry;
				}
				return default(T);
			}
		}

		public AITable(string name, List<T> items=null)
		{
			this.name = name;
			this.items = (items != null) ? items : new List<T>();
		}

		public void Add(T entry)
		{
			items.Add(entry);
		}

		public void Remove(T entry)
		{
			items.Remove(entry);
		}

		public void Write(StreamWriter stream)
		{
			stream.WriteLine("[" + name + "]");
			int n = 0;
			foreach(T entry in items)
			{
				stream.WriteLine(n + "=" + entry.ID);
				n++;
			}
			stream.WriteLine();

			foreach(T entry in items) entry.Write(stream);
		}
	}
}
