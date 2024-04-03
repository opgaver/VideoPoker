namespace VideoPoker.Core
{    
    public class PokerHand : Deck
    {
        public PokerHand(List<Card> cards)
        {
            this.cards = cards;
        }

        public override void AddCard(Card card)
        {
            if (cards.Count == 5)
            {
                throw new Exception("Hand is full");
            }
            cards.Add(card);
        }

        public void ReplaceCard(int index, Card card)
        {
            if (index < 0 || index > 4)
            {
                throw new Exception("Index out of range");
            }
            cards[index] = card;
        }

        public void AddCards(Stack<Card> cards)
        {
            for (int i = 0; i < cards.Count; i++)
            {
                AddCard(cards.Pop());
            }
        }




        public void ConsoleWrite(bool debug = false)
        {


            foreach (Card card in cards)
            {
                card.ConsoleWrite();
                Console.Write(" ");
            }

            if (debug)
            {
                Console.WriteLine();
                Console.WriteLine();                
                Console.WriteLine($"Royal flush     : {IsRoyalFlush}");
                Console.WriteLine($"Straight flush  : {IsStraightFlush}");
                Console.WriteLine($"Four of a kind  : {IsFourOfAKind}");
                Console.WriteLine($"Full house      : {IsFullHouse}");
                Console.WriteLine($"Flush           : {IsFlush}");
                Console.WriteLine($"Straight        : {IsStraight}");
                Console.WriteLine($"Three of a kind : {IsThreeOfAKind}");
                Console.WriteLine($"Two pair        : {IsTwoPair}");
                Console.WriteLine($"Jacks or better : {IsJacksOrBetter}");
                

            }
        }


        public bool IsJacksOrBetter
        {
            get
            {
                return cards.GroupBy(card => card.Rank).Any(group => group.Skip(1).Any() && group.Key >= CardValue.Jack);
            }
        }

        public bool IsTwoPair
        {
            get
            {
                return cards.GroupBy(card => card.Rank).Where(group => group.Take(3).Count() == 2).Take(3).Count() == 2;
            }
        }

        public bool IsThreeOfAKind
        {
            get
            {
                return cards.GroupBy(card => card.Rank).Any(group => group.Take(4).Count() == 3);
            }
        }

        public bool IsStraight
        {
            get
            {
                var ordered = cards.OrderBy(card => card.Rank).ToList();
                if (ordered[0].Rank == CardValue.Two && ordered[1].Rank == CardValue.Three && ordered[2].Rank == CardValue.Four && ordered[3].Rank == CardValue.Five && ordered[4].Rank == CardValue.Ace)
                {
                    return true;
                }
                for (int i = 0; i < ordered.Count - 1; i++)
                {
                    if (ordered[i].Rank + 1 != ordered[i + 1].Rank)
                    {
                        return false;
                    }
                }
                return true;
            }
        }

        public bool IsFlush
        {
            get
            {
                return cards.TrueForAll(card => card.Suit == cards[0].Suit);
            }
        }

        public bool IsFullHouse
        {
            get
            {
                return IsThreeOfAKind && IsOnePair;
            }
        }

        public bool IsFourOfAKind
        {
            get
            {
                return cards.GroupBy(card => card.Rank).Any(group => group.Take(5).Count() == 4);
            }
        }

        public bool IsStraightFlush
        {
            get
            {
                return IsFlush && IsStraight;
            }
        }

        public bool IsRoyalFlush
        {
            get
            {
                return IsFlush && IsStraight && cards.Exists(card => card.Rank == CardValue.Ace);
            }
        }

        public bool IsOnePair
        {
            get
            {
                return cards.GroupBy(card => card.Rank).Any(group => group.Take(3).Count() == 2);
            }
        }

        public void HoldCard(int index, bool value  = true)
        {
            cards[index].HoldCard = value;
        }
    }
}
