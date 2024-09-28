using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team7TextRPG.Utils;

namespace Team7TextRPG.Datas
{
    public class SaveData
    {
        public int Gold;
        public SavePlayerData PlayerData = new SavePlayerData();
    }

    public class SavePlayerData
    {
        public string? Name;
        public int Level;
        public int Exp;
        public int Hp;
        public int Mp;
        public Defines.SpeciesType SpeciesType;
        public Defines.SexType SexType;
        public Defines.JobType JobType;

        public int StatStr;
        public int StatDex;
        public int StatInt;
        public int StatLuck;
    }
}
