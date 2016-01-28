using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.IO;
using System.Linq;

namespace AIEdit
{
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
		public abstract TeamTypeEntry DefaultValue { get; }
		public abstract int SortOrder { get; }

		public abstract TeamTypeEntry Parse(OrderedDictionary section);

		public abstract void Write(StreamWriter stream, object value);
	}

	public class TeamTypeOptionBool : TeamTypeOptionStringList
	{
		private static List<AITypeListEntry> bools = new List<AITypeListEntry>()
		{
			new AITypeListEntry(0, "no"),
			new AITypeListEntry(1, "yes"),
		};

		public override int SortOrder { get { return 3; } }

		public TeamTypeOptionBool(string tag, string name) : base(tag, name, bools)
		{
		}
	}

	public class TeamTypeOptionNumber : TeamTypeOption
	{
		public TeamTypeOptionNumber(string tag, string name) : base(tag, name)
		{
		}

		public override IList List { get { return null; } }
		public override TeamTypeEntry DefaultValue { get { return new TeamTypeEntry(this, (object)0U); } }
		public override int SortOrder { get { return 2; } }

		public override TeamTypeEntry Parse(OrderedDictionary section)
		{
			if (!section.Contains(tag)) return DefaultValue;
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
		public override TeamTypeEntry DefaultValue { get { return new TeamTypeEntry(this, dataList[0]); } }
		public override int SortOrder { get { return 1; } }

		public override TeamTypeEntry Parse(OrderedDictionary section)
		{
			if (!section.Contains(tag)) return DefaultValue;
			int val = section.GetInt(tag);
			foreach (AITypeListEntry listitem in dataList)
			{
				if (listitem.Index == val) return new TeamTypeEntry(this, listitem);
			}
			return new TeamTypeEntry(this, dataList[0]);
		}

		public override void Write(StreamWriter stream, object value)
		{
			int v = (value as AITypeListEntry).Index;
			stream.WriteLine(tag + "=" + v);
		}
	}

	public class TeamTypeOptionStringList : TeamTypeOptionList
	{
		public TeamTypeOptionStringList(string tag, string name, IList dataList) : base(tag, name, dataList)
		{
		}

		public override int SortOrder { get { return 1; } }

		public override TeamTypeEntry Parse(OrderedDictionary section)
		{
			if (!section.Contains(tag)) return DefaultValue;
			string val = section.GetString(tag);
			foreach (AITypeListEntry listitem in dataList)
			{
				if (listitem.Name == val) return new TeamTypeEntry(this, listitem);
			}
			return new TeamTypeEntry(this, dataList[0]);
		}

		public override void Write(StreamWriter stream, object value)
		{
			string v = (value as AITypeListEntry).Name;
			stream.WriteLine(tag + "=" + v);
		}
	}

	public class TeamTypeOptionAIObject : TeamTypeOptionList
	{
		public TeamTypeOptionAIObject(string tag, string name, IList dataList) : base(tag, name, dataList)
		{
		}
		public override int SortOrder { get { return 0; } }

		public override TeamTypeEntry Parse(OrderedDictionary section)
		{
			if (!section.Contains(tag)) return DefaultValue;
			string id = section.GetString(tag);
			foreach(IAIObject aiobj in dataList)
			{
				if (aiobj.ID == id) return new TeamTypeEntry(this, aiobj);
			}
			return new TeamTypeEntry(this, dataList[0]);
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

		public void Reset()
		{
			if (this.value is IAIObject) (this.value as IAIObject).DecUses();
			this.value = null;
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

	public class TeamType : IAIObject, IEnumerable<TeamTypeEntry>, IParamListEntry
	{
		private string name, id;
		private List<TeamTypeEntry> entries;
		private int uses;


		public uint ParamListIndex { get { return 0; } }

		public string Name { get { return name; } set { name = value.Trim(); } }
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

		public bool HasObject(object obj)
		{
			return entries.FirstOrDefault(e => e.Value == obj) != null;
		}

		public TeamType(string id, string name, List<TeamTypeEntry> entries)
		{
			this.id = id;
			this.name = name;
			this.entries = (entries != null) ? entries : new List<TeamTypeEntry>();
		}

		public override string ToString()
		{
			return name;
		}

		public void Reset()
		{
			foreach(TeamTypeEntry e in this.entries) e.Reset();
			entries.Clear();
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
			if (name == null) name = id;

			foreach(TeamTypeOption option in options)
			{
				entries.Add(option.Parse(section));
			}

			return new TeamType(id, name, entries);
		}
	}
}
