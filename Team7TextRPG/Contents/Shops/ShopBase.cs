using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team7TextRPG.Contents.Items;
using Team7TextRPG.Datas;
using Team7TextRPG.Managers;
using Team7TextRPG.Utils;

namespace Team7TextRPG.Contents.Shops
{
    /// <summary>
    /// 상점의 정보를 미리 정의 해보는 추상 클래스입니다.
    /// </summary>
    public abstract class ShopBase
    {
        protected enum BuyOrSell
        {
            Buy = 1,
            Sell = 2,
            Exit = 3,
        }
        protected List<ItemData> SaleItems { get; set; } = new List<ItemData>();
        public virtual Defines.ShopType ShopType { get; protected set; }

        protected int pageSize = 5;
        protected int pageIndex = 0;

        public ShopBase()
        {
            // 상점에 팔 아이템 데이터를 가져오기 위한 전처리
            foreach (var item in DataManager.Instance.ShopDataDict.Values)
            {
                if (item.ShopType == ShopType)
                {
                    if (DataManager.Instance.ItemDataDict.TryGetValue(item.ItemDataId, out ItemData? itemData) == false)
                        continue;

                    SaleItems.Add(itemData);
                }
            }
        }

        protected void NextPage()
        {
            pageIndex = Math.Min(pageIndex + 1, (SaleItems.Count - 1) / pageSize);
        }
        protected void PrevPage()
        {
            pageIndex = Math.Max(pageIndex - 1, 0);
        }
        protected void CurrentPage()
        {
            TextHelper.PageWrite($"현재 페이지: {pageIndex + 1}/{(SaleItems.Count - 1) / pageSize + 1}");
        }
        public void ShowSelection()
        {
            // 1. 아이템1
            // 2. 아이템2
            // 3. 아이템3
            // 4. 아이템4
            // 5. 아이템5
            // 6. 다음 페이지
            // 7. 이전 페이지
            // 8. 나가기
            foreach (var selection in Enum.GetValues(typeof(Defines.PagingSelectionType)).Cast<Defines.PagingSelectionType>())
            {
                if (selection == Defines.PagingSelectionType.None) continue;
                TextHelper.ItContent($"{(int)selection}. {Util.PagingSelectionTypeToString(selection)}");
            }
        }

        // 살때 목록
        protected virtual void ShowSaleItems()
        {
            while (true)
            {
                Console.Clear();
                UIManager.Instance.CommonStatusBar();
                TextHelper.ItHeader(Util.ShopTypeToString(ShopType));
                CurrentPage();
                TextHelper.ItContent("번호 | 이름 | 가격 | 직업 | 설명");
                ItemData[] items = SaleItems.Skip(pageIndex * pageSize).Take(pageSize).ToArray();
                for (int i = 0; i < 5; i++)
                {
                    if (i <= items.Length - 1)
                    {
                        ItemData item = items[i];
                        string jobText = item.RequiredJobType == Defines.JobType.None ? "전체" : Util.JobTypeToString(item.RequiredJobType);
                        bool equipped = GameManager.Instance.IsEquippedItem(item.DataId);
                        TextHelper.ItContent($"{i + 1}. {(equipped ? "[E]" : "")}{item.Name} | {item.Price}G | {jobText} | {item.DescText()}");
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
                                ItemData item = items[index];
                                if (GameManager.Instance.Gold < item.Price)
                                {
                                    TextHelper.WriteLine("골드가 부족합니다.");
                                    break;
                                }

                                if (GameManager.Instance.AddItem(item))
                                {
                                    GameManager.Instance.RemoveGold(item.Price);
                                    TextHelper.WriteLine($"{item.Name}을 구매했습니다.");
                                }
                                else
                                {
                                    TextHelper.WriteLine($"더 이상 아이템을 소지할 수 없습니다.");
                                    Thread.Sleep(1000);
                                }
                            }
                        }
                        break;
                }
            }
        }
        // 팔때 목록
        protected virtual void ShowPlayerItems()
        {
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
                        TextHelper.ItContent($"{i + 1}. {(equipped ? "[E]" : "")}{item.Name} x{item.Count} | {(int)(item.Price * 0.5)}G | {jobText} | {item.Description}");
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

                                ItemBase item = items[index];
                                if(GameManager.Instance.IsEquippedItem(item.DataId))
                                {
                                    TextHelper.WriteLine("착용중인 아이템은 판매할 수 없습니다.");
                                    Thread.Sleep(1000);
                                    break;
                                }
                                GameManager.Instance.AddGold((int)(item.Price * 0.5));
                                GameManager.Instance.RemoveItem(item);
                            }
                        }
                        break;
                }
            }
        }
        // 구매 or 판매 선택
        public virtual void Show()
        {
            while (true)
            {
                Console.Clear();
                TextHelper.ItHeader($"{Util.ShopTypeToString(ShopType)}");
                TextHelper.ItContent("1. 구매");
                TextHelper.ItContent("2. 판매");
                TextHelper.ItContent("3. 나가기");
                BuyOrSell input = InputManager.Instance.GetInputType<BuyOrSell>();
                switch (input)
                {
                    case BuyOrSell.Buy:
                        ShowSaleItems();
                        break;
                    case BuyOrSell.Sell:
                        ShowPlayerItems();
                        break;
                    case BuyOrSell.Exit:
                        return;
                }
            }
        }
    }
}
