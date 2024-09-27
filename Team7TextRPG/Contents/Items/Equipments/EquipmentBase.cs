using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team7TextRPG.Contents.Items;
using Team7TextRPG.Utils;

namespace Team7TextRPG.Contents.Items.Equipments
{
    public abstract class EquipmentBase : ItemBase
    {
        public abstract Defines.EquipmentType EquipmentType { get; protected set; }

        public int StatStr { get; protected set; }
        public int StatDex { get; protected set; }
        public int StatInt { get; protected set; }
        public int StatLuck { get; protected set; }

        public int MaxHp { get; protected set; }
        public int MaxMp { get; protected set; }

    }
}
