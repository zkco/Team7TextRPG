using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team7TextRPG.Contents;
using Team7TextRPG.Contents.Items;
using Team7TextRPG.Managers;
using Team7TextRPG.Scenes;
using Team7TextRPG.Utils;

namespace Team7TextRPG.UIs
{
    public class LotteryUI : UIBase
    {
        public override void Write()
        {
            Lottery lottery = new Lottery();
            lottery.Show();
        }

        protected override string EnumTypeToText<T>(T type)
        {
            return String.Empty;
        }
    }
}
