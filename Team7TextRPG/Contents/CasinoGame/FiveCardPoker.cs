using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Team7TextRPG.Creatures;
using Team7TextRPG.Managers;
using Team7TextRPG.Scenes;
using Team7TextRPG.Utils;

namespace Team7TextRPG.Contents.CasinoGame
{
    internal enum HandRank
    {
        None,
        Top,
        Pair,
        TwoPair,
        ThreeOfAKind,
        Straight,
        Mountain,
        Flush,
        FullHouse,
        FourOfAKind,
        StraightFlush,
        RoyalStraight,
    }
    public class FiveCardPoker
    {
        private List<Card> Deck = new List<Card>();
        private List<Card> Hand = new List<Card>();
        private int _bet = 100;
        private bool _gameover = false;
        private int _changeCount = 0;

        private void MakeCard()
        {
            foreach (Suit suit in Enum.GetValues(typeof(Suit)))
            {
                foreach (Rank rank in Enum.GetValues(typeof(Rank)))
                {
                    Deck.Add(new Card(suit, rank));
                }
            }
        }

        private void Shuffle()
        {
            Random rand = new Random();
            for (int i = 0; i < Deck.Count; i++)
            {
                int j = rand.Next(i, Deck.Count);
                Card temp = Deck[i];
                Deck[i] = Deck[j];
                Deck[j] = temp;
            }
        }
        private Card DrawCard()
        {
            Card card = Deck[0];
            Deck.RemoveAt(0);
            return card;
        }

        public string CardOnBoard(Card card)
        {
            StringBuilder sb = new StringBuilder();
            switch (card.SuitOfCard)
            {
                case Suit.Spade:
                    sb.Append("♠");
                    break;
                case Suit.Heart:
                    sb.Append("♥");
                    break;
                case Suit.Clover:
                    sb.Append("♣");
                    break;
                case Suit.Diamond:
                    sb.Append("◆");
                    break;
            }

            sb.Append(" ");
            switch (card.RankOfCard)
            {
                case Rank.Jack:
                case Rank.King:
                case Rank.Queen:
                    sb.Append(card.RankOfCard);
                    break;
                default:
                    sb.Append(((int)card.RankOfCard).ToString());
                    break;
            }
            return sb.ToString();
        }

        private List<Card> Sort(List<Card> WhosHand)
        {
            for (int i = 0; i < WhosHand.Count; i++)
            {
                for (int j = 0; j < WhosHand.Count; j++)
                {
                    if ((int)WhosHand[i].RankOfCard < (int)WhosHand[j].RankOfCard)
                    {
                        var temp = WhosHand[i];
                        WhosHand[i] = WhosHand[j];
                        WhosHand[j] = temp;
                    }
                }
            }
            return WhosHand;
        }

