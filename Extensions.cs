using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using BrightIdeasSoftware;

namespace AIEdit
{
	public static class Extensions
	{
		public static T Get<T>(this IOrderedDictionary dict, string key) where T:class
		{
			return dict[key] as T;
		}

		public static string GetString(this IOrderedDictionary dict, string key)
		{
			return dict[key] as string;
		}

		public static int GetInt(this IOrderedDictionary dict, string key)
		{
			return int.Parse(dict[key] as string);
		}

		public static uint GetUint(this IOrderedDictionary dict, string key)
		{
			try
			{
				return uint.Parse(dict[key] as string);
			}
			catch(OverflowException)
			{
				return 0;
			}
		}

		public static bool GetBool(this IOrderedDictionary dict, string key)
		{
			return GetString(dict, key).CompareTo("yes") == 0;
		}

		public static AITypeListEntry GetAITypeListEntryByIndex(this IList list, int index)
		{
			foreach(AITypeListEntry entry in list)
			{
				if (entry.Index == index) return entry;
			}
			return null;
		}

		public static uint SwapEndianness(this uint x)
		{
			return ((x & 0x000000ff) << 24) |
				   ((x & 0x0000ff00) <<  8) |
				   ((x & 0x00ff0000) >>  8) |
				   ((x & 0xff000000) >> 24);
		}

		public static void EnsureVisible(this ObjectListView olv)
		{
			olv.EnsureVisible(olv.SelectedIndex);
		}
	}
}
