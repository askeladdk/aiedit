using System;
using System.IO;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace AIEdit
{
	using IniDictionary = Dictionary<string, OrderedDictionary>;

	public class IniParser
	{
		private OrderedDictionary keys;
		private StreamReader stream;	// input stream.
		private string section;			// name of the current section.

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="stream"></param>
		public IniParser(StreamReader stream)
		{
			this.stream = stream;
			keys = new OrderedDictionary();
			section = null;
		}

		/// <summary>
		/// Get the name of the current section AFTER calling Next() and BEFORE
		/// calling Skip() or Parse().
		/// </summary>
		public string Section { get { return section; } }

		public OrderedDictionary Table { get { return keys; } }

		/// <summary>
		/// Find the next section.
		/// </summary>
		/// <returns>True if the next section has been found, or false if EOF.</returns>
		public bool Next()
		{
			string line;

			keys.Clear();

			// eof
			if(stream.Peek() == -1) return false;

			if(section != null) return true;

			while( (line = stream.ReadLine()) != null )
			{
				//if(line.Length == 0) continue;
				section = ParseLine(line);
				if(section != null) return true;
			}
			return false;
		}

		/// <summary>
		/// Parse the section.
		/// </summary>
		public void Parse()
		{
			string line;

			while( (line = stream.ReadLine()) != null )
			{
				if(line.Length == 0) continue;
				
				if(line[0] == '[')
				{
					section = ParseLine(line);
					if(section != null) return;
				}
				else
				{
					if(!ParseKey(line)) return;
				}
			}
		}

		/// <summary>
		/// Skip the section.
		/// </summary>
		public void Skip()
		{
			string line;

			while( (line = stream.ReadLine()) != null )
			{
				// skip empty lines.
				if(line.Length == 0) continue;
				// start of section.
				if(line[0] == '[')
				{
					section = ParseLine(line);
					if(section != null) return;
				}
			}
		}

		private string ParseLine(string line)
		{
			int a;
			if(line.Length == 0) return null;
			// not start of a section.
			if(line[0] != '[') return null;
			// brackets don't match.
			if( (a = line.IndexOf(']', 1)) == -1 ) return null;
			// success
			return line.Substring(1, a - 1);
		}

		private bool ParseKey(string line)
		{
			string[] kv;
			char[] delim = {'='};
			int a;
            string k, v;

			a = line.IndexOf(';');
			// only comments in this line.
			if(a == 0) return true;
			// no comment.
			else if(a == -1) line = line.Trim();
			// cut the comment away.
			else line = line.Substring(0, a).Trim();

			// insert the key/value pair
			kv = line.Split(delim);

            k = kv[0].Trim();
            v = kv.Length == 1 ? k : kv[1].Trim();

            // check if key already exists.
            if (keys[k] == null) keys.Add(k, v);
			return true;
		}


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
					ini.Add(key, section);
				}
				// key=value pair
				else if(section != null)
				{
					if ((index = line.IndexOf('=')) == -1) continue;
					key = line.Substring(0, index);
					val = line.Substring(index + 1);
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
