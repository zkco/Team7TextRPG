using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team7TextRPG.Creatures;
using Team7TextRPG.Managers;
using Team7TextRPG.UIs;
using Team7TextRPG.Utils;

namespace Team7TextRPG.UIs
{
    public class StatusUI : UIBase
    {
        public override void Write()
        {
            // 스테이터스 표시
            PlayerCreature? player = GameManager.Instance.Player;
            if (player == null) return;
            while (true)
            {

                TextHelper.ItHeader($"Str : {player.StatStr}");
                TextHelper.ItHeader($"Dex : {player.StatDex}");
                TextHelper.ItHeader($"Int : {player.StatInt}");
                TextHelper.ItHeader($"Luck : {player.StatLuck}");
                TextHelper.ItHeader($"사용가능한 포인트 : {player.StatPoint}");

                GameManager.Instance.Player?.AddStatPoint(Defines.StatPointType.StatStr);

            }
        }
        protected override string EnumTypeToText<T>(T type)
        {
            throw new NotImplementedException();
        }
    }
}
