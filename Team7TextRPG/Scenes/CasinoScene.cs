using Team7TextRPG.Datas;
using Team7TextRPG.Managers;
using Team7TextRPG.Utils;

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
            FiveCardPoker,
            Exit
        }
        public override void Show()
        {
            Console.Clear();
            if (GameManager.Instance.IsGameEnd())
            {
                GameManager.Instance.GameEnd();
                InputManager.Instance.GetInputEnter();
                return;
            }

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
                    TextHelper.ItContent("1. 골드를 칩으로 바꾼다.");
                    TextHelper.ItContent("2. 칩을 골드로 바꾼다.");
                    TextHelper.ItContent("3. 돌아간다.");
                    int _inputInt = InputManager.Instance.GetInputInt("숫자를 입력하세요.", 1, 3);
                    JobManager.Instance.Push(() => 
                    {
                        if (_inputInt == 1)
                        {
                            Console.Clear();
                            TextHelper.DtContent("100골드를 칩 한 개로 교환할 수 있습니다");
                            int _tryExchange = InputManager.Instance.GetInputInt("몇개의 칩으로 바꿀까요?", 1, 10000);
                            if (_tryExchange * CHIP_PRICE > GameManager.Instance.Gold)
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
                            if (_tryExchange > GameManager.Instance.Chip)
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
                        else
                        {
                            SceneManager.Instance.LoadScene<CasinoScene>();
                            return;
                        }
                        TextHelper.StatusBar($"현재 보유 칩 갯수 : {GameManager.Instance.Chip}");
                        Thread.Sleep(1000);
                        SceneManager.Instance.LoadScene<CasinoScene>();
                        return;
                    });
                    break;
                case ChoiceGame.ExchangeReward:
                    ShowReward();
                    break;
                case ChoiceGame.BlackJack:
                    CasinoManager.Instance.blackJack?.StartBlackJack();
                    break;
                case ChoiceGame.SlotMachine:
                    CasinoManager.Instance.slotMachine?.GameStart();
                    break;
                case ChoiceGame.OddEven:
                    CasinoManager.Instance.oddEven?.GameStart();
                    break;
                case ChoiceGame.FiveCardPoker:
                    CasinoManager.Instance.fiveCardPoker?.GameStart();
                    break;
                case ChoiceGame.Exit:
                    SceneManager.Instance.LoadScene<TownScene>();
                    return;
            }

        }

        private void ShowReward()
        {
            TextHelper.BtHeader("칩을 상품으로 교환");
            Console.WriteLine("1. 스파르탄 갑옷 | 7777 칩");
            Console.WriteLine("2. 스파르탄 목걸이 | 7777 칩");
            Console.WriteLine("3. 스파르탄 반지 | 7777 칩");
            Console.WriteLine("4. 한효승의 노트북 | 2000 칩");
            Console.WriteLine("5. 이전 화면으로 돌아가기");
            int input = InputManager.Instance.GetInputInt("숫자를 입력해주세요.",1,5);
            switch(input)
            {
                case 1:
                    if(GameManager.Instance.Chip >= 7777)
                    {
                        GameManager.Instance.RemoveChip(7777);
                        GameManager.Instance.AddItem(1030);
                    }
                    else
                    {
                        TextHelper.ItContent("칩이 모자랍니다.");
                    }
                    break;
                case 2:
                    if (GameManager.Instance.Chip >= 7777)
                    {
                        GameManager.Instance.RemoveChip(7777);
                        GameManager.Instance.AddItem(1039);
                    }
                    else
                    {
                        TextHelper.ItContent("칩이 모자랍니다.");
                    }
                    break;
                case 3:
                    if (GameManager.Instance.Chip >= 7777)
                    {
                        GameManager.Instance.RemoveChip(7777);
                        GameManager.Instance.AddItem(1040);
                    }
                    else
                    {
                        TextHelper.ItContent("칩이 모자랍니다.");
                    }
                    break;
                case 4:
                    if (GameManager.Instance.Chip >= 2000)
                    {
                        GameManager.Instance.RemoveChip(2000);
                        GameManager.Instance.AddItem(1038);
                    }
                    else
                    {
                        TextHelper.ItContent("칩이 모자랍니다.");
                    }
                    break;
                case 5: break;
            }
            Thread.Sleep(1000);
            SceneManager.Instance.LoadScene<CasinoScene>();
            return;
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
                ChoiceGame.Exit => "마을로 돌아가기",
                _ => "없음"
            };
        }
    }
}
