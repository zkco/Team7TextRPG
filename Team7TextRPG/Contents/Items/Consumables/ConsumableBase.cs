using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team7TextRPG.Contents.Items;
using Team7TextRPG.Utils;

namespace Team7TextRPG.Contents.Items.Consumables
{
    public abstract class ConsumableBase : ItemBase
    {
        public abstract Defines.ConsumableType ConsumableType { get; protected set; }

        public virtual int StatStr { get; }
        public virtual int StatDex { get; }
        public virtual int StatInt { get; }
        public virtual int StatLuck { get; }

        public virtual int MaxHp { get; }
        public virtual int MaxMp { get; }

        public virtual int Attack { get; }
        public virtual int Defense { get; }
        public virtual int Speed { get; }
        public virtual double CriticalChanceRate { get; }
        public virtual double DodgeChanceRate { get; }

        public virtual int HpAmount { get; }
        public virtual int MpAmount { get; }

    }
}
