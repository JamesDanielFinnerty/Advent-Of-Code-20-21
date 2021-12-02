<Query Kind="Statements" />

var data = MyExtensions.ReadTextFile(@"C:\Users\Jfinnerty\Documents\aoc2021_day2.txt");

int aim = 0;
int horizontal = 0;
int depth = 0;

foreach(var line in data)
{
	var command = line.Split(' ')[0];
	var val = int.Parse(line.Split(' ')[1]);

	if(command == "forward")
	{
		horizontal += val;
		depth += (aim * val);
	}
	
	if(command == "down")
	{
		aim += val;
	}
	
	if(command == "up")
	{
		aim -= val;
	}	
}

Console.WriteLine(horizontal * depth);