using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.IO;

namespace AIEdit
{
    /// <summary>
    /// ScriptType.
    /// </summary>
    class ScriptType : IAIObject
    {
        private string name, id;
        private ArrayList actions;

        /// <summary>
        /// Constructor.
        /// </summary>
        public ScriptType()
        {
            actions = new ArrayList();
            name = "";
            id = "";
        }

        /// <summary>
        /// Copy constructor.
        /// </summary>
        /// <param name="st"></param>
        public ScriptType(ScriptType st, string newid)
        {
            id = newid;
            name = (string)st.name.Clone();

            actions = new ArrayList();

            // Deep copy.
            foreach (ScriptType.ScriptAction sa in st.actions)
            {
                actions.Add(new ScriptAction(sa));
            }
        }

        public ScriptAction AddAction(int action, int param)
        {
            ScriptAction sta;
            sta = new ScriptAction(action, param);
            actions.Add(sta);
            return sta;
        }

        public ScriptAction InsertAction(int action, int param, int index)
        {
            ScriptAction sta = new ScriptAction(action, param);
            actions.Insert(index, sta);
            return sta;
        }

        public bool RemoveAction(int action_index)
        {
            if (action_index >= actions.Count) return false;
            actions.RemoveAt(action_index);
            return true;
        }

        public bool ModifyAction(int action_index, int action, int param)
        {
            ScriptAction sta = (ScriptAction)Actions[action_index];
            if (sta == null) return false;
            sta.Action = action;
            sta.Param = param;
            return true;
        }

        public void SwapActions(int indexa, int indexb)
        {
            ScriptAction a = (ScriptAction)Actions[indexa];
            ScriptAction b = (ScriptAction)Actions[indexb];
            Actions[indexa] = b;
            Actions[indexb] = a;
        }

        public void Write(StreamWriter stream)
        {
            int n = 0;//, p;

            //stream.WriteLine();
            stream.WriteLine("[" + id + "]");
            stream.WriteLine("Name=" + name);

            foreach (ScriptAction sa in actions)
            {
                //p = sa.Param + sa.Offset;
                //stream.WriteLine(n.ToString() + "=" + sa.Action.ToString() + "," + p.ToString());
                sa.Write(stream, n);
                n++;
            }

            stream.WriteLine();
        }


        public ArrayList Actions { get { return actions; } }
        
        #region IAIObject Members
        public string Name { get { return name; } set { name = value; } }
        public string ID { get { return id; } set { id = value; } }
        #endregion



        /// <summary>
        /// Script action.
        /// </summary>
        public class ScriptAction
        {
			private static int[] offsets = { 0, 65536, 131072, 196608, 262144 };
            private int action, param, offset;

            public ScriptAction(int action, int param)
            {
                this.action = action;
                this.param = CheckOffset(param);
            }

            public ScriptAction(ScriptAction sa)
            {
                action = sa.action;
                param = sa.param;
                offset = sa.offset;
            }

			// annoying special case buildingtype offsets.
            private int CheckOffset(int n)
            {
				for (int i = offsets.Length - 1; i >= 0 ; i--)
				{
					int of = offsets[i];
					if (n >= of)
					{
						this.offset = i;
						return n - of;
					}
				}
				this.offset = 0;
				return n;
            }

            public void Write(StreamWriter stream, int index)
            {
                int n = param + offsets[offset];
                stream.WriteLine(index.ToString() + "=" + action.ToString() + "," + n.ToString());
            }

            public int Action { get { return action; } set { action = value; } }
            public int Param { get { return param; } set { param = CheckOffset(value); } }
            public int Offset { get { return offset; } set { offset = value; } }
        };
    }
}
