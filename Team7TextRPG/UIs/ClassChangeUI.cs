using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team7TextRPG.Utils;

namespace Team7TextRPG.UIs
{
    public class ClassChangeUI : UIBase
    {
        public override void Write()
        {
            TextHelper.BtHeader("직업 변경 미구현");
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
