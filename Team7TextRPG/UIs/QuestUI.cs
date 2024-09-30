using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team7TextRPG.Datas;
using Team7TextRPG.Managers;
using Team7TextRPG.Utils;

namespace Team7TextRPG.UIs
{
    // 의뢰를 수락한 퀘스트 표시
    public class QuestUI : UIBase
    {
        public override void Write()
        {
            Console.Clear();
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
            }
            else
            {
                // 수락중인 퀘스트 없음
                TextHelper.CtContent("수행중인 퀘스트가 없습니다.");
            }

            InputManager.Instance.GetInputEnter("이전으로 돌아가려면 Enter키를 누르세요.");
        }

        protected override string EnumTypeToText<T>(T type)
        {
            throw new NotImplementedException();
        }
    }
}
