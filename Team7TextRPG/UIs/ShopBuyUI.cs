using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team7TextRPG.UIs
{
    public class ShopBuyUI : UIBase
    {
        private readonly object item;

        public override void Write()
        {
            Write(item);
        }

        public override void Write(object items) 
        {
            Console.Clear();
            Console.WriteLine("■ 상 점 ■");
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
            Console.WriteLine("");
            Console.WriteLine("[아이템 목록]");
            Console.WriteLine("");

            for (int i = 0; i < Item.ItemBase; i++)
            {
                items[i].PrintItemStatDescription(false, i + 1, true); 
            }

            Console.WriteLine("");
            Console.WriteLine("0. 나가기");
            Console.WriteLine("1. 아이템 구매");
            Console.WriteLine("2. 아이템 판매");
            Console.WriteLine("");

        }
        protected override string EnumTypeToText<T>(T type)
        {
            throw new NotImplementedException();
        }

    }

}
