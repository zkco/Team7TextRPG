using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team7TextRPG.Contents.CasinoGame;

namespace Team7TextRPG.Managers
{
    public class CasinoManager
    {
        private static CasinoManager? _instance;
        public static CasinoManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new CasinoManager();
                return _instance;
            }
        }

        public BlackJack? blackJack = new BlackJack();
        public SlotMachine? slotMachine = new SlotMachine();
        public OddEven? oddEven = new OddEven();
        public FiveCardPoker? fiveCardPoker = new FiveCardPoker();
    }
}
