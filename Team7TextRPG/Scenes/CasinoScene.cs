using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Team7TextRPG.Managers;
using Team7TextRPG.UIs;

namespace Team7TextRPG.Scenes
{
    internal class CasinoScene : SceneBase
    {
        const int CHIP_PRICE = 100; //골드 / 칩 교환비


        private enum ChoiceGame
        {
            None,
            ExchangeChips,
            ExchangeReward,
            BlackJack,
            SlotMachine,
            OddEven,
            FiveCardPoker
        }
        public override void Show()
        {
            Console.Clear();
            WriteMessage("인생 한 방 카지노에 오신 것을 환영합니다!");
            WriteMessage("어떤 게임을 즐기러 오셨나요?");
            WriteType<ChoiceGame>();

            ChoiceGame selection = InputManager.Instance.GetInputType<ChoiceGame>();

            switch(selection)
            {
                case ChoiceGame.ExchangeChips:
                    Console.Clear();
                    WriteMessage("1. 칩을 골드로 바꾼다.");
                    WriteMessage("2. 골드를 칩으로 바꾼다.");
                    int _inputInt = InputManager.Instance.GetInputInt(Console.ReadLine(), 1, 2);
                    JobManager.Instance.Push(() => 
                    {
                        if (_inputInt == 1)
                        {
                            Console.Clear();
                            Console.WriteLine("100골드를 칩 한 개로 교환할 수 있습니다.");
                            int _tryExchange = InputManager.Instance.GetInputInt("몇개의 칩으로 바꿀까요?", 1, 10000);
                            if (_tryExchange * CHIP_PRICE > GameManager.Instance.Gold)
                            {
                                Console.WriteLine("돈이 모자랍니다.");
                                SceneManager.Instance.LoadScene<CasinoScene>();
                            }
                            else 
                            {
                                GameManager.Instance.Gold -= _tryExchange * CHIP_PRICE;
                                GameManager.Instance.Chip += _tryExchange;
                                Console.WriteLine($"{_tryExchange * CHIP_PRICE} 골드를 {_tryExchange}개의 칩으로 교환했습니다.");
                            }
                        }
                        else if(_inputInt == 2) 
                        {
                            Console.Clear();
                            Console.WriteLine("칩 한 개를 100골드로 교환할 수 있습니다.");
                            int _tryExchange = InputManager.Instance.GetInputInt("몇개의 칩을 바꿀까요?", 1, 10000);
                            if (_tryExchange > GameManager.Instance.Chip)
                            {
                                Console.WriteLine("칩이 모자랍니다.");
                                SceneManager.Instance.LoadScene<CasinoScene>();
                            }
                            else
                            {
                                GameManager.Instance.Gold += _tryExchange * CHIP_PRICE;
                                GameManager.Instance.Chip -= _tryExchange;
                                Console.WriteLine($"칩 {_tryExchange}개를 {_tryExchange * CHIP_PRICE} 골드로 교환했습니다.");
                            }
                            SceneManager.Instance.LoadScene<CasinoScene>();
                        }
                    });
                    break;
                case ChoiceGame.ExchangeReward:
                        //칩 갯수에 따라 교환할 수 있는 보상 추가 (레어 아이템, 고효율 포션 등)
                    break;
                case ChoiceGame.BlackJack:
                        
                    break;
                case ChoiceGame.SlotMachine:
                    break;
                case ChoiceGame.OddEven:
                    break;
                case ChoiceGame.FiveCardPoker:
                    break;
            }

        }

        protected override string SceneTypeToText<T>(T type)
        {
            return type switch
            {
                ChoiceGame.ExchangeChips => "칩 교환하기",
                ChoiceGame.ExchangeReward => "상품 교환하기",
                ChoiceGame.BlackJack => "블랙잭",
                ChoiceGame.SlotMachine => "슬롯머신",
                ChoiceGame.OddEven => "홀짝",
                ChoiceGame.FiveCardPoker => "파이브카드 포커",
                _ => "없음"
            };
        }
    }
}
