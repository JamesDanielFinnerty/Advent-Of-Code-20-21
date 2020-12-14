using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode
{
    public static class Day14
    {
        public static long Part1(string[] lines)
        {
            string currentMask = "";

            // Create array emulating memory. Dirty but initialised to 100,000
            // as this is larger than any location in the given data set
            var memory = new Int64[100000];

            foreach (var line in lines)
            {
                // first three chars of line signify instruction type
                string command = line.Substring(0, 3);

                if (command == "mas")
                {
                    // if mask, set current mask
                    currentMask = ParseMaskFromLine(line);
                }
                else if (command == "mem")
                {
                    // if mem, parse out values
                    int memLocation = ParseMemoryLocationFromLine(line);
                    int memValue = ParseMemoryValueFromLine(line);

                    string binaryValue = ConvertToBinary(memValue);

                    StringBuilder output = new StringBuilder();

                    for (int i = 0; i < 36; i++)
                    {
                        if (currentMask[i] != 'X')
                        {
                            output.Append(currentMask[i]);
                        }
                        else
                        {
                            output.Append(binaryValue[i]);
                        }
                    }

                    memory[memLocation] = Convert.ToInt64(output.ToString(), 2);
                }
            }

            // sum mem locations to get solution
            return memory.Sum();
        }

        

        public static long Part2(string[] lines)
        {
            // Use Sparse List for memory locations
            var memory = new List<Tuple<long, long>>();

            string currentMask = "";

            foreach (var line in lines)
            {
                // first three chars of line signify instruction type
                string command = line.Substring(0, 3);

                if (command == "mas")
                {
                    // if instruction is a new mask, parse mask from line then set.
                    currentMask = ParseMaskFromLine(line);
                }
                else if (command == "mem")
                {
                    // parse out values
                    int memLocation = ParseMemoryLocationFromLine(line);
                    int memValue = ParseMemoryValueFromLine(line);

                    string binaryValue = ConvertToBinary(memLocation);

                    StringBuilder output = new StringBuilder();

                    // iterate through bits applying mask
                    for (int i = 0; i < 36; i++)
                    {
                        if (currentMask[i] == 'X')
                        {
                            output.Append('X');
                        }
                        else if (currentMask[i] == '0')
                        {
                            output.Append(binaryValue[i]);
                        }
                        else if (currentMask[i] == '1')
                        {
                            output.Append('1');
                        }
                    }

                    string complete = output.ToString();

                    // count X's in result. This tells us how many wildcard address bits
                    // there are. Squaring this value, gives us the number of mem
                    // locastions we will end up writing too.
                    int max = complete.Count(x => x == 'X');
                    max = max * max;

                    // create locations working list and seed with initial mask
                    var locations = new List<String>();
                    locations.Add(complete);

                    int count = 0;

                    // hacky while true loop. For loops fail due to modifying collection as we go
                    while (true)
                    {
                        for (int i = 0; i < 36; i++)
                        // for each bit in our address
                        {
                            string loc = locations[count];
                            if (loc[i] == 'X')
                            // where the bit is a wildcard X
                            {
                                // generate and add 2 child floating addresses replacing X for 1 and 0
                                locations.Add(loc.Substring(0, i) + "1" + loc.Substring(i + 1));
                                locations.Add(loc.Substring(0, i) + "0" + loc.Substring(i + 1));

                                // remove parent address from list and 
                                // decrement count to reflect this removal
                                locations.Remove(loc);
                                count--;

                                // break out of the current floating address as we only
                                // wish to substitute one X per iteration
                                break;
                            }

                        }

                        // increment count
                        count++;

                        // if count is now at the max address, break
                        if (count == max - 1) { break; }
                    }

                    foreach (var location in locations)
                    {
                        long workingLocation = Convert.ToInt64(location, 2);

                        // if location is already set, then remove it so we replace below
                        memory.RemoveAll(x => x.Item1 == workingLocation);

                        // store val for location
                        memory.Add(new Tuple<long, long>(workingLocation, memValue));
                    }
                }
            }

            // sum mem vals to get solution
            return memory.Sum(x => x.Item2);
        }

        private static string ConvertToBinary(int inputNumber)
        {
            // convert input to binary then add leading 36 zeros to fill out to 36+ bits
            string binaryValue = "000000000000000000000000000000000000" + Convert.ToString(inputNumber, 2);

            // calc how many surplus zeros
            int len = binaryValue.Length - 36;

            // trim leading surplus then return
            return binaryValue.Substring(len);
        }

        private static int ParseMemoryLocationFromLine(string line)
        {
            return int.Parse(line.Split('=')[0].TrimEnd().Replace("]", string.Empty).Substring(4));
        }

        private static int ParseMemoryValueFromLine(string line)
        {
            return int.Parse(line.Split('=')[1].TrimStart());
        }

        private static string ParseMaskFromLine(string line)
        {
            return line.Split('=')[1].TrimStart();
        }
    }
}
