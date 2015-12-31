using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AIEdit
{
	public class AITypeListEntry : IComparable
	{
		private int value;
		private string name;

		public int Value { get { return value; } }
		public string Name { get { return name;} }

		public AITypeListEntry(int value, string name)
		{
			this.value = value;
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
