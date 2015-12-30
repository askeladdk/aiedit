using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AIEdit
{
	public class GroupType
	{
		private int value;
		private string name;

		public int Value { get { return value; } }
		public string Name { get { return name;} }

		public GroupType(int value, string name)
		{
			this.value = value;
			this.name = name;
		}

		public override string ToString()
		{
			return name;
		}
	}
}
