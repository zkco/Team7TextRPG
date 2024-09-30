using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team7TextRPG.Contents.Items;
using Team7TextRPG.Managers;
using Team7TextRPG.Utils;

namespace Team7TextRPG.Contents.Shops
{
    public class BlacksmithShop : ShopBase
    {
        public override Defines.ShopType ShopType { get; protected set; } = Defines.ShopType.Blacksmith;

        public override void Show()
        {
            // 대장간에서는 강화 컨텐츠를 제공한다.
            while (true)
            {
                Console.Clear();
                TextHelper.ItHeader($"{Util.ShopTypeToString(ShopType)}");
                TextHelper.ItContent("1. 강화하기");
                TextHelper.ItContent("2. 나가기");
                int input = InputManager.Instance.GetInputInt();
                switch (input)
                {
                    case 1:
                        ShowEnhanceItems();
                        break;
                    case 2:
                        return;
                }
            }
        }

        private void ShowEnhanceItems()
        {
            // 강화할 아이템을 선택하고, 강화를 진행한다.
            while (true)
            {
                Console.Clear();
                UIManager.Instance.CommonStatusBar();
                TextHelper.ItHeader(Util.ShopTypeToString(ShopType));
                CurrentPage();
                TextHelper.ItContent("번호 | 이름 | 가격 | 직업 | 설명");
                ItemBase[] items = GameManager.Instance.PlayerItems.Skip(pageIndex * pageSize).Take(pageSize).ToArray();
                for (int i = 0; i < 5; i++)
                {
                    if (i <= items.Length - 1)
                    {
                        ItemBase item = items[i];
                        string jobText = item.RequiredJobType == Defines.JobType.None ? "전체" : Util.JobTypeToString(item.RequiredJobType);
                        bool equipped = GameManager.Instance.IsEquippedItem(item.DataId);
                        int price = Defines.ENHANCEMENT_COST * (item.EnhancementLevel + 1);
                        string enhanceText = item.EnhancementLevel == 0 ? "" : $"(+{item.EnhancementLevel})";
                        TextHelper.ItContent($"{i + 1}. {(equipped ? "[E]" : "")}{item.Name}{enhanceText} x{item.Count} | {price}G | {jobText} | {item.Description}");
                    }
                    else
                    {
                        TextHelper.ItContent($"{i + 1}. -");
                    }
                }


                ShowSelection();
                Defines.PagingSelectionType selection = InputManager.Instance.GetInputType<Defines.PagingSelectionType>();

                switch (selection)
                {
                    case Defines.PagingSelectionType.NextPage:
                        NextPage();
                        break;
                    case Defines.PagingSelectionType.PrevPage:
                        PrevPage();
                        break;
                    case Defines.PagingSelectionType.Exit:
                    case Defines.PagingSelectionType.None:
                        return;
                    default:
                        if (selection > 0 && selection < Defines.PagingSelectionType.NextPage)
                        {
                            int index = (int)selection - 1;
                            if (index < items.Length)
                            {
                                if (items[index].ItemType != Defines.ItemType.Equipment)
                                {
                                    TextHelper.WriteLine("장비가 아닌 아이템은 강화할 수 없습니다.");
                                    Thread.Sleep(1000);
                                    break;
                                }

                                EquipmentItem item = (EquipmentItem)items[index];
                                int price = Defines.ENHANCEMENT_COST * (item.EnhancementLevel + 1);
                                string enhanceText = item.EnhancementLevel == 0 ? "" : $"(+{item.EnhancementLevel})";

                                if (UIManager.Instance.Confirm($"{price}G를 지불하고 {item.Name}{enhanceText} 장비를 강화하시겠습니까?"))
                                {
                                    GameManager.Instance.RemoveGold(price);
                                    item.Enhance();
                                }
                            }
                        }
                        break;
                }
            }
        }
    }
}
