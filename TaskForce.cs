using System;
using System.Collections;
using System.IO;

namespace AIEdit
{
	/// <summary>
	/// Summary description for TaskForce.
	/// </summary>
	public class TaskForce : IAIObject
	{
		private ArrayList units;
		private string name, id;
        private int group;

        /// <summary>
        /// Constructor.
        /// </summary>
		public TaskForce()
		{
			units = new ArrayList();
            group = -1;
		}

        /// <summary>
        /// Copy constructor.
        /// </summary>
        /// <param name="tf"></param>
        public TaskForce(TaskForce tf, string newid)
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
}
