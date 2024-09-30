using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team7TextRPG.Contents;
using Team7TextRPG.Utils;

namespace Team7TextRPG.Datas
{
    [Serializable]
    public class SaveData
    {
        public int Gold;
        public int Chip;
        public SavePlayerData PlayerData = new SavePlayerData();
        public SaveQuestData QuestData = new SaveQuestData();
        public List<SaveItemData> Items = new List<SaveItemData>();
    }

    [Serializable]
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

        public int StatPointStr;
        public int StatPointDex;
        public int StatPointInt;
        public int StatPointLuck;
        public int StatPoint;

        public int EWeaponId;
        public int EArmorId;
        public int EAccessoryId;
    }

    [Serializable]
    public class SaveQuestData
    {
        public HashSet<int> CompletedQuests = new HashSet<int>();
        public int CurrentQuestId;
        public int Amount1;
        public int Amount2;
    }

    [Serializable]
    public class SaveItemData
    {
        public int DataId;
        public int Count;
        public int EnhancementLevel;
        public Defines.ItemType ItemType;
    }

    public class SavedMetaData
    {
        public string? Name;
        public DateTime SavedAt;
        public int Level;
        public Defines.JobType JobType;
        public int Seq;
        public string? FileName;
    }
}
