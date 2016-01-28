using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;
using System.Globalization;
using System.Windows.Forms;

namespace AIEdit
{
	public abstract class TriggerTypeOption
	{
		protected string name;
		private int sortOrder;

		public string Name { get { return name; } }
		public int SortOrder { get { return sortOrder; } }

		public abstract IList List { get; }
		public abstract string ToString(object value);

		public abstract TriggerTypeEntry DefaultValue();

		public TriggerTypeOption(string name, int sortOrder)
		{
			this.name = name;
			this.sortOrder = sortOrder;
		}

		public virtual object FindByIndex(int index, object def=null)
		{
			foreach (AITypeListEntry entry in this.List)
			{
				if (entry.Index == index) return entry;
			}
			return def;
		}

		public virtual object FindByString(string str, object def=null)
		{
			foreach (AITypeListEntry entry in this.List)
			{
				if (entry.Name == str) return entry;
			}
			return def;
		}
	}


	public class TriggerTypeOptionBool : TriggerTypeOptionList
	{
		private static List<AITypeListEntry> bools = new List<AITypeListEntry>()
		{
			new AITypeListEntry(0, "no"),
			new AITypeListEntry(1, "yes"),
		};
		
		public TriggerTypeOptionBool(string name, int sortOrder)
			: base(name, sortOrder, bools)
		{
		}
	}

	public class TriggerTypeOptionNumber : TriggerTypeOption
	{
		public override IList List { get { return null; } }

		public override string ToString(object value)
		{
			return value.ToString();
		}

		public TriggerTypeOptionNumber(string name, int sortOrder)
			: base(name, sortOrder)
		{
		}

		public override TriggerTypeEntry DefaultValue()
		{
			return new TriggerTypeEntry(this, 0U);
		}
	}

	public class TriggerTypeOptionList : TriggerTypeOption
	{
		protected IList dataList;

		public override IList List { get { return dataList; } }

		public override string ToString(object value)
		{
			return (value as AITypeListEntry).Index.ToString();
		}

		public TriggerTypeOptionList(string name, int sortOrder, IList dataList)
			: base(name, sortOrder)
		{
			this.dataList = dataList;
		}

		public override TriggerTypeEntry DefaultValue()
		{
			return new TriggerTypeEntry(this, dataList[0]);
		}
	}

	public class TriggerTypeOptionStringList : TriggerTypeOptionList
	{
		public override string ToString(object value)
		{
			return (value as AITypeListEntry).Name;
		}

		public TriggerTypeOptionStringList(string name, int sortOrder, IList dataList)
			: base(name, sortOrder, dataList)
		{
		}
	}

	public class TriggerTypeOptionAIObject : TriggerTypeOptionList
	{
		public override string ToString(object value)
		{
			return (value as IAIObject).ID;
		}

		public TriggerTypeOptionAIObject(string name, int sortOrder, IList dataList)
			: base(name, sortOrder, dataList)
		{
		}

		public override object FindByString(string str, object def=null)
		{
			foreach (IAIObject entry in this.List)
			{
				if (entry.ID == str) return entry;
			}
			return def;
		}
	}

	public class TriggerTypeOptionTechno : TriggerTypeOptionList
	{
		public override string ToString(object value)
		{
			return (value as TechnoType).ID;
		}

		public TriggerTypeOptionTechno(string name, int sortOrder, IList dataList)
			: base(name, sortOrder, dataList)
		{
		}

		public override object FindByString(string str, object def = null)
		{
			foreach (TechnoType entry in this.List)
			{
				if (entry.ID == str) return entry;
			}
			return def;
		}
	}
	
	public class TriggerTypeEntry
	{
		private TriggerTypeOption option;
		private object value;


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

		public string Name { get { return option.Name; } }
		public int SortOrder { get { return option.SortOrder; } }
		public IList List { get { return option.List; } }
		public TriggerTypeOption Option { get { return option; } }

		public TriggerTypeEntry(TriggerTypeOption option, object value)
		{
			this.option = option;
			Value = value;
		}

		public override string ToString()
		{
			return option.ToString(value);
		}
	}

	public class TriggerType : IAIObject, IEnumerable<TriggerTypeEntry>
	{
		private string id, name;
		private Dictionary<string, TriggerTypeEntry> entries;

		public string ID { get { return id; } }
		public string Name { get { return name; } set { name = value; } }
		public int Uses { get { return 0; } }
		public void IncUses() { }
		public void DecUses() { }

		public bool HasTeamType(TeamType tt)
		{
			return entries["Team1"].Value == tt || entries["Team2"].Value == tt;
		}

