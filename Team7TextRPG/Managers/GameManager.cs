using System.Xml.Linq;
using Team7TextRPG.Contents;
using Team7TextRPG.Contents.CasinoGame;
using Team7TextRPG.Contents.Items;
using Team7TextRPG.Creatures;
using Team7TextRPG.Datas;
using Team7TextRPG.Utils;
using static Team7TextRPG.Utils.Defines;

namespace Team7TextRPG.Managers
{
    internal class GameManager
    {
        private static GameManager? _instance;
        public static GameManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new GameManager();
                return _instance;
            }
        }
        // 플레이어 정보
        public PlayerCreature? Player { get; private set; }
        public List<ItemBase> PlayerItems { get; private set; } = new List<ItemBase>();
        public List<Skill> PlayerSkills => Player?.Skills ?? new List<Skill>();
        public SaveQuestData PlayerQuest { get; private set; } = new SaveQuestData(); // 완료한 퀘스트 목록, 중복 X

        // 게임 limit 요소
        public const int DeadLine = 50;
        public int CurrentDay { get; private set; } = 0;
        public int PlayerGold { get; private set; } //소지 중인 금액
        public int PlayerChip { get; private set; } //소지 중인 칩 갯수 

        public void CreatePlayer(string name, Defines.SexType sexType, Defines.SpeciesType specisType)
        {
            // 플레이어 생성
            Player = new PlayerCreature(name, sexType, specisType);
            // 플레이어 정보 설정
            Player.SetJob(Defines.JobType.Newbie);
            // 플레이어 초기화
            Player.Init();

        }
        public MonsterCreature? CreateMonster(int dataId)
        {
            // 몬스터 생성 (테스트용 3001)
            // 다음과 같이 사용
            // MonsterCreature? monster = GameManager.Instance.CreateMonster(3001);
            if (DataManager.Instance.MonsterDataDict.TryGetValue(dataId, out MonsterData? monsterData) == false)
                return null;

            MonsterCreature monster = new MonsterCreature();
            monster.SetMonsterData(monsterData);
            return monster;
        }
        public void LoadPlayer(SaveData saveData)
        {
            Player = new PlayerCreature(saveData.PlayerData.Name ?? "Unknown", saveData.PlayerData.SexType, saveData.PlayerData.SpeciesType);
            Player.SetLoadData(saveData.PlayerData);
            PlayerChip = saveData.Chip;
            PlayerGold = saveData.Gold;
            PlayerQuest = saveData.QuestData;
            PlayerItems = saveData.Items.Select(i =>
            {
                ItemBase? item = null;
                switch (i.ItemType)
                {
                    case Defines.ItemType.Equipment:
                        item = new EquipmentItem();
                        break;
                    case Defines.ItemType.Consumable:
                        item = new ConsumableItem();
                        break;
                    case Defines.ItemType.Quest:
                        item = new QuestItem();
                        break;
                }

                item?.SetSaveData(i);
                return item;
            }).Where(i => i != null).Select(i => i!).ToList();
        }

        public void AddGold(int gold)
        {
            PlayerGold += gold;
        }
        public void RemoveGold(int gold)
        {
            PlayerGold -= gold;
        }
        public bool HasGold(int gold)
        {
            return PlayerGold >= gold;
        }

        public void AddChip(int chip)
        {
            PlayerChip += chip;
        }
        public void RemoveChip(int chip)
        {
            PlayerChip -= chip;
        }

        public bool AddItem(int dataId)
        {
            if (DataManager.Instance.ItemDataDict.TryGetValue(dataId, out ItemData? itemData) == false)
                return false;

            return AddItem(itemData);
        }
        public bool AddItem(ItemData itemData)
        {
            // 아이템 추가 로직
            // 반환값 : 추가 되었는지 여부
            ItemBase? item = PlayerItems.FirstOrDefault(s => s.DataId == itemData.DataId);
            if (item == null)
            {
                // 가지고 있는 아이템이 없다면 생성
                switch (itemData.ItemType)
                {
                    case Defines.ItemType.Equipment:
                        item = new EquipmentItem();
                        break;
                    case Defines.ItemType.Consumable:
                        item = new ConsumableItem();
                        break;
                    case ItemType.Quest:
                        item = new QuestItem();
                        break;
                }

                item?.SetItemData(itemData);
                if (item != null)
                {
                    PlayerItems.Add(item);
                    return true;
                }

                return false;
            }
            else
            {
                // 가지고 있는 아이템이 있다면
                if (item.MaxCount > item.Count)
                {
                    item.AddCount(); // 수량 추가
                    QuestItemAdd(item.DataId); // 퀘스트 진행 체크
                    return true;
                }

                return false;
            }
        }
        public void RemoveItem(int dataId)
        {
            // 아이템 삭제 로직
            ItemBase? item = PlayerItems.FirstOrDefault(s => s.DataId == dataId);
            if (item != null)
                RemoveItem(item);
        }
        public void RemoveItem(ItemBase item)
        {
            // 아이템 삭제 로직
            item.RemoveCount();
            if (item.Count == 0) // 아이템 중첩 수가 0이면
                PlayerItems.Remove(item); // 항목에서 삭제
        }

        public void AddSkill(SkillData skillData)
        {
            Player?.AddSkill(skillData);
        }
        public void RemoveSkill(int dataId)
        {
            Player?.RemoveSkill(dataId);
        }
        public void RemoveSkill(Skill skill)
        {
            Player?.RemoveSkill(skill);
        }

        public bool IsEquippedItem(int dataId)
        {
            if (DataManager.Instance.ItemDataDict.TryGetValue(dataId, out ItemData? itemData) == false)
                return false;

            return itemData.ItemType == Defines.ItemType.Equipment && (Player?.EWeapon?.DataId == itemData.DataId
                || Player?.EArmor?.DataId == itemData.DataId
                || Player?.EAccessory?.DataId == itemData.DataId);
        }

        public QuestData[] GetAvailableQuests()
        {
            return DataManager.Instance.QuestDataDict.Values.Where(s =>
            s.RequiredLevel <= Player?.Level // 의뢰 가능한 레벨이고
            && (s.RequiredJobType == Utils.Defines.JobType.None || s.RequiredJobType == Player?.JobType)// 직업과 상관없는 의뢰 또는 직업과 일치하는 직업
            && PlayerQuest.CompletedQuests.Contains(s.DataId) == false) // 클리어하지 않은 퀘스트 
                .ToArray();
        }
        public void QuestAccept(int questDataId)
        {
            if (DataManager.Instance.QuestDataDict.TryGetValue(questDataId, out QuestData? questData) == false)
                return;

            PlayerQuest.CurrentQuestId = questDataId;
            PlayerQuest.QuestType = questData.QuestType;
            PlayerQuest.TargetId1 = questData.TargetDataId1;
            PlayerQuest.TargetId2 = questData.TargetDataId2;
            PlayerQuest.TargetAmount1 = questData.Amount1;
            PlayerQuest.TargetAmount2 = questData.Amount2;
            PlayerQuest.CurrentAmount1 = 0;
            PlayerQuest.CurrentAmount2 = 0;
        }
        public void QuestCancel()
        {
            PlayerQuest.CurrentQuestId = 0;
            PlayerQuest.QuestType = Defines.QuestType.None;
            PlayerQuest.TargetId1 = 0;
            PlayerQuest.TargetId2 = 0;
            PlayerQuest.TargetAmount1 = 0;
            PlayerQuest.TargetAmount2 = 0;
            PlayerQuest.CurrentAmount1 = 0;
            PlayerQuest.CurrentAmount2 = 0;
        }
        public void QuestClear(int questDataId)
        {
            if (PlayerQuest.CompletedQuests.Contains(questDataId) == false)
            {
                PlayerQuest.CompletedQuests.Add(questDataId);
                QuestReward(questDataId);
            }

            PlayerQuest.CurrentQuestId = 0;
            PlayerQuest.QuestType = Defines.QuestType.None;
            PlayerQuest.TargetId1 = 0;
            PlayerQuest.TargetId2 = 0;
            PlayerQuest.TargetAmount1 = 0;
            PlayerQuest.TargetAmount2 = 0;
            PlayerQuest.CurrentAmount1 = 0;
            PlayerQuest.CurrentAmount2 = 0;
        }
        public void QuestKillAdd(int monsterDataId)
        {
            if (DataManager.Instance.MonsterDataDict.TryGetValue(monsterDataId, out MonsterData? monsterData) == false)
                return;

            if (PlayerQuest.TargetId1 == monsterDataId)
                PlayerQuest.CurrentAmount1++;
            if (PlayerQuest.TargetId2 == monsterDataId)
                PlayerQuest.CurrentAmount2++;
        }
        public void QuestItemAdd(int itemDataId)
        {
            if (DataManager.Instance.ItemDataDict.TryGetValue(itemDataId, out ItemData? itemData) == false)
                return;

            if (PlayerQuest.TargetId1 == itemDataId)
                PlayerQuest.CurrentAmount1++;
            if (PlayerQuest.TargetId2 == itemDataId)
                PlayerQuest.CurrentAmount2++;
        }
        public bool IsQuestClear()
        {
            return PlayerQuest.CurrentAmount1 == PlayerQuest.TargetAmount1 && PlayerQuest.CurrentAmount2 == PlayerQuest.TargetAmount2;
        }
        public void QuestReward(int questDataId)
        {
            if (DataManager.Instance.QuestDataDict.TryGetValue(questDataId, out QuestData? questData) == false)
                return;

            if (DataManager.Instance.ItemDataDict.TryGetValue(questData.ItemRewardDataId1, out ItemData? itemData1))
            {
                if (AddItem(itemData1))
                    TextHelper.ItContent($"퀘스트 보상으로 {itemData1.Name}을 획득했습니다.");
                else
                    TextHelper.ItContent($"{itemData1.Name}을 더 이상 소지할 수 없습니다.");
            }

            if (DataManager.Instance.ItemDataDict.TryGetValue(questData.ItemRewardDataId2, out ItemData? itemData2))
            {
                if (AddItem(itemData2))
                    TextHelper.ItContent($"퀘스트 보상으로 {itemData2.Name}을 획득했습니다.");
                else
                    TextHelper.ItContent($"{itemData2.Name}을 더 이상 소지할 수 없습니다.");
            }

            if (DataManager.Instance.SkillDataDict.TryGetValue(questData.SkillRewardDataId, out SkillData? rewardSKillData))
            {
                if (Player?.AddSkill(rewardSKillData) ?? false)
                    TextHelper.ItContent($"퀘스트 보상으로 {rewardSKillData.Name} 스킬을 획득했습니다.");
                else
                    TextHelper.ItContent($"{rewardSKillData.Name} 스킬을 더 이상 배울 수 없습니다.");
            }
            if (questData.GoldReward > 0)
            {
                AddGold(questData.GoldReward);
                TextHelper.ItContent($"퀘스트 보상으로 {questData.GoldReward} Gold를 획득했습니다.");
            }

            if (questData.ExpReward > 0)
            {
                Player?.AddExp(questData.ExpReward);
                TextHelper.ItContent($"퀘스트 보상으로 {questData.ExpReward} Exp를 획득했습니다.");
            }
        }
        public bool IsQuestInProgress()
        {
            return PlayerQuest.CurrentQuestId != 0;
        }

        public List<MonsterData> GetMonsterDataList(BattleType battleType)
        {
            int playerLevel = battleType == BattleType.Field ? Player?.Level ?? 1 : Defines.MAX_PLAYER_LEVEL;
            return DataManager.Instance.BattleDataDict.Values
            // 현재 배틀 위치 기준으로 필터링
            .Where(s => s.BattleType == battleType)
            // 몬스터의 레벨이 플레이어 레벨보다 같거나 낮은 몬스터만 null이 아닌 인스턴스로 반환
            .Select(x => DataManager.Instance.MonsterDataDict.TryGetValue(x.MonsterDataId, out MonsterData? monsterData) && monsterData.Level <= playerLevel ? monsterData : null)
            // null 이 아닌것만 필터링
            .Where(s => s != null)
            // nullable을 nullable 이 아닌 타입으로 반환
            .Select(s => s!)
            .ToList();
        }
    }
}
