using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using Team7TextRPG.Managers;
using Team7TextRPG.Scenes;
using Team7TextRPG.Utils;

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
        private int _bet = 10;
        private int _count = 0;

        public void Shuffle()
        {
            Random rand = new Random();
            int _temp = 0;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 7; j++)
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
                return '⑦';
            }
            else
            {
                return '◇';
            }
        }

        public void Screen()
        {
            Console.Clear();
            TextHelper.CtContent("현재 보유 칩 갯수 : {0}", GameManager.Instance.Chip);
            TextHelper.DtContent("♬♬♬ = X5    ☆☆☆ = X5");
            TextHelper.DtContent("◇◇◇ = X10   ΩΩΩ = X20   ⑦⑦⑦ = X100\r\n");
            TextHelper.ItContent("|------|------|------|");
            TextHelper.ItContent("|  {0}  |  {1}  |  {2}  |", MarkSym(_display[0, 0]), MarkSym(_display[1, 0]), MarkSym(_display[2, 0]));
            TextHelper.ItContent("|------|------|------|");
            TextHelper.DtContent("현재 베팅 {0}\r\n\r\n", _bet);
            TextHelper.ItContent("1. 돌리기 2. 베팅금액 올리기(10) 3. 베팅금액 낮추기(10) 4. 나가기");
            GetScore();
        }

        public void GameStart()
        {
            _count = 0;
            bool playing = true;

            while (playing)
            {
                Screen();
                int gameInput = InputManager.Instance.GetInputInt("숫자를 입력해주세요", 1, 4);
                switch (gameInput)
                {
                    case 1:
                        if (GameManager.Instance.Chip >= _bet)
                        {
                            GameManager.Instance.RemoveChip(_bet);
                            _count++;
                            Shuffle();
                        }
                        else
                        {
                            TextHelper.BtContent("칩이 부족하다...");
                            Thread.Sleep(1000);
                        }
                        break;
                    case 2:
                        _bet += 10;
                        if (_bet > 100)
                        {
                            _bet = 100;
                        }
                        break;
                    case 3:
                        _bet -= 10;
                        if (_bet < 1)
                        {
                            _bet = 10;
                        }
                        break;
                    case 4:
                        playing = false;
                        break;
                }
            }
            SceneManager.Instance.LoadScene<CasinoScene>();
            return;
        }

        public void GetScore()
        {
            if (_count == 0) { }
            else if (MarkSym(_display[0, 0]) == MarkSym(_display[1, 0]) &&
                MarkSym(_display[0, 0]) == MarkSym(_display[2, 0]))
            {
                switch (MarkSym(_display[0, 0]))
                {
                    case '♬':
                        GameManager.Instance.AddChip(_bet * 5);
                        TextHelper.BtContent("맞았다!");
                        break;
                    case '☆':
                        GameManager.Instance.AddChip(_bet * 5);
                        TextHelper.BtContent("맞았다!");
                        break;
                    case '◇':
                        GameManager.Instance.AddChip(_bet * 10);
                        TextHelper.BtContent("맞았다!");
                        break;
                    case 'Ω':
                        GameManager.Instance.AddChip(_bet * 20);
                        TextHelper.BtContent("대박이다!");
                        break;
                    case '⑦':
                        GameManager.Instance.AddChip(_bet * 100);
                        TextHelper.BtContent("잭팟이다!");
                        break;
                }
                _count++;
            }
            else
            {
                TextHelper.BtContent("이런...");
            }
        }
    }
}
