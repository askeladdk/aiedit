using System;

namespace AIEdit
{
	public class TechnoType : IAIObjectOld, IComparable<TechnoType>
	{
        private string name, owner, id;
        private uint cost;

		public TechnoType()
		{
            name = "";
            owner = "";
            id = "";
            cost = 0;
		}

		public TechnoType(string id, string name, uint cost)
		{
			this.id = id;
			this.name = name;
			this.owner = "";
			this.cost = cost;
		}

        public string Name { get { return name; } set { name = value; } }
        public string Owner { get { return owner; } set { owner = value; } }
        public string ID { get { return id; } set { id = value; } }
        public uint Cost { get { return cost; } set { cost = value; } }
		
		public int CompareTo(TechnoType other)
		{
			return this.name.CompareTo((other as TechnoType).name);
		}
	}
}
