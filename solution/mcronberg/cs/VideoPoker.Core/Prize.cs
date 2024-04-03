﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoPoker.Core
{
    public class Prize
    {
        public string? Name { get; set; }
        public int Value { get; set; }

        public int Payout(int bet = 1)
        {
            return bet * Value;
        }

    }
}
