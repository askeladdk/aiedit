using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AIEdit
{
	public class AITypeListEntry : IComparable
	{
		private int index;
		private string name;

		public int Index { get { return index; } }
		public string Name { get { return name;} }

		public AITypeListEntry(int index, string name)
		{
			this.index = index;
			this.name = name;
		}

		public override string ToString()
		{
			return name;
		}

		public int CompareTo(object other)
		{
			return name.CompareTo((other as AITypeListEntry).name);
		}
	}
}
