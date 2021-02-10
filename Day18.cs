using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode
{
    public static class Day18
    {
        public static int Part1(string formula)
        {
            formula = formula.Replace("(", "( ");
            formula = formula.Replace(")", " )");
            var splitItems = formula.Split(' ');
            Array.Reverse(splitItems);

            var stack = new Stack<object>(splitItems);

            int result = 0;

            while (stack.Count > 0)
            {
                if (stack.Count == 1) { return int.Parse(stack.Pop().ToString()); }

                var left = int.Parse(stack.Pop().ToString());
                var op = stack.Pop();
                var right = int.Parse(stack.Pop().ToString());

                if (op.ToString() == "*") 
                {
                    result = left*right;
                }

                if (op.ToString() == "+")
                {
                    result = left + right;
                }
            }

            stack.Push(result.ToString());

            return result;
        }

        private static int EvaluateInner(string inner)
        {
            int result = 0;



            return result;
        }
    }
}
