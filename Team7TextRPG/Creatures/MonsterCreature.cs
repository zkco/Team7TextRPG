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
            // 아이템을 떨군다.
            // 경험치를 플레이어에게 넘겨준다.
            // 퀘스트가 있으면 퀘스트에 킬 포인트 추가.
            GameManager.Instance.QuestKillAdd(DataId);
        }

        public override void OnHealed(int heal)
        {
            Hp += heal;
            if (Hp > MaxHp)
                Hp = MaxHp;
        }
    }
}
