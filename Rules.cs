using System;
using System.Collections;
using System.IO;

namespace AIEdit
{
	/// <summary>
	/// Summary description for TechnoArray.
	/// </summary>
	public class Rules
	{
		private TechnoTable[] types;
        private HouseList houses;

		public enum Types
		{
			Units,
			Buildings,
			All
		};

		public Rules()
		{
			types = new TechnoTable[4];
			types[0] = new TechnoTable("VehicleTypes");
            types[1] = new TechnoTable("InfantryTypes");
            types[2] = new TechnoTable("AircraftTypes");
            types[3] = new TechnoTable("BuildingTypes");
            houses = new HouseList();
		}

		public void Load(string file)
		{
			types[0].Load(file);
            types[1].Load(file);
            types[2].Load(file);
            types[3].Load(file);
            houses.Read(file);
			/*types[3].Add( "POWER", new TechnoType("<POWER>", "<all>", 0) );
			types[3].Add( "TECH", new TechnoType("<TECH>", "<all>", 0) );
			types[3].Add( "PROC", new TechnoType("<PROC>", "<all>", 0) );
			types[3].Add( "BARRACKS", new TechnoType("<BARRACKS>", "<all>", 0) );
			types[3].Add( "FACTORY", new TechnoType("<FACTORY>", "<all>", 0) );
			types[3].Add( "RADAR", new TechnoType("<RADAR>", "<all>", 0) );*/
		}

        public ArrayList Houses { get { return houses.Houses; } }

		public TechnoType GetByID(string id)
		{
			foreach(TechnoTable t in types)
			{
				TechnoType tt = t.GetByID(id);
				if(tt != null) return tt;
			}
			return null;
		}

		public TechnoType GetByName(string name)
		{
			foreach(TechnoTable t in types)
			{
				TechnoType tt = t.GetByName(name);
				if(tt != null) return tt;
			}
			return null;
		}

		public string[] GetNames(Types type, bool sort)
		{
			string[] r;
			ArrayList al = new ArrayList();

			switch(type)
			{
				case Types.Units:
					types[0].AddNames(al);
					types[1].AddNames(al);
					types[2].AddNames(al);
					break;
				case Types.Buildings:
					types[3].AddNames(al);
					break;
				case Types.All:
					foreach(TechnoTable t in types) t.AddNames(al);
					break;
			};

			if(sort) al.Sort();
			r = (string[])al.ToArray(typeof(string));
			return r;
		}
	}
}