		public IEnumerator<TriggerTypeEntry> GetEnumerator()
		{
			return entries.Values.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public void Reset()
		{
			foreach (KeyValuePair<string, TriggerTypeEntry> e in entries) e.Value.Reset();
			entries.Clear();
		}

		public TriggerType(string id, string name, Dictionary<string, TriggerTypeEntry> entries)
		{
			this.id = id;
			this.name = name;
			this.entries = entries;
		}

		public TriggerType(string id, string name, Dictionary<string, TriggerTypeOption> triggerTypeOptions)
		{
			this.id = id;
			this.name = name;
			this.entries = new Dictionary<string, TriggerTypeEntry>();

			foreach(KeyValuePair<string, TriggerTypeOption> option in triggerTypeOptions)
			{
				this.entries.Add(option.Key, option.Value.DefaultValue());
			}
		}

		public void Write(StreamWriter stream)
		{
			string s = string.Format("{0}={1},{2},{3},{4},{5},{6},{7}0{8}000000000000000000000000000000000000000000000000000000,{9}.000000,{10}.000000,{11}.000000,{12},0,{13},{14},{15},{16},{17},{18}",
				id,
				name,
				(entries["Team1"].Value as TeamType).ID,
				entries["Owner"].Value,
				entries["TechLevel"].Value,
				(entries["Condition"].Value as AITypeListEntry).Index,
				(entries["TechType"].Value as TechnoType).ID,
				((uint)entries["Amount"].Value).SwapEndianness().ToString("x8"),
				(entries["Operator"].Value as AITypeListEntry).Index,
				entries["Prob"].Value,
				entries["ProbMin"].Value,
				entries["ProbMax"].Value,
				(entries["Skirmish"].Value as AITypeListEntry).Index,
				(entries["Side"].Value as AITypeListEntry).Index,
				(entries["BaseDefense"].Value as AITypeListEntry).Index,
				(entries["Team2"].Value as TeamType).ID,
				(entries["Easy"].Value as AITypeListEntry).Index,
				(entries["Medium"].Value as AITypeListEntry).Index,
				(entries["Hard"].Value as AITypeListEntry).Index
			);
			stream.WriteLine(s);
		}

		public static TriggerType Parse(string id, string data,
			Dictionary<string, TriggerTypeOption> triggerTypeOptions, TeamType noneTeam, TechnoType noneTechno,
			Logger logger)
		{
			string[] split = data.Split(',');
			string tag;
			string name = split[0];
			TriggerTypeOption option;
			Dictionary<string, TriggerTypeEntry> entries = new Dictionary<string, TriggerTypeEntry>();
			object value;

			if (name == null) name = id;

			// team 1
			tag = "Team1";
			option = triggerTypeOptions[tag];
			value = option.FindByString(split[1], noneTeam);
			entries.Add(tag, new TriggerTypeEntry(option, value));

			// owner
			tag = "Owner";
			option = triggerTypeOptions[tag];
			value = option.FindByString(split[2]);
			entries.Add(tag, new TriggerTypeEntry(option, value));

			// techlevel
			tag = "TechLevel";
			option = triggerTypeOptions[tag];
			value = uint.Parse(split[3]);
			entries.Add(tag, new TriggerTypeEntry(option, value));

			// condition
			tag = "Condition";
			option = triggerTypeOptions[tag];
			value = option.FindByIndex(int.Parse(split[4]));
			entries.Add(tag, new TriggerTypeEntry(option, value));

			// techtype
			tag = "TechType";
			option = triggerTypeOptions[tag];
			string unitid = split[5];
			value = noneTechno;

			if (unitid != "<none>")
			{
				value = option.FindByString(unitid);
				if (value == null)
				{
					logger.Add("TechnoType " + split[5] + " referenced by Trigger " + id + " does not exist!");
					value = new TechnoType(unitid, unitid, 0, 0);
					option.List.Add(value);
				}
			}

			entries.Add(tag, new TriggerTypeEntry(option, value));

			// amount
			tag = "Amount";
			option = triggerTypeOptions[tag];
			value = Convert.ToUInt32(split[6].Substring(0, 8), 16).SwapEndianness();
			entries.Add(tag, new TriggerTypeEntry(option, value));

			// operator
			tag = "Operator";
			option = triggerTypeOptions[tag];
			value = uint.Parse(split[6].Substring(9, 1));
			value = option.FindByIndex((int)(uint)value);
			entries.Add(tag, new TriggerTypeEntry(option, value));

			// starting probability
			tag = "Prob";
			option = triggerTypeOptions[tag];
			value = (uint)float.Parse(split[7], CultureInfo.InvariantCulture);
			entries.Add(tag, new TriggerTypeEntry(option, value));

			// minimum probability
			tag = "ProbMin";
			option = triggerTypeOptions[tag];
			value = (uint)float.Parse(split[8], CultureInfo.InvariantCulture);
			entries.Add(tag, new TriggerTypeEntry(option, value));

			// maximum probability
			tag = "ProbMax";
			option = triggerTypeOptions[tag];
			value = (uint)float.Parse(split[9], CultureInfo.InvariantCulture);
			entries.Add(tag, new TriggerTypeEntry(option, value));

			// skirmish
			tag = "Skirmish";
			option = triggerTypeOptions[tag];
			value = option.FindByIndex(int.Parse("0" + split[10]));
			entries.Add(tag, new TriggerTypeEntry(option, value));

			// [11] is unknown and always zero

			// side
			tag = "Side";
			option = triggerTypeOptions[tag];
			int side = int.Parse(split[12]);
			value = option.FindByIndex(side);
			entries.Add(tag, new TriggerTypeEntry(option, value));

			// base defense
			tag = "BaseDefense";
			option = triggerTypeOptions[tag];
			value = option.FindByIndex(int.Parse(split[13]));
			entries.Add(tag, new TriggerTypeEntry(option, value));

			// team 2
			tag = "Team2";
			option = triggerTypeOptions[tag];
			value = option.FindByString(split[14], noneTeam);
			entries.Add(tag, new TriggerTypeEntry(option, value));

			// easy
			tag = "Easy";
			option = triggerTypeOptions[tag];
			value = option.FindByIndex(int.Parse(split[15]));
			entries.Add(tag, new TriggerTypeEntry(option, value));

			// medium
			tag = "Medium";
			option = triggerTypeOptions[tag];
			value = option.FindByIndex(int.Parse(split[16]));
			entries.Add(tag, new TriggerTypeEntry(option, value));

			// hard
			tag = "Hard";
			option = triggerTypeOptions[tag];
			value = option.FindByIndex(int.Parse(split[17]));
			entries.Add(tag, new TriggerTypeEntry(option, value));

			return new TriggerType(id, name, entries);
		}
	}
}
