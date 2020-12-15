using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode
{
    public static class Day15
    {
        public static int Solve(string input, long targetTurn)
        {
            var numbers = Array.ConvertAll(
                                input.Split(','),
                                x => int.Parse(x)
                             );

            // create array to cover all turns we need to simulate
            var turns = new int[targetTurn];

            // use -1 as a null val.
            Array.Fill(turns, -1);

            var turn = 1;

            // load given starter turns into our array
            for (; turn < numbers.Length + 1; turn++)
            {
                turns[numbers[turn - 1]] = turn;
            }

            // first spoken number after starters will always be 0.
            var currentNumber = 0;

            for (; turn < targetTurn; turn++)
            {
                // get and store the previous time our current number was spoken
                var previousTurn = turns[currentNumber];
                // overwrite in the area with this turn
                turns[currentNumber] = turn;

                // if previous time spoken was not null, then set
                // current number as the differance
                // else speak 0
                if(previousTurn != -1)
                {
                    currentNumber = turn - previousTurn;
                }
                else
                {
                    currentNumber = 0;
                }
            }

            return currentNumber;
        }
    }
}
