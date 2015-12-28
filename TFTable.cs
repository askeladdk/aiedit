using System;
using System.IO;
using System.Collections;

namespace AIEdit
{
	/// <summary>
	/// Summary description for TFTable.
	/// </summary>
	public class TFTable : AITable<TaskForceOld>
	{

		public TFTable()
		{
		}

        private string MakeId(uint id)
        {
            return "0" + id.ToString("X") + "-G";
        }

        public override string TypeList { get { return "TaskForces"; } }

        public TaskForceOld NewTF(uint id, string name)
        {
            TaskForceOld tf = new TaskForceOld();
            tf.ID = MakeId(id);
            tf.Name = name; // tf.ID;
            tf.Group = -1;
            Add(tf.ID, tf);
            return tf;
        }

        public TaskForceOld Copy(int index, uint id)
        {
            TaskForceOld old = (TaskForceOld)Table[index];
            TaskForceOld newtt = new TaskForceOld(old, MakeId(id));
            newtt.Name = newtt.Name + " COPY";
            Add(newtt.ID, newtt);
            return newtt;
        }

		public override void ParseSection(IniParser ip)
		{
            TaskForceOld tf = GetByID(ip.Section);
			TaskForceOld.TFUnit unit;
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
				unit = new TaskForceOld.TFUnit( split[1], int.Parse(split[0]) );
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

            foreach (TaskForceOld tf in Table.Values) tf.Write(stream);           
        }
	}
}
