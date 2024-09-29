using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team7TextRPG.Datas;
using Team7TextRPG.Utils;

namespace Team7TextRPG.Contents.Items
{
    public class ConsumableItem : ItemBase
    {
        public Defines.ConsumableType ConsumableType { get; private set; }

        public virtual int HpAmount { get; protected set; }
        public virtual int MpAmount { get; protected set; }

        public override void SetItemData(ItemData data)
        {
            base.SetItemData(data);
            ConsumableType = data.ConsumableType;
            HpAmount = data.HpAmount;
            MpAmount = data.MpAmount;
        }
    }
}
