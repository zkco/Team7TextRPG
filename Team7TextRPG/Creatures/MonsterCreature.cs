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
            int expReward = ExpReward;   
            int goldReward = ItemReward; 
            GameManager.Instance.Player?.AddExp(expReward);
            GameManager.Instance.AddGold(goldReward);
            GameManager.Instance.QuestKillAdd(DataId);
            Console.WriteLine($"\n{Name} 을(를) 처치하여 {expReward} 경험치와 {goldReward} 골드를 얻었습니다!");
            Console.WriteLine("몬스터의 시체에서 안 긁어진 복권을 발견했다!");
            if(GameManager.Instance.PlayerItems.Exists(lottery => lottery.DataId == 1049 && lottery.Count == 99))
            {
                Console.WriteLine("가지고 있는 복권이 너무 많아 더 이상 주울 수 없다...");
            }
            else GameManager.Instance.AddItem(1049);
            Thread.Sleep(2000);
        }

        public override void OnHealed(int heal)
        {
            Hp += heal;
            if (Hp > MaxHp)
                Hp = MaxHp;
        }
    }
}
