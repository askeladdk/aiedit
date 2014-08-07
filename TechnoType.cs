using System;

namespace AIEdit
{
	public class TechnoType : IAIObject
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

        public string Name { get { return name; } set { name = value; } }
        public string Owner { get { return owner; } set { owner = value; } }
        public string ID { get { return id; } set { id = value; } }
        public int Cost { get { return cost; } set { cost = value; } }
	}
}
