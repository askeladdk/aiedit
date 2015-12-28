using System;
using System.Collections;
using System.IO;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace AIEdit
{
	/// <summary>
	/// Summary description for TaskForce.
	/// </summary>
	public class TaskForceOld : IAIObject
	{
		private ArrayList units;
		private string name, id;
        private int group;

        /// <summary>
        /// Constructor.
        /// </summary>
		public TaskForceOld()
		{
			units = new ArrayList();
            group = -1;
		}

        /// <summary>
        /// Copy constructor.
        /// </summary>
        /// <param name="tf"></param>
        public TaskForceOld(TaskForceOld tf, string newid)
        {
            id = newid;
            name = (string)tf.name.Clone();
            units = new ArrayList();
            group = tf.group;
            // Deep copy.
            foreach (TFUnit unit in tf.units) units.Add(new TFUnit(unit));
        }

        public bool AddUnit(TFUnit newunit)
        {
            foreach (TFUnit unit in Units)
            {
                if (unit.ID.CompareTo(newunit.ID) == 0)
                {
                    unit.Count += newunit.Count;
                    return true;
                }
            }

            Units.Add(newunit);
            return true;
        }

        public bool ModifyUnit(string unitid, TFUnit newunit)
        {
            foreach (TFUnit unit in Units)
            {
                if (unit.ID.CompareTo(unitid) == 0)
                {
                    if (newunit.Count == 0)
                    {
                        Units.Remove(unit);
                    }
                    else
                    {
                        unit.ID = newunit.ID;
                        unit.Count = newunit.Count;
                    }
                    return true;
                }
            }
            return false;
        }

        public bool RemoveUnit(string unitid)
        {
            foreach (TFUnit unit in Units)
            {
                if (unit.ID.CompareTo(unitid) == 0)
                {
                    Units.Remove(unit);
                    return true;
                }
            }
            return false;
        }

        public void Write(StreamWriter stream)
        {
            int n = 0;
            //stream.WriteLine();
            stream.WriteLine("[" + id + "]");
            stream.WriteLine("Name=" + name);

            foreach (TFUnit unit in units)
            {
                stream.WriteLine(n.ToString() + "=" + unit.Count.ToString() + "," + unit.ID);
                n++;
            }
            stream.WriteLine("Group=" + group.ToString());
            stream.WriteLine();
        }

		public ArrayList Units { get { return units; } }

        public string Name { get { return name; } set { name = value; } }
        public string ID { get { return id; } set { id = value; } }

        public int Group { get { return group; } set { group = value; } }



        /// <summary>
        /// TFUnit.
        /// </summary>
        public class TFUnit
        {
            private string id;
            private int count;

            public TFUnit(string id, int count)
            {
                this.id = id;
                this.count = count;
            }

            public TFUnit(TFUnit unit)
            {
                id = (string)unit.id.Clone();
                count = unit.count;
            }

            public string ID { get { return id; } set { id = value; } }

            public int Count
            {
                get { return count; }
                set { count = value > 100 ? 100 : value; }
            }
        };
	}




	public class TaskForce
	{
		private string id;
		private OrderedDictionary units;

		public string Name;
		public int Group;

		public string ID { get { return id; } }
		public OrderedDictionary Units { get { return units; } }

		public TaskForce(string id, string name, int group, OrderedDictionary units = null)
		{
			this.id = id;
			this.Name = name;
			this.Group = group;
			this.units = (units == null) ? new OrderedDictionary() : units;
		}
		
		public int ModUnit(TechnoType unit, int amount)
		{
			int oldamount = 0;
			if (this.units.Contains(unit)) oldamount = (int)this.units[unit];
			amount = Math.Max( 0, amount + oldamount);
			if (amount == 0)
			{
				this.units.Remove(unit);
				return -1;
			}
			else
			{
				this.units[unit] = amount;
				return oldamount > 0 ? 0 : 1;
			}
		}

		public int SetUnit(TechnoType unit, int amount)
		{
			int oldamount = 0;
			if (this.units.Contains(unit)) oldamount = (int)this.units[unit];
			return ModUnit(unit, amount - oldamount);
		}

		public void DelUnit(TechnoType unit)
		{
			units.Remove(unit);
		}

		public void Write(StreamWriter stream)
		{
			int n = 0;
			stream.WriteLine("[" + this.id + "]");
			stream.WriteLine("Name=" + this.Name);

			foreach(DictionaryEntry entry in this.units)
			{
				TechnoType tt = entry.Key as TechnoType;
				int count     = (int)entry.Value;
				stream.WriteLine(n.ToString() + "=" + count.ToString() + "," + tt.ID);
				n++;
			}
			stream.WriteLine("Group=" + Group.ToString());
			stream.WriteLine();
		}

		public static TaskForce Parse(string id, OrderedDictionary section, OrderedDictionary technoTypes)
		{
			string name = section["Name"] as string;
			int group = int.Parse(section["Group"] as string);
			OrderedDictionary units = new OrderedDictionary();
			TechnoType deftt = technoTypes[0] as TechnoType;

			for (int i = 1; i < section.Count - 1; i++)
			{
				string[] split = (section[i] as string).Split(',');
				int count = int.Parse(split[0] as string);
				string unitid = split[1] as string;
				TechnoType tt = deftt;

				if (!technoTypes.Contains(unitid))
				{
					string msg = string.Format(@"TechnoType {0} referenced in TaskForce {1} does not exist.
							Replacing reference with {2}!", unitid, id, deftt.ID);
					MessageBox.Show(msg, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				else
				{
					tt = technoTypes[unitid] as TechnoType;
				}

				units[tt] = count;
			}

			return new TaskForce(id, name, group, units);
		}
	}
}
