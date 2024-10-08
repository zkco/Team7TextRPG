using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Team7TextRPG.Contents;
using Team7TextRPG.Contents.Items;
using Team7TextRPG.Datas;
using Team7TextRPG.Managers;
using Team7TextRPG.Scenes;
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

        public int Gold { get; private set; } //소지 중인 금액
        public int Chip { get; private set; } //소지 중인 칩 갯수 
        public int ScratchedLottery { get; private set; } = 1; //게임 중 긁은 복권의 갯수

        public int StatPoint { get; set; }
        public int StatPointStr { get; set; } // 힘 포인트 투자량
        public int StatPointDex { get; set; } // 민첩 포인트 투자량
        public int StatPointInt { get; set; } // 지능 포인트 투자량
        public int StatPointLuck { get; set; } // 행운 포인트 투자량

        public override int StatStr => BaseStat.StatStr + ItemStat.StatStr;
        public override int StatDex => BaseStat.StatDex + ItemStat.StatDex;
        public override int StatInt => BaseStat.StatInt + ItemStat.StatInt;
        public override int StatLuck => BaseStat.StatLuck + ItemStat.StatLuck;

        public override int MaxHp => BaseStat.MaxHp + ItemStat.MaxHp + CalcJobMaxHp();
        public override int MaxMp => BaseStat.MaxMp + ItemStat.MaxMp + CalcJobMaxMp();

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

        public void SetJob(Defines.JobType job)
        {
            this.JobType = job;
        }

        public void Init()
        {
            // 처음 캐릭터 생성시에만 사용
            this.SetLevel(1);
            this.Exp = 0;
            this.Hp = MaxHp;
            this.Mp = MaxMp;

            // 치트
            GameManager.Instance.AddGold(-90000);
            GameManager.Instance.AddChip(100);
        }

        public void SetLoadData(SavePlayerData saveData)
        {
            // 저장 데이터 로드시 사용할 예정
            SetLevel(saveData.Level);

            Chip = saveData.Chip;
            Gold = saveData.Gold;
            ScratchedLottery = saveData.ScratchedLottery;
            StatPointStr = saveData.StatPointStr;
            StatPointDex = saveData.StatPointDex;
            StatPointInt = saveData.StatPointInt;
            StatPointLuck = saveData.StatPointLuck;
            StatPoint = saveData.StatPoint;

            Exp = saveData.Exp;
            JobType = saveData.JobType;
            SexType = saveData.SexType;
            SpecisType = saveData.SpeciesType;
            Hp = saveData.Hp;
            Mp = saveData.Mp;
        }

        protected override void SetLevel(int level)
        {
            base.SetLevel(level);
            LevelData levelData = DataManager.Instance.LevelDataDict[level];
            MaxExp = levelData.MaxExp;
            StatPoint += levelData.BonusPoint;
        }

        public void AddStatPoint(Defines.StatPointType type)
        {
            // 스텟 포인트 추가
            if (StatPoint <= 0)
                return;

            if (type == Defines.StatPointType.StatStr)
            {
                StatPointStr++;
                StatPoint--;
            }
            else if (type == Defines.StatPointType.StatDex)
            {
                StatPointDex++;
                StatPoint--;
            }
            else if (type == Defines.StatPointType.StatInt)
            {
                StatPointInt++;
                StatPoint--;
            }
            else if (type == Defines.StatPointType.StatLuck)
            {
                StatPointLuck++;
                StatPoint--;
            }
        }

        public void EquipItem(EquipmentItem equipment)
        {
            // 장비 장착
            
            UnEquipItem(equipment.EquipmentType);
            if (equipment.EquipmentType == Defines.EquipmentType.Weapon)
                EWeapon = equipment;
            else if (equipment.EquipmentType == Defines.EquipmentType.Armor)
                EArmor = equipment;
            else if (equipment.EquipmentType == Defines.EquipmentType.Accessory)
                EAccessory = equipment;

            OnEuipmentChanged();
        }

        public void UnEquipItem(Defines.EquipmentType equipmentType)
        {
            // 장비 해제

            if (equipmentType == Defines.EquipmentType.Weapon)
                EWeapon = null;
            else if (equipmentType == Defines.EquipmentType.Armor)
                EArmor = null;
            else if (equipmentType == Defines.EquipmentType.Accessory)
                EAccessory = null;

            OnEuipmentChanged();
        }

        public void UseItem(ConsumableItem item)
        {
            // 아이템 사용
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

        public void AddExp(int exp)
        {
            // 경험치 획득
            Exp += exp;
            while (Exp >= MaxExp)
            {
                // 경험치 누적된 걸로 계속 레벨업
                Exp -= MaxExp;
                LevelUp();
            }
        }
        public override void LevelUp()
        {
            // 레벨업
            SetLevel(Level + 1);
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

        public override void OnHealed(int heal)
        {
            // 힐을 받을 때
            Hp += heal;
            if (Hp > MaxHp)
                Hp = MaxHp;
        }

        public override void OnDead()
        {
           
        }

        public void Rest()
        {
            Hp = MaxHp;
            Mp = MaxMp;
        }



        public void AddGold(int gold)
        {
            Gold += gold;
        }
        public void RemoveGold(int gold)
        {
            Gold -= gold;
        }
        public bool HasGold(int gold)
        {
            return Gold + Math.Abs(Defines.ENDING_BANKRUPTCY_CONDITION) >= gold;
        }

        public void AddChip(int chip)
        {
            Chip += chip;
        }
        public void RemoveChip(int chip)
        {
            Chip -= chip;
        }
        public void AddScratchedLottery()
        {
            ScratchedLottery++;
        }
    }
}
