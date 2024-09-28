using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Team7TextRPG.Utils.Defines;
using Team7TextRPG.Utils;
using Team7TextRPG.Managers;

namespace Team7TextRPG.UIs
{
    public class CommonUI : UIBase
    {
        public override void Write()
        {
            // 공통 UI 표시
            if (GameManager.Instance.Player == null)
            {
                TextHelper.WriteLine("플레이어 정보가 없습니다.");
                return;
            }

            int gold = GameManager.Instance.PlayerGold;

            string hpBar = GetHpBar(GameManager.Instance.Player.Hp, GameManager.Instance.Player.MaxHp);
            TextHelper.StatusBar($"체력바{hpBar} | 소지금 {gold}G");
            TextHelper.ItHeader(
                $"{EnumTypeToText(Defines.CommonUIType.Status)} : S | " +
                $"{EnumTypeToText(Defines.CommonUIType.Inventory)} : I | " +
                $"{EnumTypeToText(Defines.CommonUIType.Skill)} : K | " +
                $"{EnumTypeToText(Defines.CommonUIType.Quest)} : Q | "
            );
        }

        protected override string EnumTypeToText<T>(T type)
        {
            return type switch
            {
                Defines.CommonUIType.Status => "상태",
                Defines.CommonUIType.Inventory => "인벤토리",
                Defines.CommonUIType.Skill => "스킬",
                Defines.CommonUIType.Quest => "퀘스트",
                _ => "알 수 없음",
            };
        }

        private string GetHpBar(int hp, int maxHp)
        {
            int hpPercent = maxHp == 0 ? 0 : (int)Math.Ceiling((hp * 5.0) / maxHp);

            StringBuilder sb = new StringBuilder();
            sb.Append("(");
            for (int i = 0; i < 5; i++)
            {
                if (i < hpPercent)
                    sb.Append("■");
                else
                    sb.Append("□");
            }
            sb.Append(")");
            return sb.ToString();
        }
    }
}
