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
        public enum ShopSelectionType
        {
            None,
            Item1, Item2, Item3, Item4, Item5, // 아이템 5개씩 선택
            NextPage = 6,
            PrevPage = 7,
            Exit = 8
        }

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
            
            // 인벤토리 표시
            TextHelper.ItHeader("인벤토리");
            TextHelper.ItHeader($"현재 페이지: {pageIndex + 1}/{(GameManager.Instance.PlayerItems.Count - 1) / pageSize + 1}");
            TextHelper.ItHeader("번호 | 이름 | 직업 | 설명");
            ItemBase[] items = GameManager.Instance.PlayerItems.Skip(pageIndex * pageSize).Take(pageSize).ToArray();

            // 1. 인벤토리 아이템 목록 표시
            // 2. 아이템 사용 여부 확인
            // 3. 아이템 사용
        }

        protected override string EnumTypeToText<T>(T type)
        {
            throw new NotImplementedException();
        }
    }
}
