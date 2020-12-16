using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode
{
    public static class Day16
    {
        public static long Part1(string[] lines)
        {
            int rulesEndIndex = 0;
            int nearbyTicketsIndex = 0;

            for (int i=0; i<lines.Length; i++)
            {
            
                if (lines[i] == "your ticket:")
                {
                    rulesEndIndex = i - 1;
                }

                if (lines[i] == "nearby tickets:")
                {
                    nearbyTicketsIndex = i + 1;
                }
            }

            var rules = new List<Tuple<int, int>>();

            for (int i =0; i < rulesEndIndex; i++)
            {
                var rule1 = lines[i].Split(':')[1].Split(" or ")[0].Trim();
                var rule2 = lines[i].Split(':')[1].Split(" or ")[1].Trim();

                rules.Add(new Tuple<int, int>(int.Parse(rule1.Split('-')[0]), int.Parse(rule1.Split('-')[1])));
                rules.Add(new Tuple<int, int>(int.Parse(rule2.Split('-')[0]), int.Parse(rule2.Split('-')[1])));
            }

            int result = 0;

            for (int i = nearbyTicketsIndex; i < lines.Length; i++)
            {
                result += SumOfInvalidNumbers(lines[i], rules);
            }

            return result;
        }

        private static int SumOfInvalidNumbers(string ticket, List<Tuple<int,int>> rules)
        {
            int result = 0;

            var numbers = Array.ConvertAll(ticket.Split(','), s => int.Parse(s));

            foreach(var number in numbers)
            {
                var isValid = false;

                foreach (var rule in rules)
                {
                    if(number >= rule.Item1 && number <= rule.Item2)
                    {
                        isValid = true;
                    }

                }

                if (!isValid) { result += number; };
            }

            return result;
        }
    }
}
