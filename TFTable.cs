using System;
using System.IO;
using System.Collections;

namespace AIEdit
{
	/// <summary>
	/// Summary description for TFTable.
	/// </summary>
	public class TFTable : AITable<TaskForce>
	{

		public TFTable()
		{
		}

        private string MakeId(uint id)
        {
            return "0" + id.ToString("X") + "-G";
        }

        public override string TypeList { get { return "TaskForces"; } }

        public TaskForce NewTF(uint id, string name)
        {
            TaskForce tf = new TaskForce();
            tf.ID = MakeId(id);
            tf.Name = name; // tf.ID;
            tf.Group = -1;
            Add(tf.ID, tf);
            return tf;
        }

        public TaskForce Copy(int index, uint id)
        {
            TaskForce old = (TaskForce)Table[index];
            TaskForce newtt = new TaskForce(old, MakeId(id));
            newtt.Name = newtt.Name + " COPY";
            Add(newtt.ID, newtt);
            return newtt;
        }

		public override void ParseSection(IniParser ip)
		{
            TaskForce tf = GetByID(ip.Section);
			TaskForce.TFUnit unit;
            string id = ip.Section;
			string[] split;
			char[] delim = {','};
			IDictionaryEnumerator e;

            if(tf == null)
			{
				ip.Skip();
				return;
			}

			ip.Parse();

            tf.ID = id;
			tf.Name = (string)ip.Table["Name"];
            if (tf.Name == null) tf.Name = tf.ID;

            tf.Group = int.Parse((string)ip.Table["Group"]);

            e = ip.Table.GetEnumerator();
			while(e.MoveNext())
			{
				if((string)e.Key == "Name" || (string)e.Key == "Group") continue;
					
				split = ((string)e.Value).Split(delim);
				unit = new TaskForce.TFUnit( split[1], int.Parse(split[0]) );
				tf.Units.Add(unit);
			}

            MapName(tf.Name, id);
		}

        public void Write(StreamWriter stream)
        {
            int n = 0;

            // Save the [TaskForces] list.
            stream.WriteLine("[TaskForces]");
            foreach (string id in Table.Keys)
            {
                stream.WriteLine(n.ToString() + "=" + id);
                n++;
            }

            stream.WriteLine();

            foreach (TaskForce tf in Table.Values) tf.Write(stream);           
        }
	}
}
