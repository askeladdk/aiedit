using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;
using System.Globalization;

namespace AIEdit
{
    enum TriggerOption
    {
        Easy,
        Medium,
        Hard,
        Skirmish,
        BaseDefense
    }

    class TriggerTypeOld : IAIObjectOld
    {
        private string name, id;
        private string teamtype, teamtype2, owner, techtype;
        private int side, techlevel, oper, amount, prob, probmax, probmin, condition;
        private bool[] options;

        public TriggerTypeOld()
        {
            name = "";
            id = "";
            teamtype = "<none>";
            teamtype2 = "<none>";
            owner = "<all>";
            techtype = "<none>";
            side = 0;
            techlevel = 0;
            oper = 0;
            amount = 0;
            prob = 0;
            probmax = 0;
            probmin = 0;
            condition = 0;
            options = new bool[5];
        }

        /// <summary>
        /// Copy constructor.
        /// </summary>
        /// <param name="tt"></param>
        public TriggerTypeOld(TriggerTypeOld tt, string newid)
        {
            name = new string(tt.name.ToCharArray());
            id = newid;
            name = (string)tt.name.Clone();
            teamtype = (string)tt.teamtype.Clone();
            teamtype2 = (string)tt.teamtype2.Clone();
            owner = (string)tt.owner.Clone();
            techtype = (string)tt.techtype.Clone();
            side = tt.side;
            techlevel = tt.techlevel;
            oper = tt.oper;
            amount = tt.amount;
            prob = tt.prob;
            probmax = tt.probmax;
            probmin = tt.probmin;
            condition = tt.condition;
            options = (bool[])tt.options.Clone();
        }

        private int ParseInt(string[] split, int idx)
        {
            if (split[idx].Length > 0) return int.Parse(split[idx]);
            return 0;
        }

        public void Parse(string id, string line)
        {
            string[] split = line.Split(',');
            
            this.id = id;
            name = split[0];
            teamtype = split[1];
            owner = split[2];
            techlevel = int.Parse(split[3]);
            condition = int.Parse(split[4]);
            techtype = split[5];
			
			string s_amount = split[6].Substring(0, 8);
			this.amount = (int)SwapEndianness( Convert.ToUInt32(s_amount, 16) );
			string s_oper = split[6].Substring(8, 8);
			this.oper = (int)SwapEndianness( Convert.ToUInt32(s_oper, 16) );

            prob = DecToInt(split[7]);
            probmin = DecToInt(split[8]);
            probmax = DecToInt(split[9]);

            options[3] = ParseInt(split, 10) == 1;
            // 11 is unknown
            side = ParseInt(split, 12);
            options[4] = ParseInt(split, 13) == 1;
            teamtype2 = split[14];
            options[0] = ParseInt(split, 15) == 1;
            options[1] = ParseInt(split, 16) == 1;
            options[2] = ParseInt(split, 17) == 1;
        }

        public void Write(StreamWriter stream)
        {
            stream.Write(id + '=' + name + ',' + teamtype + ',' + owner + ',');
            stream.Write(techlevel.ToString() + ',' + condition.ToString() + ',');
            stream.Write(techtype + ',');

			byte[] b_amount = BitConverter.GetBytes((uint)amount);
			byte[] b_oper = BitConverter.GetBytes((uint)oper);
			stream.Write( BitConverter.ToString(b_amount).Replace("-", "").ToLower() );
			stream.Write( BitConverter.ToString(b_oper).Replace("-", "").ToLower() );

            stream.Write("000000000000000000000000000000000000000000000000,"); //000000,");

            stream.Write(prob.ToString() + ".000000,");
            stream.Write(probmin.ToString() + ".000000,");
            stream.Write(probmax.ToString() + ".000000,");

            stream.Write((options[3] ? '1' : '0') + ",0,");
            stream.Write(side.ToString() + ",");
            stream.Write((options[4] ? '1' : '0') + ",");
            stream.Write(teamtype2 + ",");
            stream.Write((options[0] ? '1' : '0') + ",");
            stream.Write((options[1] ? '1' : '0') + ",");
            stream.WriteLine(options[2] ? '1' : '0');
        }

		private uint SwapEndianness(uint x)
		{
			return ((x & 0x000000ff) << 24) +
			       ((x & 0x0000ff00) << 8) +
			       ((x & 0x00ff0000) >> 8) +
			       ((x & 0xff000000) >> 24);
		}

        private int DecToInt(string s)
        {
            int dot = s.IndexOf('.');
            string s2 = s.Substring(0, dot);
            return int.Parse(s2);
        }
		
        public string Owner { get { return owner; } set { owner = value; } }
        public string TeamType { get { return teamtype; } set { teamtype = value; } }
        public string TeamType2 { get { return teamtype2; } set { teamtype2 = value; } }
        public string TechType { get { return techtype; } set { techtype = value; } }

        public int Side { get { return side; } set { side = value; } }
        public int TechLevel { get { return techlevel; } set { techlevel = value; } }
        public int Operator { get { return oper; } set { oper = value; } }
        public int Amount { get { return amount; } set { amount = value; } }
        public int Prob { get { return prob; } set { prob = value; } }
        public int ProbMax { get { return probmax; } set { probmax = value; } }
        public int ProbMin { get { return probmin; } set { probmin = value; } }
        public int Condition { get { return condition + 1; } set { condition = value - 1; } }
        public bool[] Options { get { return options; } }

        #region IAIObject Members
        public string Name { get { return name; } set { name = value; } }
        public string ID { get { return id; } set { id = value; } }
        #endregion
    }




	public abstract class TriggerTypeOption
	{
		protected string name;
		private int sortOrder;

		public string Name { get { return name; } }
		public int SortOrder { get { return sortOrder; } }

		public abstract IList List { get; }
		public abstract string ToString(object value);

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

		public IEnumerator<TriggerTypeEntry> GetEnumerator()
		{
			return entries.Values.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public TriggerType(string id, string name, Dictionary<string, TriggerTypeEntry> entries)
		{
			this.id = id;
			this.name = name;
			this.entries = entries;
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
				((uint)entries["Amount"].Value).SwapEndianness().ToString("X8"),
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
			Dictionary<string, TriggerTypeOption> triggerTypeOptions, TeamType noneTeam)
		{
			string[] split = data.Split(',');
			string tag;
			string name = split[0];
			TriggerTypeOption option;
			Dictionary<string, TriggerTypeEntry> entries = new Dictionary<string, TriggerTypeEntry>();
			object value;

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
			value = option.FindByString(split[5]);
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
			value = option.FindByIndex(int.Parse(split[10]));
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
