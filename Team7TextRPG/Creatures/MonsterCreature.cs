using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team7TextRPG.Contents;
using Team7TextRPG.Datas;
using Team7TextRPG.Managers;
using Team7TextRPG.Utils;

namespace Team7TextRPG.Creatures
{
    /// <summary>
    /// 몬스터의 정보를 미리 정의 해보는 추상 클래스입니다.
    /// </summary>
    public class MonsterCreature : CreatureBase
    {
        public int DataId { get; private set; }
        public string? Description { get; private set; }
        public Defines.MonsterType MonsterType { get; private set; }
        public int ExpReward { get; private set; }
        public int ItemReward { get; private set; }
        public int DropItemRate { get; private set; }
        public List<Skill> Skills { get; private set; } = new List<Skill>();
        public void SetMonsterData(MonsterData monsterData)
        {
            DataId = monsterData.DataId;
            Name = monsterData.Name;
            Level = monsterData.Level;
            Description = monsterData.Description;
            MonsterType = monsterData.MonsterType;
            BaseStat.StatStr = monsterData.StatStr;
            BaseStat.StatDex = monsterData.StatDex;
            BaseStat.StatInt = monsterData.StatInt;
            BaseStat.StatLuck = monsterData.StatLuck;
            BaseStat.MaxHp = monsterData.MaxHp;
            BaseStat.MaxMp = monsterData.MaxMp;
            BaseStat.Attack = monsterData.Attack;
            BaseStat.Defense = monsterData.Defense;
            BaseStat.Speed = monsterData.Speed;
            BaseStat.DodgeChanceRate = monsterData.DodgeChanceRate;
            BaseStat.CriticalChanceRate = monsterData.CriticalChanceRate;
            ExpReward = monsterData.ExpReward;
            ItemReward = monsterData.ItemReward;
            DropItemRate = monsterData.DropItemRate;
            AddSkill(monsterData.SkillDataId1);
            AddSkill(monsterData.SkillDataId2);
            AddSkill(monsterData.SkillDataId3);
            Hp = MaxHp;
        }

        private void AddSkill(int skillDataId)
        {
            if (DataManager.Instance.SkillDataDict.TryGetValue(skillDataId, out SkillData? skillData))
            {
                Skill skill = new Skill(this);
                skill.SetSkillData(skillData);
                Skills.Add(skill);
            }
        }

        public override void OnDamaged(int damage)
        {
            // 데미지를 받을 때
            Hp -= damage;
            if (Hp <= 0)
            {
                Hp = 0;
                OnDead();
            }
        }

        public override void OnDead()
        {
            // 사망했을 때, 필요없다면 제거합시다.
        }

        public override void OnHealed(int heal)
        {
            Hp += heal;
            if (Hp > MaxHp)
                Hp = MaxHp;
        }
    }
}
