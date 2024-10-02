using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team7TextRPG.Managers;
using Team7TextRPG.Scenes;
using Team7TextRPG.Utils;

namespace Team7TextRPG.Contents.CasinoGame
{
    public class OddEven
    {
        Random rand = new Random();
        private int[] _dice = new int[2];
        private int _bet = 0;
        private bool _playing;

        private void DiceFace(int inputDice)
        {
            switch (inputDice)
            {
                case 1:
                    TextHelper.ItContent("┌───────┐ ");
                    TextHelper.ItContent("│       │ ");
                    TextHelper.ItContent("│   ●  │ ");
                    TextHelper.ItContent("│       │ ");
                    TextHelper.ItContent("└───────┘ ");
                    break;
                case 2:
                    TextHelper.ItContent("┌───────┐ ");
                    TextHelper.ItContent("│ ●    │ ");
                    TextHelper.ItContent("│       │ ");
                    TextHelper.ItContent("│     ●│ ");
                    TextHelper.ItContent("└───────┘ ");
                    break;
                case 3:
                    TextHelper.ItContent("┌───────┐ ");
                    TextHelper.ItContent("│ ●    │ ");
                    TextHelper.ItContent("│   ●  │ ");
                    TextHelper.ItContent("│     ●│ ");
                    TextHelper.ItContent("└───────┘ ");
                    break;
                case 4:
                    TextHelper.ItContent("┌───────┐ ");
                    TextHelper.ItContent("│ ●  ●│ ");
                    TextHelper.ItContent("│       │ ");
                    TextHelper.ItContent("│ ●  ●│ ");
                    TextHelper.ItContent("└───────┘ ");
                    break;
                case 5:
                    TextHelper.ItContent("┌───────┐ ");
                    TextHelper.ItContent("│ ●  ●│ ");
                    TextHelper.ItContent("│   ●  │ ");
                    TextHelper.ItContent("│ ●  ●│ ");
                    TextHelper.ItContent("└───────┘ ");
                    break;
                case 6:
                    TextHelper.ItContent("┌───────┐ ");
                    TextHelper.ItContent("│ ●  ●│ ");
                    TextHelper.ItContent("│ ●  ●│ ");
                    TextHelper.ItContent("│ ●  ●│ ");
                    TextHelper.ItContent("└───────┘ ");
                    break;
            }
        }
        private void Shuffle()
        {
            _dice[0] = rand.Next(1, 7);
            _dice[1] = rand.Next(1, 7);
        }

        private int AddScore()
        {
            return _dice[0] + _dice[1];
        }

        private int Betting()
        {
            TextHelper.DtContent($"현재 칩 갯수 : {GameManager.Instance.Chip}");
            TextHelper.ItContent("베팅할 칩 갯수를 입력해주세요.");
            _bet = InputManager.Instance.GetInputInt("숫자를 입력해주세요. (최대 300)", 1, 300);
            if (GameManager.Instance.Chip >= _bet)
            {
                GameManager.Instance.RemoveChip(_bet);
                Console.Clear();
                TextHelper.ItContent("베팅 방법을 선택해주세요.");
                Console.WriteLine("1. 홀");
                Console.WriteLine("2. 짝");
                Console.WriteLine("3. 뒤로 돌아가기");
                int input = InputManager.Instance.GetInputInt("숫자를 입력해주세요.", 1, 3);
                Console.Clear();
                return input;
            }
            else 
            {
                TextHelper.ItContent("칩이 모자랍니다.");
                return 0;
            }

        }

        public void GameStart()
        {
            _playing = true;
            while(_playing == true)
            {
                Console.Clear();
                int BetNumb = Betting();
                GameManager.Instance.RemoveChip(_bet);
                Shuffle();
                TextHelper.DtContent("주사위 숫자의 합은 {0}", AddScore());
                DiceFace(_dice[0]);
                DiceFace(_dice[1]);

                if(AddScore() % 2 == 1 && BetNumb == 1)
                {
                    TextHelper.BtContent("당신이 승리했습니다!");
                    GameManager.Instance.AddChip(_bet * 2);
                    TextHelper.DtContent($"현재 칩 갯수 : {GameManager.Instance.Chip}");
                    UIManager.Instance.Confirm("다시 하시겠습니까?",
                        () =>
                        {
                            GameStart();
                        },
                        () =>
                        {
                            _playing = false;
                            SceneManager.Instance.LoadScene<CasinoScene>();
                            return;
                        });
                }
                else if(AddScore() % 2 == 0 && BetNumb == 2) 
                {
                    TextHelper.BtContent("당신이 승리했습니다!");
                    GameManager.Instance.AddChip(_bet * 2);
                    TextHelper.DtContent($"현재 칩 갯수 : {GameManager.Instance.Chip}");
                    UIManager.Instance.Confirm("다시 하시겠습니까?",
                        () =>
                        {
                            GameStart();
                        },
                        () =>
                        {
                            _playing = false;
                            SceneManager.Instance.LoadScene<CasinoScene>();
                            return;
                        });
                }
                else
                {
                    TextHelper.BtContent("베팅한 모든 금액을 잃었습니다...");
                    TextHelper.DtContent($"현재 칩 갯수 : {GameManager.Instance.Chip}");
                    UIManager.Instance.Confirm("다시 하시겠습니까?",
                        () =>
                        {
                            GameStart();
                        },
                        () =>
                        {
                            _playing = false;
                            SceneManager.Instance.LoadScene<CasinoScene>();
                            return;
                        });
                }
            }
        }
    }
}