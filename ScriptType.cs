using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Collections;
using System.Text;
using System.IO;
using System.Linq;
using System.Windows.Forms;

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
		NumPlusMinus,
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
		string ParamToString(int param);
		IParamListEntry ParamEntry(int param);
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

		public string ParamToString(int param)
		{
			return param.ToString();
		}

		public IParamListEntry ParamEntry(int param)
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

	public class ActionTypeNumPlusMinus : IActionType, IComparable
	{
		private uint code;
		private string name, desc;

		public uint Code { get { return code; } }
		public string Name { get { return name; } }
		public string Description { get { return desc; } }
		public IList List { get { return null; } }
		public ScriptParamType ParamType { get { return ScriptParamType.NumPlusMinus; } }

		public string ParamToString(int param)
		{
			return param.ToString();
		}

		public IParamListEntry ParamEntry(int param)
		{
			return null;
		}

		public ActionTypeNumPlusMinus(uint code, string name, string desc)
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

		public IParamListEntry ParamEntry(int param)
		{
			foreach (IParamListEntry entry in list)
			{
				if (entry.ParamListIndex == (uint)param) return entry;
			}
			return null;
		}

		public string ParamToString(int param)
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
		private static int[] offsets = { 0, 65536, 131072, 196608 };
		private static string[] offsetsDesc = { "Least Threat", "Most Threat", "Closest", "Farthest" };
		private IActionType action;
		private int param, offset;

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

		public int Param { get { return param; } set { param = value; } }
		public int Offset { get { return offset; } set { offset = value; } }

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

		public ScriptAction(IActionType action, int param)
		{
			this.action = action;
			GetOffset(param, out this.param, out offset);
		}

		public ScriptAction(ScriptAction other)
		{
			this.action = other.action;
			this.param  = other.param;
			this.offset = other.offset;
		}

		public void Write(StreamWriter stream, int index)
		{
			uint a = action.Code;
			int p = param + offsets[offset];
			stream.WriteLine(index.ToString() + "=" + a.ToString() + "," + p.ToString());
		}

		private static void GetOffset(int index, out int param, out int offset)
		{
			for (int i = offsets.Length - 1; i >= 0; i--)
			{
				if (index >= offsets[i])
				{
					param = index - offsets[i];
					offset = i;
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
			this.Name = name;
			this.actions = (actions == null) ? new List<ScriptAction>() : actions;
		}

		public IAIObject Copy(string newid, string newname)
		{
			List<ScriptAction> newactions = new List<ScriptAction>();
			foreach (ScriptAction a in this.actions) newactions.Add(new ScriptAction(a));
			return new ScriptType(newid, newname, newactions);
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

		public static ScriptType Parse(string id, OrderedDictionary section, List<IActionType> types,
			Logger logger)
		{
			string name = id;		
			List<ScriptAction> actions = new List<ScriptAction>();

			try
			{
				foreach(DictionaryEntry entry in section)
				{
					if ((entry.Key as string) == "Name")
					{
						name = entry.Value as string;
					}
					else
					{
						string[] split = (entry.Value as string).Split(',');

						if (split.Length < 2)
						{
							logger.Add("ScriptType " + id + ": Entry not in format: <Index>=<Action>,<Parameter>");
						}
						else
						{
							int a  = int.Parse(split[0]);
							int p = int.Parse(split[1]);
							IActionType actionType = types[a];

							ScriptAction action = new ScriptAction(actionType, p);
							actions.Add(action);
						}
					}
				}
			}
			catch (Exception )
			{
				string msg = "Error occured at ScriptType: [" + id + "]" + "\nPlease verify its format. Application will now close.";
				MessageBox.Show(msg, "Parse Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1); 
				Application.Exit();
			}

			return new ScriptType(id, name, actions);
		}
	}
}
