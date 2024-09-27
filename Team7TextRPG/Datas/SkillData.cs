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
        public int DataId; // 스킬 데이터    아이디 ex) 1
        public string? Name; // 스킬 이름
        public string? Description; // 스킬 설명
        public int RequiredLevel; // 필요 레벨
        public Defines.JobType RequiredJob; // 필요 직업
        public int MpCost; // 소모 마나
        public int LearnPrice; // 배우는 가격
        public Defines.SkillType SkillType; // 스킬 타입 (Attack, Heal)
        public Defines.SkillValueType ValueType; // 스킬 값 타입 (Percent, Absolute)
        public int Value; // 스킬 값
        public int TargetCount; // 타겟 수
        public int HitCount; // 공격 횟수
    }
}
