using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.IO;

namespace AIEdit
{
    class HouseList
    {
        private ArrayList houses;

        public HouseList()
        {
            houses = new ArrayList();
            //houses.Add("<all>");
        }

        public void Read(string file)
        {
            StreamReader stream = new StreamReader(file);
            IniParser ip = new IniParser(stream);

            while(ip.Next())
            {
                if( (ip.Section.CompareTo("Countries") == 0) ||
					(ip.Section.CompareTo("Houses") == 0) )
                {
                    ip.Parse();
                    foreach (string s in ip.Table.Values) houses.Add(s);
					break;
                }
                else ip.Skip();
            }

            stream.Close();
        }

        public ArrayList Houses { get { return houses; } }
    }
}
