using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team7TextRPG.Contents;
using Team7TextRPG.Contents.Items;
using Team7TextRPG.Utils;

namespace Team7TextRPG.Creatures
{
    /// <summary>
    /// 플레이어 정보를 가지는 클래스입니다.
    /// </summary>
    public class PlayerCreature : CreatureBase
    {
        public int Exp { get; set; }
        public int MaxExp { get; private set; }

        public Stat ItemStat { get; private set; } = new Stat();

        public override int StatStr => BaseStat.StatStr + ItemStat.StatStr;
        public override int StatDex => BaseStat.StatDex + ItemStat.StatDex;
        public override int StatInt => BaseStat.StatInt + ItemStat.StatInt;
        public override int StatLuck => BaseStat.StatLuck + ItemStat.StatLuck;

        public override int MaxHp => BaseStat.MaxHp + ItemStat.MaxHp;
        public override int MaxMp => BaseStat.MaxMp + ItemStat.MaxMp;

        public override int Attack => BaseStat.Attack + ItemStat.Attack + CalcJobAttack();
        public override int Defense => BaseStat.Defense + ItemStat.Defense + CalcJobDefense();
        public override int Speed => BaseStat.Speed + ItemStat.Speed + CalcJobSpeed();
        public override double DodgeChanceRate => BaseStat.DodgeChanceRate + ItemStat.DodgeChanceRate + CalcJobDodgeChanceRate();
        public override double CriticalChanceRate => BaseStat.CriticalChanceRate + ItemStat.CriticalChanceRate + CalcJobCriticalChanceRate();

        public EquipmentItem? EWeapon { get; private set; }
        public EquipmentItem? EArmor { get; private set; }
        public EquipmentItem? EAccessory { get; private set; }

        public PlayerCreature(string name, Defines.SexType sexType, Defines.SpeciesType specisType)
        {
            this.Name = name;
            this.SpecisType = specisType;
            this.SexType = sexType;
        }

        public override void SetInfo(Defines.JobType job)
        {
            this.JobType = job;
            switch(JobType)
            {
                case Defines.JobType.Warrior:
                    BaseStat.StatStr = 10;
                    BaseStat.StatDex = 5;
                    BaseStat.StatInt = 5;
                    BaseStat.StatLuck = 5;
                    break;
            }
        }

        public void Equip(EquipmentItem equipment)
        {
            // 장비 장착
            OnEuipmentChanged();
        }

        public void UnEquip(EquipmentItem equipment)
        {
            // 장비 해제
            OnEuipmentChanged();
        }

        private void OnEuipmentChanged()
        {
            // 장비 변경 시 변화되는 스텟
            ItemStat.StatStr = (EWeapon?.ItemStat.StatStr ?? 0) + (EArmor?.ItemStat.StatStr ?? 0) + (EAccessory?.ItemStat.StatStr ?? 0);
            ItemStat.StatDex = (EWeapon?.ItemStat.StatDex ?? 0) + (EArmor?.ItemStat.StatDex ?? 0) + (EAccessory?.ItemStat.StatDex ?? 0);
            ItemStat.StatInt = (EWeapon?.ItemStat.StatInt ?? 0) + (EArmor?.ItemStat.StatInt ?? 0) + (EAccessory?.ItemStat.StatInt ?? 0);
            ItemStat.StatLuck = (EWeapon?.ItemStat.StatLuck ?? 0) + (EArmor?.ItemStat.StatLuck ?? 0) + (EAccessory?.ItemStat.StatLuck ?? 0);

            ItemStat.MaxHp = (EWeapon?.ItemStat.MaxHp ?? 0) + (EArmor?.ItemStat.MaxHp ?? 0) + (EAccessory?.ItemStat.MaxHp ?? 0);
            ItemStat.MaxMp = (EWeapon?.ItemStat.MaxMp ?? 0) + (EArmor?.ItemStat.MaxMp ?? 0) + (EAccessory?.ItemStat.MaxMp ?? 0);

            ItemStat.Attack = (EWeapon?.ItemStat.Attack ?? 0) + (EAccessory?.ItemStat.Attack ?? 0);
            ItemStat.Defense = (EArmor?.ItemStat.Defense ?? 0) + (EAccessory?.ItemStat.Defense ?? 0);
            ItemStat.Speed = (EWeapon?.ItemStat.Speed ?? 0) + (EAccessory?.ItemStat.Speed ?? 0);
            ItemStat.DodgeChanceRate = (EArmor?.ItemStat.DodgeChanceRate ?? 0) + (EAccessory?.ItemStat.DodgeChanceRate ?? 0);
            ItemStat.CriticalChanceRate = (EWeapon?.ItemStat.CriticalChanceRate ?? 0) + (EAccessory?.ItemStat.CriticalChanceRate ?? 0);
        }


        public override void LevelUp()
        {
            // 레벨업
        }

        public override void OnDamaged(int damage)
        {
            // 데미지를 받을 때
        }

        public override void OnHealed(int heal)
        {
            // 힐을 받을 때
        }

        public override void OnDead()
        {
            // 사망했을 때
        }
    }
}
