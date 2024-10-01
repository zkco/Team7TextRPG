using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team7TextRPG.Contents.Items;
using Team7TextRPG.Datas;
using Team7TextRPG.Managers;
using Team7TextRPG.Utils;

namespace Team7TextRPG.Contents
{
    public class Quest
    {
        private List<QuestData> _questList = new List<QuestData>();

        protected int pageSize = 5;
        protected int pageIndex = 0;

        public void NextPage()
        {
            pageIndex = Math.Min(pageIndex + 1, (_questList.Count - 1) / pageSize);
        }
        public void PrevPage()
        {
            pageIndex = Math.Max(pageIndex - 1, 0);
        }

        public void StartQuestProcess()
        {

            if (GameManager.Instance.IsQuestInProgress())
            {
                TextHelper.CtContent("이미 퀘스트를 수행중입니다.");
                InputManager.Instance.GetInputEnter("이전으로 돌아가려면 Enter키를 누르세요.");
                return;
            }

            _questList = GameManager.Instance.GetAvailableQuests().ToList();

            // 퀘스트 의뢰 받기
            while (true)
            {
                Console.Clear();
                // 인벤토리 표시
                TextHelper.ItHeader("수락 가능한 퀘스트");
                TextHelper.PageWrite($"현재 페이지: {pageIndex + 1}/{(_questList.Count - 1) / pageSize + 1}");
                TextHelper.ItContent("번호 | 이름 | 직업 | 설명");
                QuestData[] items = _questList.Skip(pageIndex * pageSize).Take(pageSize).ToArray();
                // 1. 인벤토리 아이템 목록 표시
                for (int i = 0; i < 5; i++)
                {
                    if (i <= items.Length - 1)
                    {
                        QuestData item = items[i];
                        TextHelper.ItContent($"{i + 1}. {item.Name} | {Util.JobTypeToString(item.RequiredJobType)} | {item.Description}");
                    }
                    else
                    {
                        TextHelper.ItContent($"{i + 1}. -");
                    }
                }
                ShowSelection();
                // 2. 아이템 사용 여부 확인
                // 3. 아이템 사용
                int input = InputManager.Instance.GetInputInt("번호를 입력하세요.",
                    (int)Defines.PagingSelectionType.None + 1,
                    (int)Defines.PagingSelectionType.Exit);

                switch ((Defines.PagingSelectionType)input)
                {
                    case Defines.PagingSelectionType.NextPage:
                        NextPage();
                        break;
                    case Defines.PagingSelectionType.PrevPage:
                        PrevPage();
                        break;
                    case Defines.PagingSelectionType.None:
                    case Defines.PagingSelectionType.Exit:
                        return;
                    default:
                        // 아이템 사용
                        QuestData selectedItem = items[input - 1];
                        if (UIManager.Instance.Confirm($"{selectedItem.Name}을 수락하시겠습니까?"))
                        {
                            GameManager.Instance.QuestAccept(selectedItem.DataId);
                            TextHelper.ItContent($"{selectedItem.Name}을 수락하였습니다.");
                            InputManager.Instance.GetInputEnter();
                            return;
                        }
                        break;
                }
            }
        }

        private void ShowSelection()
        {
            // 1. 아이템1
            // 2. 아이템2
            // 3. 아이템3
            // 4. 아이템4
            // 5. 아이템5
            // 6. 다음 페이지
            // 7. 이전 페이지
            // 8. 나가기
            foreach (var selection in Enum.GetValues(typeof(Defines.PagingSelectionType)).Cast<Defines.PagingSelectionType>())
            {
                if (selection == Defines.PagingSelectionType.None) continue;
                TextHelper.ItContent($"{(int)selection}. {Util.PagingSelectionTypeToString(selection)}");
            }
        }

