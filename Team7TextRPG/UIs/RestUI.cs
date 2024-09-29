using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team7TextRPG.Managers;
using Team7TextRPG.Utils;

namespace Team7TextRPG.UIs
{
    public class RestUI : UIBase
    {
        public override void Write()
        {
            TextHelper.BtHeader("휴식");

            if (UIManager.Instance.Confirm("200G 지불하고 휴식하시겠습니까?"))
            {
                if (GameManager.Instance.HasGold(200) == false)
                {
                    TextHelper.ItContent("골드가 부족합니다.");
                    Thread.Sleep(1000);
                    return;
                }

                GameManager.Instance.RemoveGold(200);
                GameManager.Instance.Player?.Rest();
                TextHelper.ItContent("체력과 마나가 모두 회복되었습니다.");
                TextHelper.ItContent("마을로 돌아갑니다.");
                Thread.Sleep(1000);
            }
        }

        protected override string EnumTypeToText<T>(T type)
        {
            return type switch
            {
                _ => "없음",
            };
        }
    }
}
