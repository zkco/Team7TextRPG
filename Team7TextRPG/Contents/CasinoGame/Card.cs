using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team7TextRPG.Contents.CasinoGame
{
    public enum Suit { Spade, Heart, Clover, Diamond }
    public enum Rank { Ace = 1, Two, Three, Four, Five, Six, Seven, Eight, Nine, Ten, Jack, Queen, King }
    internal class Card
    {
        public Suit SuitOfCard;
        public Rank RankOfCard;
        public List<Card> Deck = new List<Card>();
        public List<Card> Hand = new List<Card>();
        public List<Card> DealerHand = new List<Card>();
        public bool GameOver = false;

        public Card(Suit suit, Rank rank)
        {
            SuitOfCard = suit;
            RankOfCard = rank;
        }

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

        }
        public void Split()
        {

        }

        public int CalcScore()
        {
            int TotalScore = 0;
            for(int i = 0; i < Hand.Count; i++)
            {
                if ((TotalScore - 11) < 21 && (int)Hand[i].RankOfCard == 1)
                {
                    TotalScore += 11;
                }
                else if (TotalScore > 21 && (int)Hand[i].RankOfCard == 1)
                {
                    TotalScore += 1;
                }
                else if ((int)Hand[i].RankOfCard > 10)
                {
                    TotalScore += 10;
                }
                else 
                {
                    TotalScore += (int)Hand[i].RankOfCard;
                }
            }
            return TotalScore;
        }

        public void StartBackJack()
        {
            while(GameOver)
            {

            }
        }
    }


}
