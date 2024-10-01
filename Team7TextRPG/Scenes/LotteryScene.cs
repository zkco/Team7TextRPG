using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team7TextRPG.Managers;
using Team7TextRPG.Utils;

namespace Team7TextRPG.Scenes
{
    public class LotteryScene : SceneBase
    {
        public override void Show()
        {
            TextHelper.BtHeader("복권 긁기");
            TextHelper.ItContent("현재 보유 중인 복권 수량 {___}"); //복권 갯수
            UIManager.Instance.Confirm("복권을 긁습니다.",
                () =>
                {
                    TextHelper.SlowPrint("복권을 긁고 있습니다.");
                    Thread.Sleep(1000);
                    //복권 갯수 삭감
                    //복권 결과 출력 함수
                },
                () =>
                {
                    return;
                });
        }

        private int LotteryReward()
        {
            int reward = 0;
            int targetScore = 0;
            Random rand = new Random();
            targetScore = rand.Next(1000) + 1;
            if(targetScore > 300)
            {
                TextHelper.DtContent("꽝... 다음 기회에..");
            }
            else if (targetScore > 100)
            {
                TextHelper.DtContent("5등");
            }
            else if (targetScore > 50)
            {
                TextHelper.DtContent("4등!");
            }
            else if (targetScore > 5)
            {
                TextHelper.DtContent("3등!!!");
            }
            else if (targetScore > 1)
            {
                TextHelper.DtContent("아깝다!! 2등!!");
            }
            else if (targetScore == 1)
            {
                Winning();
                Console.Clear();
                TextHelper.DtContent("$$ 1등 당첨 $$");
            }
            return reward;
        }

        protected override string SceneTypeToText<T>(T type)
        {
            throw new NotImplementedException();
        }

        private void Winning()
        {
            string[] _winningString1 = new string[]
        {
            "                                                                    ",
            "                                ★                                  ",
            "                         ★★★★★★★★                           ",
            "                                ★                                  ",
            "                              ★  ★                                ",
            "                            ★      ★                              ",
            "                                                                    ",
            "                      ★★★★★★★★★★★                        ",
            "                                ★                                  ",
            "                                ★                                  ",
            "                          ★★★★★★★★                          ",
            "                                        ★                          ",
            "                                       ★                           ",
            "                                      ★                            ",
            "                                     ★                             ",
            "                                     ★                             ",
            "                                                                    ",
            "                                                                    "
        };
            string[] _winningString2 = new string[]
                    {
            "                                                                    ",
            "                                                                    ",
            "                    ★                     ★★         ★          ",
            "    ★★★★★★    ★                                  ★          ",
            "    ★              ★               ★★★★★★★★   ★          ",
            "    ★              ★★★★                ★          ★          ",
            "    ★★★★★★    ★                    ★  ★    ★★★          ",
            "                    ★                  ★     ★       ★          ",
            "                    ★                                  ★          ",
            "                                                                    ",
            "            ★★★                         ★★★★★★             ",
            "         ★        ★                      ★        ★             ",
            "         ★        ★                      ★        ★             ",
            "            ★★★                         ★★★★★★             ",
            "                                                                    ",
            "                                                                    ",
            "                                                                    ",
            "                                                                    "
                    };
            int x = Console.WindowWidth / 2;
            int y = Console.WindowHeight / 2;

            for (int i = 0; i < y; i++)
            {
                int j = i - 1;
                Console.SetCursorPosition(x, Console.WindowHeight - i);
                Console.Write('@');
                Thread.Sleep(50);
                Console.SetCursorPosition(x, Console.WindowHeight - i);
                Console.Write(' ');
            }
            for (int i = 0, j = 0; i < x; i++)
            {
                float k = MathF.Sqrt(i);
                float xD = MathF.Sin((MathF.PI) * i / 4) * k * 4;
                float yD = MathF.Cos(-(MathF.PI) * i / 4) * k * 2;
                Console.SetCursorPosition(x + (int)xD, y + (int)yD);
                Console.Write('*');
                if (i % 4 == 0)
                {
                    j++;
                    switch (j)
                    {
                        case 0:
                            {
                                Console.Clear();
                                Console.BackgroundColor = (ConsoleColor)15;
                                break;
                            }
                        case 1:
                            {
                                Console.Clear();
                                Console.BackgroundColor = (ConsoleColor)9;
                                break;
                            }
                        case 2:
                            {
                                Console.Clear();
                                Console.BackgroundColor = (ConsoleColor)3;
                                break;
                            }
                        case 3:
                            {
                                Console.Clear();
                                Console.BackgroundColor = (ConsoleColor)11;
                                break;
                            }
                        case 4:
                            {
                                Console.Clear();
                                Console.BackgroundColor = (ConsoleColor)1;
                                break;
                            }
                        case 5:
                            {
                                Console.Clear();
                                Console.BackgroundColor = (ConsoleColor)0;
                                Console.Clear();
                                break;
                            }
                    }
                    Thread.Sleep(50);
                }
            }
            Console.Clear();
            for (int i = 0; i < _winningString1.Length; i++)
            {
                Console.SetCursorPosition(x - _winningString1[1].Length/2, i);
                Console.WriteLine(_winningString1[i]);
            }
            for (int i = 0; i < _winningString2.Length; i++)
            {
                Console.SetCursorPosition(x - _winningString2[0].Length / 2, i);
                Console.WriteLine(_winningString2[i]);
                Thread.Sleep(100);
            }
            Thread.Sleep(1000);
        }
    }
}
