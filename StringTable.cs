using System;
using System.Collections.Generic;
using System.Collections;
using System.Collections.Specialized;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace AIEdit
{
    class StringTable
    {
        private OrderedDictionary param_types;
        private ArrayList action_types;
        private DoubleMap<int, string> group;
        private IniParser ip;

        public StringTable()
        {
            param_types = new OrderedDictionary();
            param_types["TargetTypes"] = new ArrayList();
            param_types["UnloadTypes"] = new ArrayList();
            param_types["MissionTypes"] = new ArrayList();
            param_types["FacingTypes"] = new ArrayList();
            param_types["TalkBubbleTypes"] = new ArrayList();
            param_types["NoTypes"] = new ArrayList();
            param_types["VeteranLevels"] = new ArrayList();
            param_types["MCDecisions"] = new ArrayList();
            param_types["Sides"] = new ArrayList();
            param_types["Operators"] = new ArrayList();
            param_types["Conditions"] = new ArrayList();
            param_types["Offsets"] = new ArrayList();
            param_types["Difficulties"] = new ArrayList();
            param_types["TeamTypeOptions"] = new ArrayList();
            param_types["TriggerTypeOptions"] = new ArrayList();

            param_types["BuildingTypes"] = new ArrayList();
            param_types["ScriptTypes"] = new ArrayList();
            param_types["TeamTypes"] = new ArrayList();
            
            group = new DoubleMap<int, string>();

            action_types = new ArrayList();
        }

        public ArrayList this[string type]
        {
            get
            {
                if (!param_types.Contains(type)) return null;
                return (ArrayList)param_types[type];
            }

            set
            {
                param_types[type] = value;
            }
        }

        public ArrayList ActionTypes { get { return action_types; } }
        public DoubleMap<int, string> Group { get { return group; } }

        public bool Load(string file)
        {
            string id;
            StreamReader stream;

            try
            {
                stream = new StreamReader(file);
            }
            catch
            {
                return false;
            }

            ip = new IniParser(stream);
            while (ip.Next())
            {
                id = ip.Section;
                // ActionTypes
                if (id.CompareTo("ActionTypes") == 0) ParseActionTypes();
                // Param Types
                else if (param_types.Contains(id)) ParseParamType(id);
                // Group
                else if (id.CompareTo("Group") == 0) ParseGroup();
                else ip.Skip();
            }

            stream.Close();

            return true;
        }

        private void ParseGroup()
        {
            int num;
            string text;
            IDictionaryEnumerator e;

            ip.Parse();
            e = ip.Table.GetEnumerator();
            while (e.MoveNext())
            {
                num = int.Parse((string)e.Key);
                text = (string)e.Value;
                group[num] = text;
            }
        }

        private void ParseParamType(string type)
        {
            ip.Parse();
            foreach (string s in ip.Table.Values) ((ArrayList)param_types[type]).Add(s);
        }

        private void ParseActionTypes()
        {
            string[] split;
            string name, type, desc;
            ScriptActionTypeOld sat;
            ip.Parse();

            foreach (string line in ip.Table.Values)
            {
                split = line.Split(',');
                name = split[0].Trim();
                type = split[1].Trim();
                desc = split[2].Trim();

                sat = new ScriptActionTypeOld();

                // name
                sat.Name = name;

                // input style
                if (param_types.Contains(type))
                {
                    sat.Style = ComboBoxStyle.DropDownList;
                    sat.List = (ArrayList)param_types[type];
                }
                else if (type.CompareTo("Number") == 0)
                {
                    sat.Style = ComboBoxStyle.Simple;
                    sat.List = null;
                }
                else
                {
                    sat.Style = ComboBoxStyle.DropDownList;
                    sat.List = (ArrayList)param_types["NoTypes"];
                }

                // description
                sat.Description = desc.Length == 0 ? name + "." : desc;

                action_types.Add(sat);
            }
        }
    }
}
