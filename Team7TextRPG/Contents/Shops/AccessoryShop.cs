using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team7TextRPG.Managers;
using Team7TextRPG.Utils;

namespace Team7TextRPG.Contents.Shops
{
    public class AccessoryShop : ShopBase
    {
        public override Defines.ShopType ShopType { get; protected set; } = Defines.ShopType.Accessory;

    }
}
