using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team7TextRPG.Datas;
using Team7TextRPG.Utils;

namespace Team7TextRPG.Contents.Items
{
    public class EquipmentItem : ItemBase
    {
        public Defines.EquipmentType EquipmentType { get; protected set; }

        public override void SetItemData(ItemData data)
        {
            base.SetItemData(data);
            EquipmentType = data.EquipmentType;
        }

    }
}