        public void EndQuestProcess()
        {
            Console.Clear();
            // 퀘스트 보상 지급
            SaveQuestData quest = GameManager.Instance.PlayerQuest;
            TextHelper.BtHeader("수행중인 퀘스트");
            if (DataManager.Instance.QuestDataDict.TryGetValue(
                quest.CurrentQuestId, out QuestData? questData))
            {
                // 수락중인 퀘스트 있음
                // 퀘스트 제목
                TextHelper.CtHeader(questData.Name ?? "");
                // 퀘스트 내용
                TextHelper.CtContent(questData.Description ?? "");
                // 퀘스트 목표
                if (questData.QuestType == Defines.QuestType.Hunt)
                {
                    // ex) 사냥 목표 : 고블린 퇴치 (0/10)
                    if (DataManager.Instance.MonsterDataDict.TryGetValue(quest.TargetId1, out MonsterData? monsterData1))
                    {
                        TextHelper.ItHeader($"사냥 목표");
                        TextHelper.ItContent($" - {monsterData1.Name} 퇴치 ({quest.CurrentAmount1}/{quest.TargetAmount1})");
                    }

                    if (DataManager.Instance.MonsterDataDict.TryGetValue(quest.TargetId2, out MonsterData? monsterData2))
                        TextHelper.CtContent($" - {monsterData2.Name} 퇴치 ({quest.CurrentAmount2}/{quest.TargetAmount2})");
                }
                else if (questData.QuestType == Defines.QuestType.Gathering)
                {
                    // ex) 수집 목표 : 돌 수집 (0/10)
                    if (DataManager.Instance.ItemDataDict.TryGetValue(quest.TargetId1, out ItemData? itemData1))
                    {
                        TextHelper.ItHeader($"수집 목표");
                        TextHelper.ItContent($" - {itemData1.Name} 수집 ({quest.CurrentAmount1}/{quest.TargetAmount1})");
                    }

                    if (DataManager.Instance.ItemDataDict.TryGetValue(quest.TargetId2, out ItemData? itemData2))
                        TextHelper.CtContent($" - {itemData2.Name} 수집 ({quest.CurrentAmount2}/{quest.TargetAmount2})");
                }

                // 퀘스트 보상
                TextHelper.ItHeader($"완료 보상");
                if (DataManager.Instance.SkillDataDict.TryGetValue(questData.SkillRewardDataId, out SkillData? rewardSKillData))
                    TextHelper.ItContent($" - {rewardSKillData.Name}");

                if (DataManager.Instance.ItemDataDict.TryGetValue(questData.ItemRewardDataId1, out ItemData? rewardItemData))
                    TextHelper.ItContent($" - {rewardItemData.Name}");

                if (DataManager.Instance.ItemDataDict.TryGetValue(questData.ItemRewardDataId2, out ItemData? rewardItemData2))
                    TextHelper.ItContent($" - {rewardItemData2.Name}");

                if (questData.GoldReward > 0)
                    TextHelper.ItContent($" - {questData.GoldReward} Gold");

                if (questData.ExpReward > 0)
                    TextHelper.ItContent($" - {questData.ExpReward} Exp");

                if (GameManager.Instance.IsQuestClear())
                {
                    if (UIManager.Instance.Confirm("퀘스트 목표를 달성했습니다. 완료하고 보상을 받으시겠습니까?"))
                    {
                        GameManager.Instance.QuestClear(questData.DataId);
                        InputManager.Instance.GetInputEnter("이전으로 돌아가려면 Enter키를 누르세요.");
                    }
                }
                else
                {
                    TextHelper.CtContent("퀘스트 목표를 당설하지 못했습니다.");
                    if (UIManager.Instance.Confirm("현재 퀘스트를 포기하시겠습니까?"))
                    {
                        GameManager.Instance.QuestCancel();
                        TextHelper.CtContent("현재 수행중인 퀘스트를 포기했습니다.");
                    }

                    InputManager.Instance.GetInputEnter("이전으로 돌아가려면 Enter키를 누르세요.");
                }
            }
            else
            {
                // 수락중인 퀘스트 없음
                TextHelper.CtContent("수행중인 퀘스트가 없습니다.");
                InputManager.Instance.GetInputEnter("이전으로 돌아가려면 Enter키를 누르세요.");
            }
        }
    }
}
