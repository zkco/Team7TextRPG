using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team7TextRPG.Utils;

namespace Team7TextRPG.Contents.Items.Consumables
{
    public class CsPotion : ConsumableBase
    {
        public override Defines.ConsumableType ConsumableType { get; protected set; } = Defines.ConsumableType.Potion;
    }
}
