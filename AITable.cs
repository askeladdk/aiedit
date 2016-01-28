using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;

namespace AIEdit
{
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
			entry.Reset();
			items.Remove(entry);
		}

		public void Write(StreamWriter stream, bool writeIDs=true)
		{
			stream.WriteLine("[" + name + "]");

			if (writeIDs)
			{
				int n = 0;
				foreach (T entry in items)
				{
					stream.WriteLine(n + "=" + entry.ID);
					n++;
				}
				stream.WriteLine();
			}

			foreach(T entry in items) entry.Write(stream);
		}
	}
}
