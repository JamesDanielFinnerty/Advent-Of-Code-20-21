using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode
{
    public static class Day12
    {
        public static int Part1(string[] input)
        {
            var theBoat = new Ship();

            foreach (var instruction in input)
            {
                char cmd = instruction.First();
                int value = int.Parse(instruction.Substring(1));
                theBoat.ProcessCommandPart1(cmd, value);
            }

            return theBoat.GetManhatten();
        }

        public static int Part2(string[] input)
        {
            var theBoat = new Ship();

            foreach (var instruction in input)
            {
                char cmd = instruction.First();
                int value = int.Parse(instruction.Substring(1));
                theBoat.ProcessCommandPart2(cmd, value);
            }

            return theBoat.GetManhatten();
        }

        public class Ship
        {
            int x = 0;
            int y = 0;
            int orient = 90;

            int wayX = 10;
            int wayY = 1;

            public Ship()
            {

            }

            public void ProcessCommandPart2(char command, int value)
            {
                if (command == 'N') { this.wayY += value; }
                else if (command == 'E') { this.wayX += value; }
                else if (command == 'S') { this.wayY -= value; }
                else if (command == 'W') { this.wayX -= value; }
                else if (command == 'R') { RotateWayPoint(value); }
                else if (command == 'L') { RotateWayPoint(-value); }
                else if (command == 'F')
                {
                    for (int i = 0; i < value; i++)
                    {
                        x += wayX;
                        y += wayY;
                    }
                }

            }

            public void ProcessCommandPart1(char command, int value)
            {

                if (command == 'R' || command == 'L' || command == 'F')
                {
                    this.Turn(command, value);
                }
                else
                {
                    if (command == 'N') { this.y += value; }
                    else if (command == 'E') { this.x += value; }
                    else if (command == 'S') { this.y -= value; }
                    else if (command == 'W') { this.x -= value; }
                }
            }

            public int GetManhatten()
            {
                return Math.Abs(x) + Math.Abs(y);
            }
            private void Turn(char command, int value)
            {
                orient += 360;

                if (command == 'R') { orient += value; }
                else if (command == 'L') { orient -= value; }

                orient = orient % 360;

                if (command == 'F')
                {

                    if (orient == 0) { this.y += value; }
                    else if (orient == 90) { this.x += value; }
                    else if (orient == 180) { this.y -= value; }
                    else if (orient == 270) { this.x -= value; }
                }
            }

            private void RotateWayPoint(double angleInDegrees)
            {
                double angleInRadians = -1 * angleInDegrees * (Math.PI / 180);
                double cosTheta = Math.Cos(angleInRadians);
                double sinTheta = Math.Sin(angleInRadians);
                double tempX = (cosTheta * (this.wayX) - sinTheta * (this.wayY));
                double tempY = (sinTheta * (this.wayX) + cosTheta * (this.wayY));

                this.wayX = (int)Math.Round(tempX);
                this.wayY = (int)Math.Round(tempY);
            }
        }
    }
}
