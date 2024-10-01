using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team7TextRPG.Creatures;
using Team7TextRPG.Datas;
using Team7TextRPG.Utils;

namespace Team7TextRPG.Contents
{
    public class Skill
    {
        public int DataId { get; protected set; }
        public CreatureBase Owner { get; protected set; }
        public virtual string? Name { get; protected set; }
        public virtual string? Description { get; protected set; }
        public int RequiredLevel { get; protected set; } // 필요 레벨
        public Defines.JobType RequiredJobType { get; protected set; } // 필요 직업
        public int MpCost { get; protected set; } // 소모 마나
        public int LearnPrice { get; protected set; } // 배우는 가격
        public Defines.SkillType SkillType { get; protected set; } // 스킬 타입 (Attack, Heal)
        public Defines.SkillValueType ValueType { get; protected set; } // 스킬 값 타입 (Percent, Absolute)
        public int Value { get; protected set; } // 스킬 값
        public int TargetCount { get; protected set; } // 타겟 수
        public int HitCount { get; protected set; } // 공격 횟수

        public Skill(CreatureBase owner)
        {
            Owner = owner;
        }

        public void SetSkillData(SkillData skillData)
        {
            DataId = skillData.DataId;
            Name = skillData.Name;
            Description = skillData.Description;
            RequiredLevel = skillData.RequiredLevel;
            RequiredJobType = skillData.RequiredJob;
            MpCost = skillData.MpCost;
            LearnPrice = skillData.LearnPrice;
            SkillType = skillData.SkillType;
            ValueType = skillData.ValueType;
            Value = skillData.Value;
            TargetCount = skillData.TargetCount;
            HitCount = skillData.HitCount;
        }

        public bool Use(CreatureBase[] targets)
        {
            if (Owner.Level < RequiredLevel)
            {
                TextHelper.WriteLine($"{Owner.Name}의 레벨이 부족합니다.");
                return false;
            }
            if (Owner.Mp < MpCost)
            {
                TextHelper.WriteLine($"{Owner.Name}의 MP가 부족합니다.");
                return false;
            }

            if (this.SkillType == Defines.SkillType.Attack)
            {
                foreach (var target in targets)
                {
                    int damage = 0;
                    if (this.ValueType == Defines.SkillValueType.Absolute)
                        damage = this.Value; // 절대값
                    else if (this.ValueType == Defines.SkillValueType.Percent)
                        damage = (int)(Owner.Attack * (this.Value / 100.0)); // 스킬 시전자 공격력의 퍼센트

                    target.OnDamaged(damage);
                }

                return true;
            }
            else if (this.SkillType == Defines.SkillType.Heal)
            {
                foreach (var target in targets)
                {
                    int heal = 0;
                    if (this.ValueType == Defines.SkillValueType.Absolute)
                        heal = this.Value; // 절대값
                    else if (this.ValueType == Defines.SkillValueType.Percent)
                        heal = (int)(Owner.MaxHp * (this.Value / 100.0)); // 스킬 시전자 최대 생명력의 퍼센트

                    target.OnHealed(heal);
                }

                return true;
            }

            return false;
        }
    }
}
