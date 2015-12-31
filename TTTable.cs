using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace AIEdit
{
    /// <summary>
    /// TeamType Table.
    /// </summary>
    class TTTable : AITableOld<TeamTypeOld>
    {
        public TTTable()
        {
        }

        private string MakeId(uint id)
        {
            return "0" + id.ToString("X") + "-G";
        }

        public TeamTypeOld NewTT(uint id, string name)
        {
            TeamTypeOld tt = new TeamTypeOld();
            tt.ID = MakeId(id);
            tt.Name = name;
            Add(tt.ID, tt);
            return tt;
        }

        public TeamTypeOld Copy(int index, uint id)
        {
            TeamTypeOld old = (TeamTypeOld)Table[index];
            TeamTypeOld newtt = new TeamTypeOld(old, MakeId(id));
            newtt.Name = newtt.Name + " COPY";
            Add(newtt.ID, newtt);
            return newtt;
        }

        public void Write(StreamWriter stream)
        {
            int n = 0;
            stream.WriteLine("[TeamTypes]");
            foreach (string id in Table.Keys)
            {
                stream.WriteLine(n.ToString() + "=" + id);
                n++;
            }

            stream.WriteLine();

            foreach (TeamTypeOld tt in Table.Values)
            {
                tt.Write(stream);
            }
        }

        public override void ParseSection(IniParser ip)
        {
            string id = ip.Section;
            TeamTypeOld tt = GetByID(id);

            if (tt == null)
            {
                ip.Skip();
                return;
            }

            ip.Parse();

            // String settings.
            tt.ID = id;
            tt.Name = (string)ip.Table["Name"];
            if (tt.Name == null) tt.Name = tt.ID;
            tt.ScriptType = (string)ip.Table["Script"];
            tt.TaskForce = (string)ip.Table["TaskForce"];
			tt.House = (string)ip.Table["House"];

            // Integer settings.
            tt.Max = ToInt((string)ip.Table["Max"]);
            tt.MCDecision = ToInt((string)ip.Table["MindControlDecision"]);
            tt.Priority = ToInt((string)ip.Table["Priority"]);
            tt.VeteranLevel = ToInt((string)ip.Table["VeteranLevel"]);
			tt.Group = ToInt( (string)ip.Table["Group"] );

            // Boolean settings.
            tt[TTSetting.Aggressive] = ToBool((string)ip.Table["Aggressive"]);
            tt[TTSetting.Annoyance] = ToBool((string)ip.Table["Annoyance"]);
            tt[TTSetting.AreTeamMembersRecruitable] = ToBool((string)ip.Table["AreTeamMembersRecruitable"]);
            tt[TTSetting.Autocreate] = ToBool((string)ip.Table["Autocreate"]);
            tt[TTSetting.AvoidThreats] = ToBool((string)ip.Table["AvoidThreats"]);
            tt[TTSetting.Droppod] = ToBool((string)ip.Table["Droppod"]);
            tt[TTSetting.Full] = ToBool((string)ip.Table["Full"]);
            tt[TTSetting.GuardSlower] = ToBool((string)ip.Table["GuardSlower"]);
            tt[TTSetting.IsBaseDefense] = ToBool((string)ip.Table["IsBaseDefense"]);
            tt[TTSetting.IonImmune] = ToBool((string)ip.Table["IonImmune"]);
            tt[TTSetting.Loadable] = ToBool((string)ip.Table["Loadable"]);
            tt[TTSetting.LooseRecruit] = ToBool((string)ip.Table["LooseRecruit"]);
            tt[TTSetting.OnlyTargetHouseEnemy] = ToBool((string)ip.Table["OnlyTargetHouseEnemy"]);
            tt[TTSetting.OnTransOnly] = ToBool((string)ip.Table["OnTransOnly"]);
            tt[TTSetting.Prebuild] = ToBool((string)ip.Table["Prebuild"]);
            tt[TTSetting.Recruiter] = ToBool((string)ip.Table["Recruiter"]);
            tt[TTSetting.Reinforce] = ToBool((string)ip.Table["Reinforce"]);
            tt[TTSetting.Suicide] = ToBool((string)ip.Table["Suicide"]);
            tt[TTSetting.TransportsReturnOnUnload] = ToBool((string)ip.Table["TransportsReturnOnUnload"]);
            tt[TTSetting.UseTransportOrigin] = ToBool((string)ip.Table["UseTransportOrigin"]);
            tt[TTSetting.Whiner] = ToBool((string)ip.Table["Whiner"]);

            MapName(tt.Name, tt.ID);
        }

        private int ToInt(string s) { return s != null ? int.Parse(s) : 0; }
        private bool ToBool(string s) { return s != null && s.CompareTo("yes") == 0; }

        public override string TypeList { get { return "TeamTypes"; } }
    }
}
