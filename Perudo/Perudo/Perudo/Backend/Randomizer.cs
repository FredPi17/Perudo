﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Randomizer
    {
        private Random random = new Random();

        public int Next(int min, int max)
        {
            return random.Next(min, max);
        }

    }
}
