using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Collections;
using System.Text;
using System.IO;
using System.Linq;

namespace AIEdit
{
	public interface IParamListEntry
	{
		uint ParamListIndex { get; }
	}

	public class ParamListEntry : IParamListEntry, IComparable
	{
		private string value;
		private uint index;

		public uint ParamListIndex { get { return index; } }

		public ParamListEntry(string value, uint index)
		{
			this.value = value;
			this.index = index;
		}

		public override string ToString()
		{
			return value;
		}

		public int CompareTo(object other)
		{
			return value.CompareTo((other as ParamListEntry).value);
		}
	}

	public enum ScriptParamType
	{
		Number,
		List,
		TechnoType,
		AIObject
	}

	public interface IActionType
	{
		uint Code { get; }
		string Name { get; }
		string Description { get; }
		IList List { get; }
		ScriptParamType ParamType { get; }
		string ParamToString(uint param);
		IParamListEntry ParamEntry(uint param);
	}

	public class ActionTypeNumber : IActionType, IComparable
	{
		private uint code;
		private string name, desc;

		public uint Code { get { return code; } }
		public string Name { get { return name; } }
		public string Description { get { return desc; } }
		public IList List { get { return null; } }
		public ScriptParamType ParamType { get { return ScriptParamType.Number; } }

		public string ParamToString(uint param)
		{
			return param.ToString();
		}

		public IParamListEntry ParamEntry(uint param)
		{
			return null;
		}

		public ActionTypeNumber(uint code, string name, string desc)
		{
			this.code = code;
			this.name = name;
			this.desc = desc;
		}

		public override string ToString()
		{
			return name;
		}

		public int CompareTo(object other)
		{
			return name.CompareTo((other as IActionType).Name);
		}
	}

	public class ActionTypeList : IActionType, IComparable
	{
		private uint code;
		private string name, desc;
		private IList list;
		ScriptParamType paramType;

		public uint Code { get { return code; } }
		public string Name { get { return name; } }
		public string Description { get { return desc; } }
		public IList List { get { return list; } }
		public ScriptParamType ParamType { get { return paramType; } }

		public IParamListEntry ParamEntry(uint param)
		{
			foreach (IParamListEntry entry in list)
			{
				if (entry.ParamListIndex == param) return entry;
			}
			return null;
		}

		public string ParamToString(uint param)
		{
			IParamListEntry entry = ParamEntry(param);
			return entry != null ? entry.ToString() : "<error>";
		}

		public ActionTypeList(uint code, string name, string desc, IList list, ScriptParamType paramType)
		{
			this.code = code;
			this.name = name;
			this.desc = desc;
			this.list = list;
			this.paramType = paramType;
		}

		public override string ToString()
		{
			return name;
		}

		public int CompareTo(object other)
		{
			return name.CompareTo((other as IActionType).Name);
		}
	}

	/// <summary>
	/// Script action.
	/// </summary>
	public class ScriptAction
	{
		private static uint[] offsets = { 0, 65536, 131072, 196608 };
		private static string[] offsetsDesc = { "Least Threat", "Most Threat", "Closest", "Farthest" };
		private IActionType action;
		private uint param, offset;

		public IActionType Action
		{
			get
			{
				return action;
			}
			set
			{
				if(action.List != value.List)
				{
					param  = 0;
					offset = 0;
				}
				action = value;
			}
		}

		public uint Param { get { return param; } set { param = value; } }
		public uint Offset { get { return offset; } set { offset = value; } }

		public string ParamString { get { return action.ParamToString(param); } }

		public string OffsetString
		{
			get
			{
				if (action.ParamType != ScriptParamType.TechnoType) return "";
				return offsetsDesc[offset];
			}
		}

		public IParamListEntry ParamEntry
		{
			get
			{
				return action.ParamEntry(param);
			}
		}

		public ScriptAction(IActionType action, uint param)
		{
			this.action = action;
			GetOffset(param, out this.param, out offset);
		}

		public void Write(StreamWriter stream, int index)
		{
			uint a = action.Code;
			uint p = param + offsets[offset];
			stream.WriteLine(index.ToString() + "=" + a.ToString() + "," + p.ToString());
		}

		private static void GetOffset(uint index, out uint param, out uint offset)
		{
			for (int i = offsets.Length - 1; i >= 0; i--)
			{
				if (index >= offsets[i])
				{
					param = index - offsets[i];
					offset = (uint)i;
					return;
				}
			}
			param = index;
			offset = 0;
		}

		public static string[] OffsetDescriptions()
		{
			return offsetsDesc;
		}
	};

	/// <summary>
	/// Script Type.
	/// </summary>
	public class ScriptType : IAIObject, IEnumerable<ScriptAction>, IParamListEntry
	{
		private List<ScriptAction> actions;
		private string name, id;
		private int uses;

		public uint ParamListIndex { get { return 0; } }

		public string Name { get { return name; } set { name = value.Trim(); } }
		public string ID { get { return id; } }
		public int Count { get { return actions.Count; } }
		public int Uses { get { return uses; } }
		public IList Actions { get { return actions; } }

		public void IncUses() { uses++; }
		public void DecUses() { uses--; }

		public IEnumerator<ScriptAction> GetEnumerator()
		{
			return actions.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public override string ToString()
		{
			return name;
		}

		public ScriptType(string id, string name, List<ScriptAction> actions=null)
		{
			this.id = id;
			this.name = name;
			this.actions = (actions == null) ? new List<ScriptAction>() : actions;
		}

		private int Swap(int a, int b)
		{
			ScriptAction tmp = actions[a];
			actions[a] = actions[b];
			actions[b] = tmp;
			return b;
		}

		public int MoveUp(int index)
		{
			if (index <= 0) return index;
			return Swap(index, index - 1);
		}

		public int MoveDown(int index)
		{
			if (index >= actions.Count - 1) return index;
			return Swap(index, index + 1);
		}

		public void Add(ScriptAction action)
		{
			actions.Add(action);
		}

		public void Insert(ScriptAction action, int index)
		{
			actions.Insert(index, action);
		}

		public void Remove(ScriptAction a)
		{
			actions.Remove(a);
		}

		public void Reset()
		{

		}

		public void Write(StreamWriter stream)
		{
			int n = 0;
			stream.WriteLine("[" + id + "]");
			stream.WriteLine("Name=" + name);
			foreach(ScriptAction a in actions) a.Write(stream, n++);
			stream.WriteLine();
		}

		public static ScriptType Parse(string id, OrderedDictionary section, List<IActionType> types)
		{
			int starti = 1;
			string name = section["Name"] as string;		
			List<ScriptAction> actions = new List<ScriptAction>();

			if (name == null)
			{
				starti = 0;
				name = id;
			}

			for (int i = starti; i < section.Count; i++)
			{
				string[] split = (section[i] as string).Split(',');
				int a  = int.Parse(split[0]);
				uint p = uint.Parse(split[1]);
				IActionType actionType = types[a];

				ScriptAction action = new ScriptAction(actionType, p);
				actions.Add(action);
			}

			return new ScriptType(id, name, actions);
		}
	}
}
