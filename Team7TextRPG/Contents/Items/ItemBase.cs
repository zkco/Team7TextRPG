using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team7TextRPG.Datas;
using Team7TextRPG.Managers;
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
        public int EnhancementLevel { get; protected set; } = 0; //처음에는 0강

        public virtual Stat ItemStat { get; protected set; } = new Stat();

        public virtual void SetItemData(ItemData data)
        {
            DataId = data.DataId;
            Name = data.Name;
            Description = data.DescText();
            ItemType = data.ItemType;
            Price = data.Price * (EnhancementLevel + 1);
            MaxCount = data.MaxCount;
            ItemStat.StatStr = CalcEnhancementValue(data.StatStr);
            ItemStat.StatDex = CalcEnhancementValue(data.StatDex);
            ItemStat.StatInt = CalcEnhancementValue(data.StatInt);
            ItemStat.StatLuck = CalcEnhancementValue(data.StatLuck);
            ItemStat.Attack = CalcEnhancementValue(data.Attack);
            ItemStat.Defense = CalcEnhancementValue(data.Defense);
            ItemStat.Speed = CalcEnhancementValue(data.Speed);
            ItemStat.DodgeChanceRate = CalcEnhancementValue(data.DodgeChanceRate);
            ItemStat.CriticalChanceRate = CalcEnhancementValue(data.CriticalChanceRate);
            ItemStat.MaxHp = CalcEnhancementValue(data.MaxHp);
            ItemStat.MaxMp = CalcEnhancementValue(data.MaxMp);
            Count = 1;
        }

        private int CalcEnhancementValue(int value)
        {
            if (value == 0) return 0;
            return (int)(value * EnhancementLevel * 1.2);
        }

        private double CalcEnhancementValue(double value)
        {
            if (value == 0) return 0;
            return value + EnhancementLevel * 1.2;
        }

        public virtual void SetSaveData(SaveItemData data)
        {
            if (DataManager.Instance.ItemDataDict.TryGetValue(data.DataId, out ItemData? dataItem))
            {
                EnhancementLevel = data.EnhancementLevel;
                SetItemData(dataItem);
            }
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
