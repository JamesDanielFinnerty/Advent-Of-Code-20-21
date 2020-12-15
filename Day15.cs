using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode
{
    public static class Day15
    {
        public static int Part1(string input, long targetTurn)
        {
            var numbers = Array.ConvertAll(
                                input.Split(','),
                                x => int.Parse(x)
                             );

            var turns = new int[30000000];
            Array.Fill(turns, -1);

            var turn = 1;

            for (; turn < numbers.Length + 1; turn++)
            {
                turns[numbers[turn - 1]] = turn;
            }

            var currentNumber = 0;

            for (; turn < targetTurn; turn++)
            {
                var previousTurn = turns[currentNumber];
                turns[currentNumber] = turn;

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
