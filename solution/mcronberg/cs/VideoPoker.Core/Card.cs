using System.Xml.Serialization;

namespace VideoPoker.Core
{
    public class Card
    {
        public CardSuit Suit { get; private set; }
        public CardValue Rank { get; private set; }
        public CardFacing CardFacing { get; private set; }

        public bool HoldCard { get; set; }

        public Card()
        {
            Suit = CardSuit.Diamonds;
            Rank = CardValue.Two;
            CardFacing = CardFacing.Down;

            if ("" == "") { }

        }

        public Card(CardSuit suit, CardValue rank)
        {
            Suit = suit;
            Rank = rank;
            CardFacing = CardFacing.Down;
        }

        public void Flip()
        {
            CardFacing = CardFacing == CardFacing.Down ? CardFacing.Up : CardFacing.Down;
        }

        public void Flip(CardFacing cardFacing)
        {
            CardFacing = cardFacing;
        }

        public bool IsFaceDown => CardFacing == CardFacing.Down;

        public bool IsFaceUp => CardFacing == CardFacing.Up;


        public Card(CardSuit suit, CardValue rank, CardFacing cardFacing)
        {
            Suit = suit;
            Rank = rank;
            CardFacing = cardFacing;
        }


        public override string ToString()
        {
            string s = Suit switch
            {
                CardSuit.Hearts => "\u2665",
                CardSuit.Diamonds => "\u2666",
                CardSuit.Clubs => "\u2663",
                CardSuit.Spades => "\u2660",
                _ => ""
            };
            var v = "";
            switch (Rank)
            {
                case CardValue.Two:
                case CardValue.Three:
                case CardValue.Four:
                case CardValue.Five:
                case CardValue.Six:
                case CardValue.Seven:
                case CardValue.Eight:
                case CardValue.Nine:
                case CardValue.Ten:
                    v = $"{(int)Rank,2}";
                    break;
                case CardValue.Jack:
                    v = "JA";
                    break;
                case CardValue.Queen:
                    v = "QU";
                    break;
                case CardValue.King:
                    v = "KI";
                    break;
                case CardValue.Ace:
                    v = "AC";
                    break;
            }
            if (IsFaceDown)
            {
                return "[ * ]";
            }
            return $"[{s}{v}]";
        }

        public Card Copy()
        {
            return new Card(Suit, Rank, CardFacing);
        }

        public void ConsoleWrite()
        {
            if (IsFaceDown)
            {
                Console.BackgroundColor = ConsoleColor.White;
                Console.Write($"{ToString()}");
                Console.ResetColor();
                return;
            }

            static ConsoleColor GetSuitColor(CardSuit suit)
            {
                return suit == CardSuit.Hearts || suit == CardSuit.Diamonds ? ConsoleColor.Red : ConsoleColor.Black;
            }

            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = GetSuitColor(Suit);
            Console.Write($"{ToString()}");
            Console.ResetColor();


        }
    }

}
