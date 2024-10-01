using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team7TextRPG.Utils;

namespace Team7TextRPG.Datas
{
    public class QuestData
    {
        public int DataId; // 데이터 아이디 ex) 1
        public int NpcDataId; // 의뢰 NPC 데이터 아이디
        public Defines.QuestType QuestType; // 퀘스트 타입 (Hunt, Gathering)
        public string? Name; // 퀘스트 이름
        public string? Description; // 퀘스트 설명
        public int Order; // 퀘스트 순서
        public int TargetDataId1; // 목표 데이터 아이디 1
        public int Amount1; // 목표 수량 1
        public int TargetDataId2; // 목표 데이터 아이디 2
        public int Amount2; // 목표 수량 2
        public int ExpReward; // 경험치 보상
        public int GoldReward; // 골드 보상
        public int ItemRewardDataId1; // 아이템 보상 데이터 아이디 1
        public int ItemRewardDataId2; // 아이템 보상 데이터 아이디 2
        public int RequiredLevel; // 필요 레벨
    }
}
