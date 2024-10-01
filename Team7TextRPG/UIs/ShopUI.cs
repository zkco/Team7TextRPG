using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team7TextRPG.Contents.Shops;
using Team7TextRPG.Managers;
using Team7TextRPG.Utils;

namespace Team7TextRPG.UIs
{
    public class ShopUI : UIBase
    {
        public Defines.ShopType ShopType { get; private set; }
        public ShopBase? Shop { get; private set; }
        public ShopUI(Defines.ShopType type)
        {
            ShopType = type;
            switch (type)
            {
                case Defines.ShopType.Weapon:
                    Shop = new WeaponShop();
                    break;
                case Defines.ShopType.Armor:
                    Shop = new ArmorShop();
                    break;
                case Defines.ShopType.Potion:
                    Shop = new PotionShop();
                    break;
                case Defines.ShopType.Accessory:
                    Shop = new AccessoryShop();
                    break;
                case Defines.ShopType.Blacksmith:
                    Shop = new BlacksmithShop();
                    break;
            }
        }

        public override void Write()
        {
            // 공통적인 로직이 필요하다면 Show 전에 처리
            Shop?.Show();
        }

        protected override string EnumTypeToText<T>(T type)
        {
            return String.Empty;
        }
    }
}