        private HandRank HandRanking(List<Card> WhosHand)
        {
            int flushCount = 0;
            int straightCount = 0;
            int pairCount = 0;
            bool isFlush = false;
            bool isStraight = false;
            bool isRoyal = false;
            //숫자 순 정렬
            for (int i = 0; i < WhosHand.Count; i++)
            {
                for (int j = 0; j > WhosHand.Count; j++)
                {
                    if ((int)WhosHand[i].RankOfCard > (int)WhosHand[j].RankOfCard)
                    {
                        var temp = WhosHand[i];
                        WhosHand[i] = WhosHand[j];
                        WhosHand[j] = temp;
                    }
                }
            }
            for (int i = 0; i < WhosHand.Count - 1; i++)
            {
                if ((int)WhosHand[i].RankOfCard + 1 == (int)WhosHand[i + 1].RankOfCard)
                {
                    straightCount++;
                }
                if ((int)WhosHand[i].RankOfCard == (int)WhosHand[i + 1].RankOfCard)
                {
                    pairCount++;
                }
                if (WhosHand[i].SuitOfCard == WhosHand[i + 1].SuitOfCard)
                {
                    flushCount++;
                }
            }
            if (flushCount == 4) isFlush = true; //플러시
            if (straightCount >= 3)
            {
                if ((int)WhosHand[0].RankOfCard == 1 &&
                    (int)WhosHand[4].RankOfCard == 13)
                {
                    isRoyal = true; //로얄,마운틴
                    isStraight = true;
                }
                if (straightCount == 4) isStraight = true; //스트레이트
            }
            if (pairCount == 1) return HandRank.Pair; //원페어
            else if (pairCount == 2)
            {
                if (WhosHand.GroupBy(card => card.RankOfCard).Count(group => group.Count() == 2) == 2)
                {
                    //투페어
                    return HandRank.TwoPair;
                }
                //트리플
                else return HandRank.ThreeOfAKind;
            }
            else if (pairCount == 3)
            {
                if (WhosHand.GroupBy(card => card.RankOfCard).Any(group => group.Count() == 4))
                {
                    //포카드
                    return HandRank.FourOfAKind;
                }
                else return HandRank.FullHouse; //풀하우스 
            }
            if (isStraight == true)
            {
                if (isFlush == true && isRoyal == true) return HandRank.RoyalStraight; //로얄스트레이트플러시
                else if (isFlush == true) return HandRank.StraightFlush; //스트레이트플러시
                else if (isRoyal == true) return HandRank.Mountain; //마운틴
                else return HandRank.Straight; //스트레이트
            }
            else if (isFlush == true) return HandRank.Flush; //플러시

            return HandRank.Top;
        }

        private void Board()
        {
            int i = 0;
            Console.Clear();
            TextHelper.CtContent("현재 판 돈 {0}", _bet * 2);
            TextHelper.CtContent("현재 보유 칩 갯수 : {0}\r\n", GameManager.Instance.PlayerChip);
            TextHelper.DtContent("나의 패");
            foreach (Card card in Hand)
            {
                Console.WriteLine($"{i + 1}. " + CardOnBoard(Hand[i]));
                i++;
            }
        }

