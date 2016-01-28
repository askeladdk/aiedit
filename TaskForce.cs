using System;
using System.Collections;
using System.IO;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

namespace AIEdit
{
	public class TaskForceEntry
	{
		private TechnoType unit;
		private uint count;

		public TechnoType Unit { get { return unit; } set { unit = value; } }
		public string Name { get { return unit.Name; } }
		public uint Count { get { return count; } set { count = value; } }

		public uint Cost
		{
			get
			{
				return unit.Cost * count;
			}
		}

		public TaskForceEntry(TechnoType unit, uint count)
		{
			this.unit = unit;
			this.count = count;
		}

		public TaskForceEntry(TaskForceEntry other)
		{
			this.unit  = other.unit;
			this.count = other.count;
		}
	}

	public class TaskForce : IAIObject, IEnumerable<TaskForceEntry>
	{
		private string name, id;
		private AITypeListEntry group;
		private List<TaskForceEntry> units;
		private int uses;

		public string Name { get { return name; } set { name = value.Trim(); } }
		public string ID { get { return id; } }
		public int Uses { get { return uses; } }
		public AITypeListEntry Group { get { return group; } set { group = value; } }

		public void IncUses() { uses++; }
		public void DecUses() { uses--; }

		public IEnumerator<TaskForceEntry> GetEnumerator()
		{
			return units.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public override string ToString()
		{
			return name;
		}

		private TaskForceEntry GetEntry(TechnoType unit)
		{
			foreach(TaskForceEntry entry in units)
			{
				if (entry.Unit == unit) return entry;
			}
			return null;
		}

		private TaskForceEntry Add(TechnoType unit, uint count)
		{
			TaskForceEntry entry = new TaskForceEntry(unit, count);
			this.units.Add(entry);
			return entry;
		}

		public uint TotalCost()
		{
			uint cost = 0;
			foreach(TaskForceEntry entry in this.units)
			{
				cost += entry.Cost;
			}
			return cost;
		}

		public TaskForce(string id, string name, AITypeListEntry group, List<TaskForceEntry> units = null)
		{
			this.id = id;
			this.name = name;
			this.group = group;
			this.units = (units == null) ? new List<TaskForceEntry>() : units;
		}

		public IAIObject Copy(string newid, string newname)
		{
			List<TaskForceEntry> newunits = new List<TaskForceEntry>();
			foreach (TaskForceEntry e in units) newunits.Add(new TaskForceEntry(e));
			return new TaskForce(newid, newname, group, newunits);
		}

		public int Mod(TechnoType unit, int count)
		{
			TaskForceEntry entry = GetEntry(unit);
			if(entry == null)
			{
				if (count > 0)
				{
					Add(unit, (uint)count);
					return 1;
				}
				return 0;
			}

			count = Math.Max(0, count + (int)entry.Count);

			if (count == 0)
			{
				this.units.Remove(entry);
				return -1;
			}
			else
			{
				entry.Count = (uint)count;
				return 0;
			}
		}

		public int Set(TechnoType unit, uint count)
		{
			TaskForceEntry entry = GetEntry(unit);
			uint oldcount = (entry == null) ? 0 : entry.Count;
			return Mod(unit, (int)(count - oldcount));
		}

		public void Remove(TechnoType unit)
		{
			TaskForceEntry entry = GetEntry(unit);
			if (entry != null) this.units.Remove(entry);
		}

		public void Reset()
		{
			units.Clear();
		}

		public void Write(StreamWriter stream)
		{
			int n = 0;
			stream.WriteLine("[" + this.id + "]");
			stream.WriteLine("Name=" + this.name);

			foreach(TaskForceEntry entry in this.units)
			{
				stream.WriteLine(n + "=" + entry.Count + "," + entry.Unit.ID);
				n++;
			}
			stream.WriteLine("Group=" + this.group.Index);
			stream.WriteLine();
		}

		public static TaskForce Parse(string id, OrderedDictionary section,
			List<TechnoType> technoTypes, List<AITypeListEntry> groupTypes,
			Logger logger)
		{
			int starti = 1;
			string name = section.GetOrDefault("Name", null);
			List<TaskForceEntry> units = new List<TaskForceEntry>();
			TechnoType deftt = technoTypes[0] as TechnoType;
			AITypeListEntry group = groupTypes[0];

			if (section.Contains("Group"))
			{
				int groupi = int.Parse(section["Group"] as string);
				group = groupTypes.SingleOrDefault(g => g.Index == groupi);
				if (group == null) group = groupTypes[0];
			}
			else
			{
				logger.Add("Task Force " + id + " does not have a Group. Defaulting to \"" + group.Name + "\"!");
			}

			if (name == null)
			{
				starti = 0;
				name = id;
			}

			for (int i = starti; i < section.Count - 1; i++)
			{
				string[] split = (section[i] as string).Split(',');
				uint count = uint.Parse(split[0] as string);
				string unitid = split[1] as string;
				TechnoType tt = technoTypes.SingleOrDefault(t => t.ID == unitid);

				if (tt == null)
				{
					logger.Add("TechnoType " + unitid + " referenced by Task Force " + id + " does not exist!");
					tt = new TechnoType(unitid, unitid, 0, 0);
					technoTypes.Add(tt);
				}

				units.Add(new TaskForceEntry(tt, count));
			}

			return new TaskForce(id, name, group, units);
		}
	}
}
