<Query Kind="Statements" />

string[] lines = System.IO.File.ReadAllLines(@"C:\Users\James\Documents\AdventOfCode\day3input.txt");

var totalRows = lines.Count();
var counts = new int[lines[0].Length];
string gamma = "";
string epsilon = "";

Console.WriteLine(counts);

foreach(var line in lines){
	for(int i = 0; i < line.Length; i++){
		if(line[i].ToString() == "1"){
			counts[i]++;
		}
	}			
}

foreach(var count in counts){

	Console.WriteLine(count);

	if(count > (totalRows/2))
	{
		gamma += "1";
		epsilon += "0";
	}
	else
	{
		gamma += "0";
		epsilon += "1";
	}
}

Console.WriteLine(gamma);
Console.WriteLine(epsilon);