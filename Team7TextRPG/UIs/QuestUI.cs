using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team7TextRPG.UIs
{
    public class QuestUI : UIBase
    {
        private readonly object item;

        public override void Write()
        {

        }

            Console.WriteLine("");
            Console.WriteLine("0. ?��?�?);
            Console.WriteLine("1. ?�이??구매");
            Console.WriteLine("");

        }
        protected override string EnumTypeToText<T>(T type)
        {
            return String.Empty;
        }

    }

}