        private HandRank DealerHand()
        {
            Random rand = new Random();
            int i = rand.Next(1, 1000);
            if (i > 800) return HandRank.Top;
            else if (i > 500) return HandRank.Pair;
            else if (i > 300) return HandRank.TwoPair;
            else if (i > 200) return HandRank.ThreeOfAKind;
            else if (i > 100) return HandRank.Straight;
            else if (i > 50) return HandRank.Mountain;
            else if (i > 25) return HandRank.Flush;
            else if (i > 15) return HandRank.FullHouse;
            else if (i > 8) return HandRank.FourOfAKind;
            else if (i > 1) return HandRank.StraightFlush;
            else return HandRank.RoyalStraight;
        }
        private int Raise(int count)
        {
            Console.Clear();
            Board();
            int input = InputManager.Instance.GetInputInt($"레이즈 할 칩을 입력해주세요. ({_bet} ~ {_bet * 2})", _bet, _bet * 2);
            if (GameManager.Instance.PlayerChip >= _bet)
            {
                GameManager.Instance.RemoveChip(input);
                _bet += input;
                count++;
            }
            else
            {
                TextHelper.DtContent("소지한 칩이 부족합니다.");
                TextHelper.ItContent("체크");
                Thread.Sleep(1000);
                count++;
            }
            return count;
        }
        public void GameStart()
        {
            _gameover = false;
            _changeCount = 0;
            _bet = 100;
            int count = 0;
            Hand.Clear();
            MakeCard();
            Shuffle();
            TextHelper.CtContent("현재 보유 칩 갯수 : {0}\r\n", GameManager.Instance.PlayerChip);
            TextHelper.ItContent("게임 시작을 위해 100개의 칩을 지불합니다.");
            int inputInt = InputManager.Instance.GetInputInt("1. 네 2. 아니오",1,2);
            if(inputInt == 1)
            {
                if (GameManager.Instance.PlayerChip >= 100)
                {
                    GameManager.Instance.RemoveChip(100);
                }
                else
                {
                    TextHelper.CtContent("칩이 모자랍니다.");
                    Thread.Sleep(1000);
                    SceneManager.Instance.LoadScene<CasinoScene>();
                    return;
                }
            }
            else
            {
                SceneManager.Instance.LoadScene<CasinoScene>();
                return;
            }
            while (_gameover == false)
            {
                GameMain(count);
            }
            TextHelper.ItContent("다시 하시겠습니까?");
            inputInt = InputManager.Instance.GetInputInt("1. 네 2. 아니오", 1, 2);
            if(inputInt == 1)
            {
                if (GameManager.Instance.PlayerGold < 100)
                {
                    TextHelper.CtContent("칩이 모자랍니다.");
                    Thread.Sleep(1000);
                    return;
                }
                else
                {
                    GameStart();
                }
            }
            else
            {
                SceneManager.Instance.LoadScene<CasinoScene>();
                return;
            }
        }
        private int GameMain(int count)
        {
            while (Hand.Count < 5)
            {
                Hand.Add(DrawCard());
            }
            Sort(Hand);
            Board();
            if (count <= 2)
            {
                Console.Clear();
                Board();
                if (_changeCount < 2)
                {
                    TextHelper.ItContent("6. 교체하지 않음");
                    int input = InputManager.Instance.GetInputInt($"교체할 카드를 골라주세요. ({_changeCount + 1}/2)", 1, 6);
                    switch (input)
                    {
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                        case 5:
                            Hand.RemoveAt(input - 1);
                            Hand.Add(DrawCard());
                            _changeCount++;
                            return GameMain(count);
                        case 6:
                            break;
                    }
                }
                Console.Clear();
                Board();
                TextHelper.ItContent("1. 레이즈");
                TextHelper.ItContent("2. 체크");
                TextHelper.ItContent("3. 폴드");
                int playing = InputManager.Instance.GetInputInt($"숫자를 입력해주세요.", 1, 3);
                switch (playing)
                {
                    case 1:
                        count = Raise(count);
                        _changeCount = 0;
                        count++;
                        GameMain(count);
                        break;
                    case 2:
                        TextHelper.BtContent("체크");
                        Thread.Sleep(1000);
                        _changeCount = 0;
                        count++;
                        GameMain(count);
                        break;
                    case 3:
                        Console.Clear();
                        Board();
                        TextHelper.BtContent("폴드하셨습니다.");
                        _changeCount = 0;
                        _gameover = true;
                        count = 3;
                        Thread.Sleep(1000);
                        break;
                }
            }
            else
            {
                Console.Clear();
                Board();
                var dealerHand = DealerHand();
                TextHelper.ItContent("패를 공개합니다.\r\n");
                Thread.Sleep(1000);
                TextHelper.DtContent("당신의 패");
                TextHelper.BtContent(HandRanking(Hand).ToString());
                Thread.Sleep(1000);
                TextHelper.DtContent("딜러의 패");
                TextHelper.BtContent(dealerHand.ToString());
                Thread.Sleep(2000);
                Console.Clear();
                if ((int)HandRanking(Hand) > (int)dealerHand)
                {
                    TextHelper.ItContent("당신이 승리했습니다!");
                    TextHelper.ItContent($"획득한 칩 : {_bet * 2}");
                    GameManager.Instance.AddChip(_bet * 2);
                    if ((int)HandRanking(Hand) >= 9)
                    {
                        TextHelper.BtContent($"$$빅 핸드 보너스 {_bet * 3}$$");
                        TextHelper.BtContent($"$$총 획득 칩 : {_bet * 5}$$");
                        GameManager.Instance.AddChip(_bet * 3);
                    }
                }
                else if ((int)HandRanking(Hand) < (int)dealerHand)
                {
                    TextHelper.CtContent("당신이 패배했습니다...");
                    TextHelper.ItContent($"잃은 칩 : {_bet}");
                }
                else if ((int)HandRanking(Hand) == (int)dealerHand)
                {
                    TextHelper.CtContent("묻고 더블로 가!");
                    Thread.Sleep(3000);
                    Hand.Clear();
                    count = 0;
                    return GameMain(0);
                }
                _changeCount = 0;
                _gameover = true;
                Thread.Sleep(3000);
            }
            return 0;
        }
    }
}
