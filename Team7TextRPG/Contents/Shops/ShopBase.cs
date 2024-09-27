using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team7TextRPG.Datas;

namespace Team7TextRPG.Contents.Shops
{
    /// <summary>
    /// 상점의 정보를 미리 정의 해보는 추상 클래스입니다.
    /// </summary>
    public abstract class ShopBase
    {
        protected virtual List<ItemData> SaleItems { get; set; } = new List<ItemData>();

        public abstract void Show();
        public abstract void Buy();
        public abstract void Sell();
    }
}
