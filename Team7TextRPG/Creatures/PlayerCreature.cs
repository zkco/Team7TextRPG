using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team7TextRPG.Contents.Items.Equipments;
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

        public CreatureStat ItemStat { get; private set; } = new CreatureStat();

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

        public EqWeapon? EWeapon { get; private set; }
        public EqArmor? EArmor { get; private set; }
        public EqAccessory? EAccessory { get; private set; }

        public PlayerCreature(string name, Defines.SexType sexType, Defines.SpecisType specisType)
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

        public void Equip(EquipmentBase equipment)
        {
            // 장비 장착
            OnEuipmentChanged();
        }

        public void UnEquip(EquipmentBase equipment)
        {
            // 장비 해제
            OnEuipmentChanged();
        }

        private void OnEuipmentChanged()
        {
            // 장비 변경 시 변화되는 스텟
            ItemStat.StatStr = (EWeapon?.StatStr ?? 0) + (EArmor?.StatStr ?? 0) + (EAccessory?.StatStr ?? 0);
            ItemStat.StatDex = (EWeapon?.StatDex ?? 0) + (EArmor?.StatDex ?? 0) + (EAccessory?.StatDex ?? 0);
            ItemStat.StatInt = (EWeapon?.StatInt ?? 0) + (EArmor?.StatInt ?? 0) + (EAccessory?.StatInt ?? 0);
            ItemStat.StatLuck = (EWeapon?.StatLuck ?? 0) + (EArmor?.StatLuck ?? 0) + (EAccessory?.StatLuck ?? 0);

            ItemStat.MaxHp = (EWeapon?.MaxHp ?? 0) + (EArmor?.MaxHp ?? 0) + (EAccessory?.MaxHp ?? 0);
            ItemStat.MaxMp = (EWeapon?.MaxMp ?? 0) + (EArmor?.MaxMp ?? 0) + (EAccessory?.MaxMp ?? 0);

            ItemStat.Attack = (EWeapon?.Attack ?? 0) + (EAccessory?.Attack ?? 0);
            ItemStat.Defense = (EArmor?.Defense ?? 0) + (EAccessory?.Defense ?? 0);
            ItemStat.Speed = (EWeapon?.Speed ?? 0) + (EAccessory?.Speed ?? 0);
            ItemStat.DodgeChanceRate = (EArmor?.DodgeChanceRate ?? 0) + (EAccessory?.DodgeChanceRate ?? 0);
            ItemStat.CriticalChanceRate = (EWeapon?.CriticalChanceRate ?? 0) + (EAccessory?.CriticalChanceRate ?? 0);
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
