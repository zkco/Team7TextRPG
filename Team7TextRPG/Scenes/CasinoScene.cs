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
using Team7TextRPG.Utils;
using static Team7TextRPG.Scenes.DungeonScene;

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
            TextHelper.BtHeader("카지노");
            // [상태, 인벤토리, 스킬, 퀘스트]
            UIManager.Instance.CommonWriteBar();
            TextHelper.DtContent("인생 한 방 카지노에 오신 것을 환영합니다!");
            TextHelper.DtContent("어떤 게임을 즐기러 오셨나요?");
            WriteType<ChoiceGame>();

            string input = InputManager.Instance.GetInputKeyword();

            // 공통 UI 호출한 경우 볼일 마치가 다시 처음으로
            if (UIManager.Instance.CommonLoad(input))
            {
                SceneManager.Instance.LoadScene<CasinoScene>();
                return;
            }

            ChoiceGame selection = InputManager.Instance.ParseInputType<ChoiceGame>(input);

            switch(selection)
            {
                case ChoiceGame.ExchangeChips:
                    Console.Clear();
                    TextHelper.ItContent("1. 칩을 골드로 바꾼다.");
                    TextHelper.ItContent("2. 골드를 칩으로 바꾼다.");
                    int _inputInt = InputManager.Instance.GetInputInt("숫자를 입력하세요.", 1, 2);
                    if (_inputInt == 1)
                    {
                        Console.Clear();
                        TextHelper.DtContent("100골드를 칩 한 개로 교환할 수 있습니다.");
                        int _tryExchange = InputManager.Instance.GetInputInt("몇개의 칩으로 바꿀까요?", 1, 10000);
                        if (_tryExchange * CHIP_PRICE > GameManager.Instance.PlayerGold)
                        {
                            TextHelper.DtContent("돈이 모자랍니다.");
                        }
                        else
                        {
                            GameManager.Instance.RemoveGold(_tryExchange * CHIP_PRICE);
                            GameManager.Instance.AddChip(_tryExchange);
                            TextHelper.DtContent($"{_tryExchange * CHIP_PRICE} 골드를 {_tryExchange}개의 칩으로 교환했습니다.");
                        }
                    }
                    else if (_inputInt == 2)
                    {
                        Console.Clear();
                        TextHelper.DtContent("칩 한 개를 100골드로 교환할 수 있습니다.");
                        int _tryExchange = InputManager.Instance.GetInputInt("몇개의 칩을 바꿀까요?", 1, 10000);
                        if (_tryExchange > GameManager.Instance.PlayerChip)
                        {
                            Console.WriteLine("칩이 모자랍니다.");
                        }
                        else
                        {
                            GameManager.Instance.AddGold(_tryExchange * CHIP_PRICE);
                            GameManager.Instance.RemoveChip(_tryExchange);
                            TextHelper.DtContent($"칩 {_tryExchange}개를 {_tryExchange * CHIP_PRICE} 골드로 교환했습니다.");
                        }
                    }
                    TextHelper.StatusBar($"현재 보유 칩 갯수 : {GameManager.Instance.PlayerChip}");
                    Thread.Sleep(1000);
                    SceneManager.Instance.LoadScene<CasinoScene>();
                    break;
                case ChoiceGame.ExchangeReward:
                        //칩 갯수에 따라 교환할 수 있는 보상 추가 (레어 아이템, 고효율 포션 등)
                    break;
                case ChoiceGame.BlackJack:
                    CasinoManager.Instance.blackJack?.StartBlackJack();
                    break;
                case ChoiceGame.SlotMachine:
                    CasinoManager.Instance.slotMachine?.GameStart();
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
