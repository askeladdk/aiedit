using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace AIEdit
{
    public enum TTSetting
    {
        Aggressive,
        Annoyance,
        AreTeamMembersRecruitable,
        Autocreate,
        AvoidThreats,
        Droppod,
        Full,
        GuardSlower,
        IonImmune,
        IsBaseDefense,
        Loadable,
        LooseRecruit,
        OnlyTargetHouseEnemy,
        OnTransOnly,
        Prebuild,
        Recruiter,
        Reinforce,
        Suicide,
        TransportsReturnOnUnload,
        UseTransportOrigin,
        Whiner
    };

    class TeamType : IAIObject
    {
        private bool[] settings;
        private string name, id, scriptid, taskid;
        private int priority, max, group, mcdecision, vetlvl, techlevel;
        private int index;

        /// <summary>
        /// Constructor.
        /// </summary>
        public TeamType()
        {
            settings = new bool[21];
            name = "";
            id = "";
            scriptid = "<none>";
            taskid = "<none>";
            priority = 0;
            max = 0;
            group = -1;
            mcdecision = 0;
            vetlvl = 1;
            techlevel = 0;
        }

        /// <summary>
        /// Copy constructor.
        /// </summary>
        /// <param name="tt"></param>
        public TeamType(TeamType tt, string newid)
        {
            settings = (bool[])tt.settings.Clone();
            id = newid;
            name = (string)tt.name.Clone();//tt.name + " " + id;
            scriptid = (string)tt.scriptid.Clone();
            taskid = (string)tt.taskid.Clone();
            priority = tt.priority;
            max = tt.max;
            group = tt.group;
            mcdecision = tt.mcdecision;
            vetlvl = tt.vetlvl;
            techlevel = tt.techlevel;
        }

        public void Write(StreamWriter stream)
        {
            //stream.WriteLine();
            stream.WriteLine("[" + id + "]");
            stream.WriteLine("Name=" + name);
            stream.WriteLine("VeteranLevel=" + vetlvl.ToString());
            stream.WriteLine("MindControlDecision=" + mcdecision.ToString());
            stream.WriteLine("Loadable=" + ToStr(this[10]));
            stream.WriteLine("Full=" + ToStr(this[6]));
            stream.WriteLine("Annoyance=" + ToStr(this[1]));
            stream.WriteLine("GuardSlower=" + ToStr(this[7]));
            stream.WriteLine("House=<none>");
            stream.WriteLine("Recruiter=" + ToStr(this[15]));
            stream.WriteLine("Autocreate=" + ToStr(this[3]));
            stream.WriteLine("Prebuild=" + ToStr(this[14]));
            stream.WriteLine("Reinforce=" + ToStr(this[16]));
            stream.WriteLine("Droppod=" + ToStr(this[5]));
            stream.WriteLine("UseTransportOrigin=" + ToStr(this[19]));
            stream.WriteLine("Whiner=" + ToStr(this[20]));
            stream.WriteLine("LooseRecruit=" + ToStr(this[11]));
            stream.WriteLine("Aggressive=" + ToStr(this[0]));
            stream.WriteLine("Suicide=" + ToStr(this[17]));
            stream.WriteLine("Priority=" + priority.ToString());
            stream.WriteLine("Max=" + max.ToString());
            stream.WriteLine("TechLevel=" + techlevel.ToString());
            stream.WriteLine("Group=" + group.ToString());
            stream.WriteLine("OnTransOnly=" + ToStr(this[13]));
            stream.WriteLine("AvoidThreats=" + ToStr(this[4]));
            stream.WriteLine("IonImmune=" + ToStr(this[8]));
            stream.WriteLine("TransportsReturnOnUnload=" + ToStr(this[18]));
            stream.WriteLine("AreTeamMembersRecruitable=" + ToStr(this[2]));
            stream.WriteLine("IsBaseDefense=" + ToStr(this[9]));
            stream.WriteLine("OnlyTargetHouseEnemy=" + ToStr(this[12]));
            stream.WriteLine("Script=" + scriptid);
            stream.WriteLine("TaskForce=" + taskid);

            stream.WriteLine();
        }

        private string ToStr(bool b)
        {
            if (b) return "yes";
            return "no";
        }

        public string ScriptType { get { return scriptid; } set { scriptid = value; } }
        public string TaskForce { get { return taskid; } set { taskid = value; } }
        public int Priority { get { return priority; } set { priority = value; } }
        public int Max { get { return max; } set { max = value; } }
        public int Group { get { return group; } set { group = value; } }
        public int MCDecision { get { return mcdecision; } set { mcdecision = value; } }
        public int VeteranLevel { get { return vetlvl; } set { vetlvl = value; } }
        public int TechLevel { get { return techlevel; } set { techlevel = value; } }

        public bool[] Settings { get { return settings; } }

        public bool this[int index]
        {
            get { return settings[index]; }
            set { settings[index] = value; }
        }

        public bool this[TTSetting o]
        {
            get { return settings[(int)o]; }
            set { settings[(int)o] = value; }
        }

        #region IAIObject Members
        public string Name { get { return name; } set { name = value; } }
        public string ID { get { return id; } set { id = value; } }
        public int Index { get { return index; } set { index = value; } }
        #endregion
    }
}
