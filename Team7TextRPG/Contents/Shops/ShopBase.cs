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
            Sell = 2
        }
        public int DataId { get; protected set; }
        protected List<ItemData> SaleItems { get; set; } = new List<ItemData>();
        public virtual Defines.ShopType ShopType { get; protected set; }

        protected int pageSize = 5;
        protected int pageIndex = 0;

        public ShopBase()
        {
            DataManager.Instance.ShopDataDict.Values.ToList().ForEach(shopData =>
            {
                if (shopData.ShopType == ShopType)
                {
                    this.DataId = shopData.DataId;
                    this.ShopType = shopData.ShopType;
                }
            });
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
            TextHelper.ItHeader($"현재 페이지: {pageIndex + 1}/{(SaleItems.Count - 1) / pageSize + 1}");
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
            Enum.GetValues(typeof(Defines.PagingSelectionType)).Cast<Defines.PagingSelectionType>().ToList().ForEach(selection =>
            {
                TextHelper.WriteLine($"{(int)selection}. {selection}");
            });
        }
        // 나열..
        public virtual void Buy(Defines.PagingSelectionType selection)
        {
            ItemData[] items = SaleItems.Skip(pageIndex * pageSize).Take(pageSize).ToArray();

            switch (selection)
            {
                case Defines.PagingSelectionType.NextPage:
                    NextPage();
                    return;
                case Defines.PagingSelectionType.PrevPage:
                    PrevPage();
                    return;
                case Defines.PagingSelectionType.Exit:
                case Defines.PagingSelectionType.None:
                    return;
                default:
                    if (selection >= Defines.PagingSelectionType.Item1 && selection <= Defines.PagingSelectionType.Item5)
                    {
                        int index = (int)selection - (int)Defines.PagingSelectionType.Item1;
                        if (index < items.Length)
                        {
                            ItemData item = items[index];
                            if (GameManager.Instance.PlayerGold < item.Price)
                            {
                                TextHelper.WriteLine("골드가 부족합니다.");
                                return;
                            }

                            GameManager.Instance.RemoveGold(item.Price);
                            GameManager.Instance.AddItem(item);
                            TextHelper.WriteLine($"{item.Name}을 구매했습니다.");
                        }
                    }
                    return;
            }
        }

        public virtual void Sell(Defines.PagingSelectionType selection)
        {

        }

        // 살때 목록
        protected virtual void ShowSaleItems()
        {
            TextHelper.ItContent(" 이름 | 가격 | 직업 | 설명");
            ItemData[] items = SaleItems.Skip(pageIndex * pageSize).Take(pageSize).ToArray();
            for (int i = 0; i < 5; i++)
            {
                if (i <= items.Length - 1)
                {
                    ItemData item = items[i];
                    string jobText = item.RequiredJobType == Defines.JobType.None ? "전체" : Util.JobTypeToString(item.RequiredJobType);
                    TextHelper.ItContent($"{i + 1}. {item.Name} | {item.Price} | {jobText} | {item.DescText}");
                }
                else
                {
                    TextHelper.ItContent($"{i + 1}. -");
                }
            }

            ShowSelection();
        }
        // 팔때 목록
        protected virtual void ShowPlayerItems()
        {
            TextHelper.ItContent(" 이름 | 가격 | 직업 | 설명");
            ItemBase[] items = GameManager.Instance.PlayerItems.Skip(pageIndex * pageSize).Take(pageSize).ToArray();
            for (int i = 0; i < 5; i++)
            {
                if (i <= items.Length - 1)
                {
                    ItemBase item = items[i];
                    string jobText = item.RequiredJobType == Defines.JobType.None ? "전체" : Util.JobTypeToString(item.RequiredJobType);
                    TextHelper.ItContent($"{i + 1}. {item.Name} | {item.Price} | {jobText} | {item.Description}");
                }
                else
                {
                    TextHelper.ItContent($"{i + 1}. -");
                }
            }

            ShowSelection();
        }
        // 구매 or 판매 선택
        public virtual void Show()
        {
            TextHelper.ItHeader($"{Util.ShopTypeToString(ShopType)}");
            TextHelper.ItContent("1. 구매");
            TextHelper.ItContent("2. 판매");
            while (true)
            {
                BuyOrSell input = InputManager.Instance.GetInputType<BuyOrSell>();
                switch (input)
                {
                    case BuyOrSell.Buy:
                        TextHelper.ItHeader(Util.ShopTypeToString(ShopType));
                        CurrentPage();
                        ShowSaleItems();
                        break;
                    case BuyOrSell.Sell:
                        TextHelper.ItHeader(Util.ShopTypeToString(ShopType));
                        CurrentPage();
                        ShowPlayerItems();
                        break;
                }
            }
        }
    }
}
