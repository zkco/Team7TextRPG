using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team7TextRPG.Datas;
using Team7TextRPG.Utils;
using static Team7TextRPG.Utils.Defines;

namespace Team7TextRPG.Contents.Items
{
    /// <summary>
    /// 아이템 정보를 미리 정의 해보는 추상 클래스입니다.
    /// </summary>
    public abstract class ItemBase
    {
        public int DataId { get; protected set; }
        public Defines.ItemType ItemType { get; protected set; }
        public string? Name { get; protected set; }
        public string? Description { get; protected set; }
        public int Price { get; protected set; }
        public Defines.JobType RequiredJobType { get; protected set; }
        public int Count { get; protected set; }
        public int MaxCount { get; protected set; }

        public virtual Stat ItemStat { get; protected set; } = new Stat();

        public virtual void SetItemData(ItemData data)
        {
            DataId = data.DataId;
            Name = data.Name;
            Description = data.DescText();
            ItemType = data.ItemType;
            Price = data.Price;
            MaxCount = data.MaxCount;
            ItemStat.StatStr = data.StatStr;
            ItemStat.StatDex = data.StatDex;
            ItemStat.StatInt = data.StatInt;
            ItemStat.StatLuck = data.StatLuck;
            ItemStat.Attack = data.Attack;
            ItemStat.Defense = data.Defense;
            ItemStat.Speed = data.Speed;
            ItemStat.DodgeChanceRate = data.DodgeChanceRate;
            ItemStat.CriticalChanceRate = data.CriticalChanceRate;
            ItemStat.MaxHp = data.MaxHp;
            ItemStat.MaxMp = data.MaxMp;
            Count = 1;
        }

        public virtual void Use()
        {
            Console.WriteLine($"아이템 {Name}을 사용했습니다.");
        }

        public virtual void AddCount(int count = 1)
        {
            Count += count;
            if (Count > MaxCount)
                Count = MaxCount;
        }

        public virtual void RemoveCount(int count = 1)
        {
            Count -= count;
            if (Count < 0)
                Count = 0;
        }
        public override string ToString()
        {
            return $"{Name} - {Description} - 가격: {Price}골드";
        }
    }
}
