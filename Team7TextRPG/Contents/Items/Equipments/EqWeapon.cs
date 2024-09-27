using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team7TextRPG.Utils;

namespace Team7TextRPG.Contents.Items.Equipments
{
    public class EqWeapon : EquipmentBase
    {
        public override Defines.EquipmentType EquipmentType { get; protected set; } = Defines.EquipmentType.Weapon;

        public override string? Name { get; protected set; } = "목검";
        public override string? Description { get; protected set; } = "나무로 만든 검";
        public override int Price { get; protected set; } = 100;

        public virtual int Attack { get; protected set; }
        public virtual int Speed { get; protected set; }
        public virtual double CriticalChanceRate { get; protected set; }

    }
}
