using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoPoker.Core
{
    public class Game
    {
        private int wallet;
        private int bet;        
        private PokerHand? hand;
        private Deck? deck;

        public Game(int wallet = 25)
        {
            this.wallet = wallet;
        }

        public void FirstDraw(int bet)
        {
            this.wallet -= bet;
            this.bet = bet;
            this.deck = Deck.CreateDeck();
            this.deck.Shuffle();
            this.deck.Flip();
            hand = new PokerHand(deck.DealCards(5));

        }
        public Prize SecondDraw(int[] holds)
        {
            foreach (var hold in holds)
            {
                hand?.HoldCard(hold);
            }
            for (int i = 0; i < 5; i++)
            {
                if (!hand![i].HoldCard)
                    hand[i] = deck!.DealCard();
            }
            var prize = FindPrize();
            this.wallet += prize.Value * bet;
            return prize;
        }

        public Prize FindPrize()
        {

            if (hand!.IsRoyalFlush)
                return Prizes.RoyalFlush;
            if (hand!.IsStraightFlush)
                return Prizes.StraightFlush;
            if (hand!.IsFourOfAKind)
                return Prizes.FourOfAKind;
            if (hand!.IsFullHouse)
                return Prizes.FullHouse;
            if (hand!.IsFlush)
                return Prizes.Flush;
            if (hand!.IsStraight)
                return Prizes.Straight;
            if (hand!.IsThreeOfAKind)
                return Prizes.ThreeOfAKind;
            if (hand!.IsTwoPair)
                return Prizes.TwoPair;
            if (hand!.IsJacksOrBetter)
                return Prizes.JacksOrBetter;
            return Prizes.AllOther;
        }

        public int Wallet
        {
            get
            {
                return wallet;
            }
        }

        public PokerHand Hand
        {
            get
            {
                return hand!;
            }
        }

    }
}
