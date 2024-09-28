using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team7TextRPG.Utils;

namespace Team7TextRPG.Contents.Shops
{
    public class BlacksmithShop : ShopBase
    {
        public override Defines.ShopType ShopType { get; protected set; } = Defines.ShopType.Blacksmith;

        public override void Show()
        {
            // 대장간에서는 강화 컨텐츠를 제공한다.
            ShowPlayerItems();
        }
    }
}
