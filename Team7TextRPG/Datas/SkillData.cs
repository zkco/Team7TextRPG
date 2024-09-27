using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team7TextRPG.Utils;

namespace Team7TextRPG.Datas
{
    public class SkillData
    {
        public int DataId;
        public string? Name;
        public string? Description;
        public int RequiredLevel;
        public Defines.JobType RequiredJob;
        public int MpCost;
        public int LearnPrice;
        public Defines.SkillType SkillType;
        public Defines.SkillValueType ValueType;
        public int Value;
        public int TargetCount;
        public int HitCount;
    }
}
