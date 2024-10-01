using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Team7TextRPG.Managers;
using Team7TextRPG.Scenes;
using Team7TextRPG.Utils;

namespace Team7TextRPG.Contents.CasinoGame
{
    public enum Suit { Spade, Heart, Clover, Diamond }
    public enum Rank { Ace = 1, Two, Three, Four, Five, Six, Seven, Eight, Nine, Ten, Jack, Queen, King }
    public class Card
    {
        public Suit SuitOfCard;
        public Rank RankOfCard;

        public Card(Suit suit, Rank rank)
        {
            SuitOfCard = suit;
            RankOfCard = rank;
        }
    }
}
