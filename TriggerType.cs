using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;

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

    class TriggerType : IAIObject
    {
        private string name, id;
        private string teamtype, teamtype2, owner, techtype;
        private int side, techlevel, oper, amount, prob, probmax, probmin, condition;
        private bool[] options;

        public TriggerType()
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
        public TriggerType(TriggerType tt, string newid)
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

            amount = HexToInt(split[6], 0);
            oper = HexToInt(split[6], 8);

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

            stream.Write(ToBigEndianStr((uint)amount));
            stream.Write(ToBigEndianStr((uint)oper));
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

        private string ByteToHex(uint b)
        {
            if (b < 16) return "0" + (char)('0' + b);
            else return b.ToString("X");
        }

        private string ToBigEndianStr(uint hex)
        {
            string s = "";

            s += ByteToHex(hex & 0x000000ff);
            s += ByteToHex((hex & 0x0000ff00) >> 8);
            s += ByteToHex((hex & 0x00ff0000) >> 16);
            s += ByteToHex((hex & 0xff000000) >> 24);
            return s;
        }

        private int DecToInt(string s)
        {
            int dot = s.IndexOf('.');
            string s2 = s.Substring(0, dot);
            return int.Parse(s2);
        }

        private int HexToInt(char c)
        {
            if (c >= '0' && c <= '9') return (int)c - '0';
            else if (c >= 'A' && c <= 'F') return (int)c - 'A';
            else if (c >= 'a' && c <= 'f') return (int)c - 'a';
            else return 0;
        }

        private int HexToInt(string s, int start)
        {
            int total = 0, shift = 0;
            int max = start + 8;

            for (int i = start; i < max; i += 2)
            {
                total += ((HexToInt(s[i]) << 4) + HexToInt(s[i + 1])) << shift;
                shift += 8;
            }
            return total;
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
}

