using System;
using System.Collections.Generic;
using System.Linq;
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

        public static long Part2(string[] lines)
        {
            int rulesEndIndex = 0;
            int nearbyTicketsIndex = 0;

            for (int i = 0; i < lines.Length; i++)
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

            var rules = new List<Rule>();

            for (int i = 0; i < rulesEndIndex; i++)
            {
                rules.Add(new Rule(lines[i]));
            }

            var validTickets = new List<string>();

            for (int i = nearbyTicketsIndex; i < lines.Length; i++)
            {
                if(IsTicketValid(lines[i], rules))
                {
                    validTickets.Add(lines[i]);
                }
            }

            int rulesCount = rules.Count;

            var outputRules = new List<int>();

            for (int columnNo = 0; columnNo < rulesCount; columnNo++)
            {
                var validRules = new List<Rule>();

                foreach (var thisRule in rules)
                {
                    bool success = true;

                    foreach(var validTicket in validTickets)
                    {
                        var number = int.Parse(validTicket.Split(',')[columnNo]);

                        var isThisNumberValid = thisRule.IsNumberValid(number);

                        if (!isThisNumberValid)
                        {
                            success = false;
                        }
                    }

                    if (success)
                    {
                        validRules.Add(thisRule);
                    }

                }

                if (validRules.Count == 1)
                { 
                    if(validRules.Single().name.StartsWith("depart"))
                    {
                        outputRules.Add(columnNo);
                    }
                    
                    rules.Remove(validRules.Single());
                    columnNo = -1;
                }
            }

            // formatting
            string myTicket = "109,199,223,179,97,227,197,151,73,79,211,181,71,139,53,149,137,191,83,193";

            var myNumbers = Array.ConvertAll(myTicket.Split(','), s => int.Parse(s)).ToList();

            long result = 1;

            foreach(var rule in outputRules)
            {
                result = result * myNumbers[rule];
            }

            return result;
        }

        public class Rule
        {
            public string name = "";
            int rule1a = 0;
            int rule1b = 0;
            int rule2a = 0;
            int rule2b = 0;

            public Rule(string line)
            {
                this.name = line.Split(':')[0].Trim();
                var rule1 = line.Split(':')[1].Split(" or ")[0].Trim();
                var rule2 = line.Split(':')[1].Split(" or ")[1].Trim();

                rule1a = int.Parse(rule1.Split('-')[0]);
                rule1b = int.Parse(rule1.Split('-')[1]);

                rule2a = int.Parse(rule2.Split('-')[0]);
                rule2b = int.Parse(rule2.Split('-')[1]);
            }

            public bool IsNumberValid(int number)
            {
                bool inFirstRange = (number >= this.rule1a && number <= this.rule1b);
                bool inSecondRange = (number >= this.rule2a && number <= this.rule2b);

                return inFirstRange || inSecondRange;
            }
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

        private static bool IsTicketValid(string ticket, List<Rule> rules)
        {
            bool result = true;

            var numbers = Array.ConvertAll(ticket.Split(','), s => int.Parse(s));

            foreach (var number in numbers)
            {
                var isValid = false;

                foreach (var rule in rules)
                {
                    if (rule.IsNumberValid(number))
                    {
                        isValid = true;
                    }

                }

                if (!isValid) { result = false; };
            }

            return result;
        }
    }
}
