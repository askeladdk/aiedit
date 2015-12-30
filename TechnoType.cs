using System;
using System.IO;

namespace AIEdit
{
	public class TechnoType : IAIObject, IParamListEntry, IComparable<TechnoType>
	{
        private string name, owner, id;
        private uint cost;
		private uint tableIndex;

		public TechnoType(string id, string name, uint cost, uint tableIndex)
		{
			this.id = id;
			this.name = name;
			this.owner = "";
			this.cost = cost;
			this.tableIndex = tableIndex;
		}

        public string Name { get { return name; } set { name = value; } }
        public string Owner { get { return owner; } set { owner = value; } }
        public string ID { get { return id; } set { id = value; } }
        public uint Cost { get { return cost; } set { cost = value; } }
		public uint TableIndex { get { return tableIndex; } }

		public int CompareTo(TechnoType other)
		{
			return this.name.CompareTo((other as TechnoType).name);
		}

		public override string ToString()
		{
			return name;
		}
		
		public void Write(StreamWriter stream)
		{ }

		public uint ParamListIndex { get { return tableIndex; } }
	}
}
