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
            // split input string by commas then cast to ints inside a tuple array
            // tuple item 1 is number, item 2 is age1, item 3 is age2
            var data = Array.ConvertAll(
                                input.Split(','),
                                x => new Number(int.Parse(x), null, null)
                             ).ToList();

            // we start on turn 1, no 0 indexing
            int turn = 1;
            Number lastNumberSpoken = null;

            // handle special case for turns relating to initial numbers
            for (int i = 0; i < data.Count; i++)
            {
                data[i].lastSpoken = turn;
                lastNumberSpoken = data[i];
                turn++;
            }

            for (int i = turn; i <= targetTurn; i++)
            {
                int currentNumberValue = 0;

                if(lastNumberSpoken.spokenBeforeThat != null)
                {
                    currentNumberValue = lastNumberSpoken.GetGap();
                }

                // get object for this number if it exists.
                var currentNumber = data.Where(x => x.value == currentNumberValue).SingleOrDefault();

                // if it doesn't exist then create it
                if (currentNumber == null)
                {
                    currentNumber = new Number(currentNumberValue, null, null);
                    data.Add(currentNumber);
                }

                currentNumber.spokenBeforeThat = currentNumber.lastSpoken;
                currentNumber.lastSpoken = turn;

                lastNumberSpoken = currentNumber;

                turn++;
                
                if(turn == targetTurn+1)
                {
                    //var x = data.OrderBy(x => x.value).ToList();
                    return currentNumberValue;
                }
            }

            // this should not be reached
            return -1;
        }

        private class Number
        {
            public int value = 0;
            public int? lastSpoken = null;
            public int? spokenBeforeThat = null;

            public Number(int value, int? lastSpoken, int? spokenBeforeThat)
            {
                this.value = value;
                this.lastSpoken = lastSpoken;
                this.spokenBeforeThat = spokenBeforeThat;
            }

            public int GetGap()
            {
                return lastSpoken.GetValueOrDefault() - spokenBeforeThat.GetValueOrDefault();
            }
        }
    }
}
