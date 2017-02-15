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
		public static IniDictionary ParseToDictionary(StreamReader stream, string filename, Logger logger)
		{
			IniDictionary ini = new IniDictionary();
			OrderedDictionary section = null;
			string key, val, line;
			int index;
			int linenr = 0;
			string sectionKey = "";

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
					sectionKey = line.Substring(1, index - 1);
					section = new OrderedDictionary();
					if (!ini.ContainsKey(sectionKey))
					{
						ini.Add(sectionKey, section);
					}
					else
					{
                        logger.Add("Duplicate section [" + sectionKey + "] in " + filename + "!");
					}
				}
				// key=value pair
				else if(section != null)
				{
					if ((index = line.IndexOf('=')) == -1) continue;
					key = line.Substring(0, index).Trim();
					val = line.Substring(index + 1).Trim();

					if (section.Contains(key))
					{
                        logger.Add("Duplicate tag/index [" + sectionKey + "] => " + key + " in " + filename + "!");
					}

					section[key] = val;
				}
			}

			return ini;
		}

		public static IniDictionary ParseToDictionary(string path, Logger logger)
		{
			StreamReader reader = new StreamReader(path);
			string[] fullname = path.Split('\\');

			IniDictionary d = ParseToDictionary(reader, fullname[fullname.Length - 1], logger);
			reader.Close();
			return d;
		}
	}
}
