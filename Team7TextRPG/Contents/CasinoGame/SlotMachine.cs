using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team7TextRPG.Managers;

namespace Team7TextRPG.Contents.CasinoGame
{
    public class SlotMachine
    {
        private int[,] _display = new int[,]
        {
            {0, 0, 1, 1, 2, 3, 4 },
            {0, 0, 1, 1, 2, 3, 4 },
            {0, 0, 1, 1, 2, 3, 4 }
        };
        private int bet = 10;

        public void Shuffle()
        {
            Random rand = new Random();
            int _temp = 0;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0;  j < 7; j++)
                {
                    int k = rand.Next(0, 7);
                    _temp = _display[i, j];
                    _display[i, j] = _display[i, k];
                    _display[i, k] = _temp;
                }
            }
        }

        public char MarkSym(int input)
        {
            if (input == 0)
            {
                return '♬';
            }
            else if (input == 1)
            {
                return '☆';
            }
            else if (input == 2)
            {
                return 'Ω';
            }
            else if (input == 3)
            {
                return '7';
            }
            else
            {
                return '◇';
            }
        }

        public void Screen()
        {
            Console.Clear();
            Console.WriteLine("♬♬♬ = X2   ☆☆☆ = X2");
            Console.WriteLine("◇◇◇ = X3   ΩΩΩ = X5   7 7 7 = X10");
            Console.WriteLine("◇◇◇ = X3   ΩΩΩ = X5   7 7 7 = X10");
            Console.WriteLine("|-------|-------|-------|");
            Console.WriteLine("|       |       |       |");
            Console.WriteLine("|   {0}   |   {1}  |   {2}   |", MarkSym(_display[0,0]), MarkSym(_display[1, 0]), MarkSym(_display[2, 0]));
            Console.WriteLine("|       |       |       |");
            Console.WriteLine("|-------|-------|-------|");
            Console.WriteLine("현재 베팅 {0}\r\n\r\n",bet);
            Console.WriteLine("1. 돌리기 2. 베팅금액 올리기(50) 3. 베팅금액 낮추기(50) 4. 나가기");
        }

        public void GameStart()
        {
            bool playing = true;
            while (playing == true)
            {
                Screen();
                int gameInput = InputManager.Instance.GetInputInt("숫자를 입력해주세요", 1, 4);
                switch (gameInput)
                {
                    case 1:
                        Shuffle();

                        break;
                }
            }
        }
    }
}
