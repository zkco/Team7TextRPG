using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team7TextRPG.UIs;

namespace Team7TextRPG.UIs
{
    public class InventoryUI : UIBase
    {
        public override void Write()
        {
            // 인벤토리 표시
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
