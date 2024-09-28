using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team7TextRPG.Contents.Items;
using Team7TextRPG.Datas;
using Team7TextRPG.Managers;
using Team7TextRPG.UIs;
using Team7TextRPG.Utils;

namespace Team7TextRPG.UIs
{
    public class InventoryUI : UIBase
    {
        protected int pageSize = 5;
        protected int pageIndex = 0;

        public void NextPage()
        {
            pageIndex = Math.Min(pageIndex + 1, (GameManager.Instance.PlayerItems.Count - 1) / pageSize);
        }
        public void PrevPage()
        {
            pageIndex = Math.Max(pageIndex - 1, 0);
        }

        public override void Write()
        {
            while (true)
            {
                Console.Clear();
                // 인벤토리 표시
                TextHelper.ItHeader("인벤토리");
                TextHelper.PageWrite($"현재 페이지: {pageIndex + 1}/{(GameManager.Instance.PlayerItems.Count - 1) / pageSize + 1}");
                TextHelper.ItContent("번호 | 이름 | 직업 | 설명");
                ItemBase[] items = GameManager.Instance.PlayerItems.Skip(pageIndex * pageSize).Take(pageSize).ToArray();
                // 1. 인벤토리 아이템 목록 표시
                for (int i = 0; i < 5; i++)
                {
                    if (i <= items.Length - 1)
                    {
                        ItemBase item = items[i];
                        string jobText = item.RequiredJobType == Defines.JobType.None ? "전체" : Util.JobTypeToString(item.RequiredJobType);
                        TextHelper.ItContent($"{i + 1}. {item.Name} | {jobText} | {item.Description}");
                    }
                    else
                    {
                        TextHelper.ItContent($"{i + 1}. -");
                    }
                }
                ShowSelection();
                // 2. 아이템 사용 여부 확인
                // 3. 아이템 사용
                int input = InputManager.Instance.GetInputInt("번호를 입력하세요.",
                    (int)Defines.PagingSelectionType.None + 1,
                    (int)Defines.PagingSelectionType.Exit);

                switch ((Defines.PagingSelectionType)input)
                {
                    case Defines.PagingSelectionType.NextPage:
                        NextPage();
                        break;
                    case Defines.PagingSelectionType.PrevPage:
                        PrevPage();
                        break;
                    case Defines.PagingSelectionType.None:
                    case Defines.PagingSelectionType.Exit:
                        return;
                    default:
                        // 아이템 사용
                        ItemBase selectedItem = items[input - 1];
                        if (selectedItem.ItemType == Defines.ItemType.Equipment)
                            GameManager.Instance.Player?.EquipItem((EquipmentItem)selectedItem);
                        else if (selectedItem.ItemType == Defines.ItemType.Consumable)
                            GameManager.Instance.Player?.UseItem((ConsumableItem)selectedItem);
                        break;
                }
            }
        }

        protected override string EnumTypeToText<T>(T type)
        {
            throw new NotImplementedException();
        }

        private void ShowSelection()
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
    }
}
