using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team7TextRPG.Utils;

namespace Team7TextRPG.Datas
{
    public class ItemData
    {
        public int DataId; // 데이터 아이디 ex) 1
        public string? Name; // 아이템 이름
        public string? Description; // 아이템 설명
        public int Price; // 가격
        public Defines.ItemType ItemType; // 아이템 타입 (Equipment, Consumable)
        public Defines.EquipmentType EquipmentType; // 장비 타입 (Weapon, Armor, Accessory)
        public Defines.ConsumableType ConsumableType; // 소비 타입 (Hp, Mp)
        public Defines.JobType RequiredJobType; // 요구 직업

        public int StatStr; // 힘 ex) 5
        public int StatDex; // 민첩 ex) 5
        public int StatInt; // 지능 ex) 5
        public int StatLuck; // 운 ex) 5

        public int MaxHp; // 최대 체력 ex) 100
        public int MaxMp; // 최대 마나 ex) 100

        public int Attack; // 공격력 ex) 10
        public int Defense; // 방어력 ex) 5
        public int Speed; // 속도 ex) 5
        public double DodgeChanceRate; // 회피 확률 ex) 0.1 (10%)
        public double CriticalChanceRate; // 치명타 확률 ex) 0.5 (50%)

        public int HpAmount; // 체력 회복량 ex) 10
        public int MpAmount; // 마나 회복량 ex) 10

        public string? DescText () => Description?
                .Replace("{StatStr}", StatStr.ToString())
                .Replace("{StatDex}", StatDex.ToString())
                .Replace("{StatInt}", StatInt.ToString())
                .Replace("{StatLuck}", StatLuck.ToString())
                .Replace("{MaxHp}", MaxHp.ToString())
                .Replace("{MaxMp}", MaxMp.ToString())
                .Replace("{Attack}", Attack.ToString())
                .Replace("{Defense}", Defense.ToString())
                .Replace("{Speed}", Speed.ToString())
                .Replace("{DodgeChanceRate}", $"{(DodgeChanceRate * 100):0.00}%")
                .Replace("{CriticalChanceRate}", $"{(CriticalChanceRate * 100):0.00}%")
                .Replace("{HpAmount}", HpAmount.ToString())
                .Replace("{MpAmount}", MpAmount.ToString());
    }
}
