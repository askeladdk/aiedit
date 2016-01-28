using System;
using System.IO;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace AIEdit
{
	using IniDictionary = Dictionary<string, OrderedDictionary>;

	public class IniParser
	{
		/**
		 *  New parser reads .ini into dictionary.
		 */
		public static IniDictionary ParseToDictionary(StreamReader stream)
		{
			IniDictionary ini = new IniDictionary();
			OrderedDictionary section = null;
			string key, val, line;
			int index;
			int linenr = 0;

			while( (line = stream.ReadLine()) != null )
			{
				linenr++;
				// strip off whitespaces and comments
				line = line.TrimStart();
				if( (index = line.IndexOf(';')) != -1 )
				{
					line = line.Substring(0, index).TrimEnd();
				}

				// skip empty lines
				if(line.Length == 0)
				{
					continue;
				}
				// start of section
				else if(line[0] == '[')
				{
					if( (index = line.IndexOf(']')) == -1 ) continue;
					key = line.Substring(1, index - 1);
					section = new OrderedDictionary();
					if( !ini.ContainsKey(key) ) ini.Add(key, section);
				}
				// key=value pair
				else if(section != null)
				{
					if ((index = line.IndexOf('=')) == -1) continue;
					key = line.Substring(0, index).Trim();
					val = line.Substring(index + 1).Trim();
					section[key] = val;
				}
			}

			return ini;
		}

		public static IniDictionary ParseToDictionary(string path)
		{
			StreamReader reader = new StreamReader(path);
			IniDictionary d = ParseToDictionary(reader);
			reader.Close();
			return d;
		}
	}
}
