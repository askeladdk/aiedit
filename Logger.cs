using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AIEdit
{
	public class Logger : IEnumerable<string>
	{
		private List<string> entries;

		public int Count { get { return entries.Count; } }

		public Logger()
		{
			entries = new List<string>();
		}

		public void Add(string message)
		{
			entries.Add(message);
		}

		public void Clear()
		{
			entries.Clear();
		}

		public IEnumerator<string> GetEnumerator()
		{
			return entries.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}
