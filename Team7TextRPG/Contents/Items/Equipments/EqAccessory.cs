using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team7TextRPG.Utils;

namespace Team7TextRPG.Contents.Items.Equipments
{
    public class EqAccessory : EquipmentBase
    {
        public override Defines.EquipmentType EquipmentType { get; protected set; } = Defines.EquipmentType.Accessory;

        public override string? Name { get; protected set; } = "녹슨 반지";
        public override string? Description { get; protected set; } = "녹슬어 버린 반지";
        public override int Price { get; protected set; } = 500;

        public int Attack { get; protected set; }
        public int Defense { get; protected set; }
        public int Speed { get; protected set; }
        public double CriticalChanceRate { get; protected set; }
        public double DodgeChanceRate { get; protected set; }
    }
}
