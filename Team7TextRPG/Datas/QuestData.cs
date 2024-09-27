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
        public int NpcDataId;
        public Defines.QuestType QuestType;
        public string? Name;
        public string? Description;
        public int Order;
        public int TargetDataId1;
        public int Amount1;
        public int TargetDataId2;
        public int Amount2;
        public int ExpReward;
        public int GoldReward;
        public int ItemRewardDataId1;
        public int ItemRewardDataId2;
        public int RequiredLevel;
    }
}
