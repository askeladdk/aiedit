using System;

namespace AIEdit
{
	public class TechnoType : IAIObject, IComparable<TechnoType>
	{
        private string name, owner, id;
        private int cost;

		public TechnoType()
		{
            name = "";
            owner = "";
            id = "";
            cost = 0;
		}

		public TechnoType(string id, string name, int cost)
		{
			this.id = id;
			this.name = name;
			this.owner = "";
			this.cost = cost;
		}

        public string Name { get { return name; } set { name = value; } }
        public string Owner { get { return owner; } set { owner = value; } }
        public string ID { get { return id; } set { id = value; } }
        public int Cost { get { return cost; } set { cost = value; } }
		
		public int CompareTo(TechnoType other)
		{
			return this.name.CompareTo((other as TechnoType).name);
		}
	}
}
