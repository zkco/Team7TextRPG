using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
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
    internal class FiveCardPoker
    {
        private List<Card> Deck = new List<Card>();
        private List<Card> Hand = new List<Card>();

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
                for (int j = 1; j < WhosHand.Count; j++)
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
                if ((int)WhosHand[i].RankOfCard + 1 == (int)WhosHand[i + 1].RankOfCard + 1)
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
            if (straightCount > 3)
            {
                if (WhosHand[0].RankOfCard == Rank.Ace &&
                    WhosHand[4].RankOfCard == Rank.King)
                {
                    isRoyal = true; //로얄,마운틴
                    isStraight = true;
                }
                if (straightCount > 4) isStraight = true; //스트레이트
            }
            if (pairCount == 1)
            {
                if (WhosHand.GroupBy(card => card.RankOfCard).Any(group => group.Count() == 2))
                {
                    var isHigh = WhosHand.GroupBy(card => card.RankOfCard).Where(group => group.Count() == 2);
                    return HandRank.Pair; //원페어
                }
            }
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
                else //풀하우스
                    return HandRank.FullHouse;
            }
            if (isStraight == true)
            {
                if (isFlush == true &&
                    isRoyal == true) return HandRank.RoyalStraight; //로얄스트레이트플러시
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
            TextHelper.ItHeader("나의 패");
            foreach(Card card in Hand)
            {
                Console.WriteLine("i. "+CardOnBoard(Hand[i]));
                i++;
            }
        }

        public void StartGame()
        {
            bool gameover = false;
            Hand.Clear();
            int count = 0;
            while (gameover == false)
            {
                while (Hand.Count < 5)
                {
                    Hand.Add(DrawCard());
                }
                Board(); 
                if(count <= 2)
                {
                    int input = 0;
                }
            }
        }
    }
}
