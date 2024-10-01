using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team7TextRPG.Datas;
using Team7TextRPG.Utils;

namespace Team7TextRPG.Managers
{
    public class DataManager
    {
        private static DataManager? _instance;
        public static DataManager Instance => _instance ??= new DataManager();

        public Dictionary<int, ItemData> ItemDataDict = new Dictionary<int, ItemData>();
        public Dictionary<int, LevelData> LevelDataDict = new Dictionary<int, LevelData>();
        public Dictionary<int, MonsterData> MonsterDataDict = new Dictionary<int, MonsterData>();
        public Dictionary<int, NpcData> NpcDataDict = new Dictionary<int, NpcData>();
        public Dictionary<int, QuestData> QuestDataDict = new Dictionary<int, QuestData>();
        public Dictionary<int, ShopData> ShopDataDict = new Dictionary<int, ShopData>();
        public Dictionary<int, SkillData> SkillDataDict = new Dictionary<int, SkillData>();
        public Dictionary<int, BattleData> BattleDataDict = new Dictionary<int, BattleData>();

        public void Init()
        {
            // 프로그램 시작할때 반드시 실행되어야 함.
            LoadLevelData();
            LoadItemData();
            LoadMonsterData();
            LoadNpcData();
            LoadQuestData();
            LoadShopData();
            LoadSkillData();
            LoadBattleData();
        }

        private void LoadLevelData()
        {
            List<LevelData> data = DataTransfer.ParseExcelDataToList<LevelData>("Level");
            LevelDataDict.Clear();
            foreach (var item in data)
                LevelDataDict.Add(item.Level, item);
        }
        private void LoadItemData()
        {
            List<ItemData> data = DataTransfer.ParseExcelDataToList<ItemData>("Item");
            ItemDataDict.Clear();
            foreach (var item in data)
                ItemDataDict.Add(item.DataId, item);
        }
        private void LoadMonsterData()
        {
            List<MonsterData> data = DataTransfer.ParseExcelDataToList<MonsterData>("Monster");
            MonsterDataDict.Clear();
            foreach (var item in data)
                MonsterDataDict.Add(item.DataId, item);
        }
        private void LoadNpcData()
        {
            List<NpcData> data = DataTransfer.ParseExcelDataToList<NpcData>("Npc");
            NpcDataDict.Clear();
            foreach (var item in data)
                NpcDataDict.Add(item.DataId, item);
        }
        private void LoadQuestData()
        {
            List<QuestData> data = DataTransfer.ParseExcelDataToList<QuestData>("Quest");
            QuestDataDict.Clear();
            foreach (var item in data)
                QuestDataDict.Add(item.DataId, item);
        }
        private void LoadShopData()
        {
            List<ShopData> data = DataTransfer.ParseExcelDataToList<ShopData>("Shop");
            ShopDataDict.Clear();
            foreach (var item in data)
                ShopDataDict.Add(item.DataId, item);
        }
        private void LoadSkillData()
        {
            List<SkillData> data = DataTransfer.ParseExcelDataToList<SkillData>("Skill");
            SkillDataDict.Clear();
            foreach (var item in data)
                SkillDataDict.Add(item.DataId, item);
        }
        private void LoadBattleData()
        {
            List<BattleData> data = DataTransfer.ParseExcelDataToList<BattleData>("Battle");
            BattleDataDict.Clear();
            foreach (var item in data)
                BattleDataDict.Add(item.DataId, item);
        }
    }
}
