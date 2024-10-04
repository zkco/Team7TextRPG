using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team7TextRPG.Managers;
using Team7TextRPG.Scenes;
using Team7TextRPG.Utils;

namespace Team7TextRPG.Contents.CasinoGame
{
    public class BlackJack
    {
        public List<Card> Deck = new List<Card>();
        public List<Card> Hand = new List<Card>();
        public List<Card> DealerHand = new List<Card>();
        private bool _gameOver = false;
        private bool _isStay = false;

        public void MakeCard()
        {
            foreach (Suit suit in Enum.GetValues(typeof(Suit)))
            {
                foreach (Rank rank in Enum.GetValues(typeof(Rank)))
                {
                    Deck.Add(new Card(suit, rank));
                }
            }
        }

        public void Shuffle()
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

        public Card Hit()
        {
            Card card = Deck[0];
            Deck.RemoveAt(0);
            return card;
        }

        public void Stay()
        {
            while (CalcScore(DealerHand) < 17)
            {
                DealerHand.Add(Hit());
                ShowBoard(DealerHand, Hand);
                Thread.Sleep(100);
            }
            _isStay = true;
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
                case Rank.Jack: // ||
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

        public int CalcScore(List<Card> WhosHand)
        {
            int TotalScore = 0;
            int Ace = 0;
            for (int i = 0; i < WhosHand.Count; i++)
            {
                if ((int)WhosHand[i].RankOfCard > 10)
                {
                    TotalScore += 10;
                }
                else if ((int)WhosHand[i].RankOfCard == 1)
                {
                    TotalScore += 11;
                    Ace++;
                }
                else
                {
                    TotalScore += (int)WhosHand[i].RankOfCard;
                }
            }
            if (TotalScore > 21 && Ace > 0)
            {
                TotalScore -= 10;
                Ace--;
            }
            return TotalScore;
        }

        public void ShowBoard(List<Card> Dealer, List<Card> My)
        {
            Console.Clear();
            TextHelper.DtContent("딜러의 패");
            int i = 0;
            foreach (Card card in Dealer)
            {
                Console.WriteLine(CardOnBoard(Dealer[i]));
                i++;
            }
            TextHelper.DtContent("나의 패");
            i = 0;
            foreach (Card card in My)
            {
                Console.WriteLine(CardOnBoard(My[i]));
                i++;
            }
            TextHelper.ItContent("현재 딜러의 점수 : {0} 현재 나의 점수 : {1}", CalcScore(Dealer), CalcScore(My));
        }

        public void StartBlackJack()
        {
            Deck.Clear();
            Hand.Clear();
            DealerHand.Clear();
            _gameOver = false;
            _isStay = false;
            int _winCost = 0;
            MakeCard();
            Shuffle();
            TextHelper.CtContent("현재 보유 칩 갯수 : {0}", GameManager.Instance.Chip);
            int betChips = InputManager.Instance.GetInputInt("베팅 칩 갯수를 입력해주세요. (최대 10000개)", 1, 10000);
            if (betChips > GameManager.Instance.Chip)
            {
                if (GameManager.Instance.Chip < 1)
                {
                    Console.WriteLine("칩이 없습니다. 카지노 화면으로 돌아갑니다.");
                    SceneManager.Instance.LoadScene<CasinoScene>();
                    return;
                }
                Console.WriteLine("칩 갯수가 모자랍니다.");
                InputManager.Instance.GetInputInt("베팅 칩 갯수를 입력해주세요. (최대 10000개)", 1, 10000);
            }
            else
            {
                GameManager.Instance.RemoveChip(betChips);
                for (int i = 0; i < 2; i++)
                {
                    DealerHand.Add(Hit());
                    Hand.Add(Hit());
                }
                while (_gameOver == false && _isStay == false)
                {
                    ShowBoard(DealerHand, Hand);
                    if (CalcScore(Hand) < 21) UIManager.Instance.Confirm("패를 추가로 뽑으시겠습니까?",
                        () =>
                        {
                            Hand.Add(Hit());
                        },
                        () =>
                        {
                            TextHelper.DtContent("스테이");
                            Stay();
                        });
                    if (CalcScore(Hand) > 21)
                    {
                        TextHelper.DtContent("버스트 하였습니다.");
                        _gameOver = true;
                    }
                    if (CalcScore(Hand) == 21) break;
                }
                if (_isStay == true)
                {
                    if (CalcScore(Hand) > CalcScore(DealerHand) && CalcScore(Hand) < 21)
                    {
                        TextHelper.DtContent("당신이 승리하였습니다!");
                        _winCost = betChips * 2;
                    }
                    else if (CalcScore(DealerHand) > 21 && CalcScore(Hand) < 21)
                    {
                        TextHelper.DtContent("딜러가 버스트하였습니다!");
                        _winCost = betChips * 2;
                    }
                }
                if (CalcScore(Hand) == 21)
                {
                    TextHelper.DtContent("당신이 블랙잭으로 승리하였습니다!!");
                    _winCost = betChips * 3;
                }
            }
            GameManager.Instance.AddChip(_winCost);
            TextHelper.CtContent("칩을 {0}개 획득했습니다.", _winCost);
            UIManager.Instance.Confirm("한 게임 더 하시겠습니까?",
                () =>
                {
                    StartBlackJack();
                },
                () =>
                {
                    SceneManager.Instance.LoadScene<CasinoScene>();
                    return;
                });
        }
    }
}
