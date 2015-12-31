using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.IO;
using System.Linq;

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

    class TeamTypeOld : IAIObjectOld
    {
        private bool[] settings;
        private string name, id, scriptid, taskid, house;
        private int priority, max, group, mcdecision, vetlvl, techlevel;
        private int index;

        /// <summary>
        /// Constructor.
        /// </summary>
        public TeamTypeOld()
        {
            settings = new bool[21];
            name = "";
            id = "";
            scriptid = "<none>";
            taskid = "<none>";
			house = "<none>";
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
        public TeamTypeOld(TeamTypeOld tt, string newid)
        {
            settings = (bool[])tt.settings.Clone();
            id = newid;
            name = (string)tt.name.Clone();//tt.name + " " + id;
            scriptid = (string)tt.scriptid.Clone();
            taskid = (string)tt.taskid.Clone();
			house = (string)tt.house.Clone();
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
            stream.WriteLine("Loadable=" + ToStr(this[TTSetting.Loadable]));
            stream.WriteLine("Full=" + ToStr(this[TTSetting.Full]));
            stream.WriteLine("Annoyance=" + ToStr(this[TTSetting.Annoyance]));
            stream.WriteLine("GuardSlower=" + ToStr(this[TTSetting.GuardSlower]));
            stream.WriteLine("House=" + this.house);
            stream.WriteLine("Recruiter=" + ToStr(this[TTSetting.Recruiter]));
            stream.WriteLine("Autocreate=" + ToStr(this[TTSetting.Autocreate]));
            stream.WriteLine("Prebuild=" + ToStr(this[TTSetting.Prebuild]));
            stream.WriteLine("Reinforce=" + ToStr(this[TTSetting.Reinforce]));
            stream.WriteLine("Droppod=" + ToStr(this[TTSetting.Droppod]));
            stream.WriteLine("UseTransportOrigin=" + ToStr(this[TTSetting.UseTransportOrigin]));
            stream.WriteLine("Whiner=" + ToStr(this[TTSetting.Whiner]));
            stream.WriteLine("LooseRecruit=" + ToStr(this[TTSetting.LooseRecruit]));
            stream.WriteLine("Aggressive=" + ToStr(this[TTSetting.Aggressive]));
            stream.WriteLine("Suicide=" + ToStr(this[TTSetting.Suicide]));
            stream.WriteLine("Priority=" + priority.ToString());
            stream.WriteLine("Max=" + max.ToString());
            stream.WriteLine("TechLevel=" + techlevel.ToString());
            stream.WriteLine("Group=" + group.ToString());
            stream.WriteLine("OnTransOnly=" + ToStr(this[TTSetting.OnTransOnly]));
            stream.WriteLine("AvoidThreats=" + ToStr(this[TTSetting.AvoidThreats]));
            stream.WriteLine("IonImmune=" + ToStr(this[TTSetting.IonImmune]));
            stream.WriteLine("TransportsReturnOnUnload=" + ToStr(this[TTSetting.TransportsReturnOnUnload]));
            stream.WriteLine("AreTeamMembersRecruitable=" + ToStr(this[TTSetting.AreTeamMembersRecruitable]));
            stream.WriteLine("IsBaseDefense=" + ToStr(this[TTSetting.IsBaseDefense]));
            stream.WriteLine("OnlyTargetHouseEnemy=" + ToStr(this[TTSetting.OnlyTargetHouseEnemy]));
            stream.WriteLine("Script=" + scriptid);
            stream.WriteLine("TaskForce=" + taskid);

            stream.WriteLine();
        }

        private string ToStr(bool b)
        {
			return b ? "yes" : "no";
        }

        public string ScriptType { get { return scriptid; } set { scriptid = value; } }
        public string TaskForce { get { return taskid; } set { taskid = value; } }
        public string House { get { return house; } set { house = value; } }
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





	public abstract class TeamTypeOption
	{
		protected string tag, name;

		public string Name { get { return name; } }

		public TeamTypeOption(string tag, string name)
		{
			this.tag = tag;
			this.name = name;
		}

		public abstract IList List { get; }
		public abstract object DefaultValue { get; }
		public abstract int SortOrder { get; }

		public abstract TeamTypeEntry Parse(OrderedDictionary section);

		public abstract void Write(StreamWriter stream, object value);
	}

	public class TeamTypeOptionBool : TeamTypeOption
	{
		public TeamTypeOptionBool(string tag, string name) : base(tag, name)
		{
		}

		public override IList List { get { return null; } }
		public override object DefaultValue { get { return false; } }
		public override int SortOrder { get { return 3; } }

		public override TeamTypeEntry Parse(OrderedDictionary section)
		{
			return new TeamTypeEntry(this, section.GetBool(tag));
		}

		public override void Write(StreamWriter stream, object value)
		{
			string v = (bool)value ? "yes" : "no";
			stream.WriteLine(tag + "=" + v);
		}
	}

	public class TeamTypeOptionNumber : TeamTypeOption
	{
		public TeamTypeOptionNumber(string tag, string name) : base(tag, name)
		{
		}

		public override IList List { get { return null; } }
		public override object DefaultValue { get { return 0U; } }
		public override int SortOrder { get { return 2; } }

		public override TeamTypeEntry Parse(OrderedDictionary section)
		{
			return new TeamTypeEntry(this, section.GetUint(tag));
		}

		public override void Write(StreamWriter stream, object value)
		{
			stream.WriteLine(tag + "=" + ((uint)value));
		}
	}

	public class TeamTypeOptionList : TeamTypeOption
	{
		protected IList dataList;

		public TeamTypeOptionList(string tag, string name, IList dataList)
			: base(tag, name)
		{
			this.dataList = dataList;
		}

		public override IList List { get { return dataList; } }
		public override object DefaultValue { get { return dataList[0]; } }
		public override int SortOrder { get { return 1; } }

		public override TeamTypeEntry Parse(OrderedDictionary section)
		{
			int val = section.GetInt(tag);
			foreach (AITypeListEntry listitem in dataList)
			{
				if (listitem.Value == val) return new TeamTypeEntry(this, listitem);
			}
			return new TeamTypeEntry(this, null);
		}

		public override void Write(StreamWriter stream, object value)
		{
			int v = (value as AITypeListEntry).Value;
			stream.WriteLine(tag + "=" + v);
		}
	}

	public class TeamTypeOptionStringList : TeamTypeOption
	{
		protected IList dataList;

		public TeamTypeOptionStringList(string tag, string name, IList dataList)
			: base(tag, name)
		{
			this.dataList = dataList;
		}

		public override IList List { get { return dataList; } }
		public override object DefaultValue { get { return dataList[0]; } }
		public override int SortOrder { get { return 1; } }

		public override TeamTypeEntry Parse(OrderedDictionary section)
		{
			string val = section.GetString(tag);
			foreach (AITypeListEntry listitem in dataList)
			{
				if (listitem.Name == val) return new TeamTypeEntry(this, listitem);
			}
			return new TeamTypeEntry(this, null);
		}

		public override void Write(StreamWriter stream, object value)
		{
			string v = (value as AITypeListEntry).Name;
			stream.WriteLine(tag + "=" + v);
		}
	}

	public class TeamTypeOptionAIObject : TeamTypeOption
	{
		protected IList dataList;

		public TeamTypeOptionAIObject(string tag, string name, IList dataList) : base(tag, name)
		{
			this.dataList = dataList;
		}

		public override IList List { get { return dataList; } }
		public override object DefaultValue { get { return dataList[0]; } }
		public override int SortOrder { get { return 0; } }

		public override TeamTypeEntry Parse(OrderedDictionary section)
		{
			string id = section.GetString(tag);
			foreach(IAIObject aiobj in dataList)
			{
				if (aiobj.ID == id) return new TeamTypeEntry(this, aiobj);
			}
			return new TeamTypeEntry(this, null);
		}

		public override void Write(StreamWriter stream, object value)
		{
			string v = (value as IAIObject).ID;
			stream.WriteLine(tag + "=" + v);
		}
	}



	public class TeamTypeEntry
	{
		private TeamTypeOption option;
		private object value;

		public string Name { get { return option.Name; } }
		public TeamTypeOption Option { get { return option; } }
		public int SortOrder { get { return option.SortOrder; } }

		public object Value
		{
			get
			{
				return value;
			}
			set
			{
				if (this.value is IAIObject) (this.value as IAIObject).DecUses();
				if (value is IAIObject) (value as IAIObject).IncUses();
				this.value = value;
			}
		}

		public TeamTypeEntry(TeamTypeOption option, object value)
		{
			this.option = option;
			Value = value;
		}

		public void Write(StreamWriter stream)
		{
			option.Write(stream, value);
		}
	}

	public class TeamType : IAIObject, IEnumerable<TeamTypeEntry>
	{
		private string name, id;
		private List<TeamTypeEntry> entries;
		private int uses;

		public string Name { get { return name; } set { name = value; } }
		public string ID { get { return id; } }
		public int Uses { get { return uses; } }

		public void IncUses() { uses++; }
		public void DecUses() { uses--; }

		public IEnumerator<TeamTypeEntry> GetEnumerator()
		{
			return entries.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public TeamType(string id, string name, List<TeamTypeEntry> entries)
		{
			this.id = id;
			this.name = name;
			this.entries = (entries != null) ? entries : new List<TeamTypeEntry>();
		}

		public void Write(StreamWriter stream)
		{
			stream.WriteLine("[" + id + "]");
			stream.WriteLine("Name=" + name);
			foreach (TeamTypeEntry entry in entries) entry.Write(stream);
			stream.WriteLine();
		}

		public static TeamType Parse(string id, OrderedDictionary section, List<TeamTypeOption> options)
		{
			List<TeamTypeEntry> entries = new List<TeamTypeEntry>();
			string name = section.GetString("Name");

			foreach(TeamTypeOption option in options)
			{
				entries.Add(option.Parse(section));
			}

			return new TeamType(id, name, entries);
		}
	}
}
