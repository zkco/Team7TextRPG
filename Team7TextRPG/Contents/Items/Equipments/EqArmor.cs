using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team7TextRPG.Utils;

namespace Team7TextRPG.Contents.Items.Equipments
{
    public class EqArmor : EquipmentBase
    {
        public override Defines.EquipmentType EquipmentType { get; protected set; } = Defines.EquipmentType.Weapon;

        public override string? Name { get; protected set; } = "가죽갑옷";
        public override string? Description { get; protected set; } = "가죽갑옷로 만든 갑옷";
        public override int Price { get; protected set; } = 200;

        public int Defense { get; protected set; }
        public double DodgeChanceRate { get; protected set; }
    }
}
