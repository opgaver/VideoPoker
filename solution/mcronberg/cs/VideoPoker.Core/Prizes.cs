using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoPoker.Core
{
    public class Prizes
    {

        public static readonly Prize RoyalFlush = new() { Name = "Royal Flush", Value = 800 };
        public static readonly Prize StraightFlush = new() { Name = "Straight Flush", Value = 50 };
        public static readonly Prize FourOfAKind = new() { Name = "Four of a Kind", Value = 25 };
        public static readonly Prize FullHouse = new() { Name = "Full House", Value = 9 };
        public static readonly Prize Flush = new() { Name = "Flush", Value = 6 };
        public static readonly Prize Straight = new() { Name = "Straight", Value = 4 };
        public static readonly Prize ThreeOfAKind = new() { Name = "Three of a Kind", Value = 3 };
        public static readonly Prize TwoPair = new() { Name = "Two Pair", Value = 2 };
        public static readonly Prize JacksOrBetter = new() { Name = "Jacks or Better", Value = 1 };
        public static readonly Prize AllOther = new() { Name = "All other", Value = 0 };
    }
}
