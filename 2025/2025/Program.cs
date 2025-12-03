using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    private static readonly Dictionary<int, Action<string[]>> Solvers = new()
    {
        [1] = Day01.Solve,
        [2] = Day02.Solve,
    };

    static void Main(string[] args)
    {
        Console.WriteLine("Enter day number: ");
        int day = int.Parse(Console.ReadLine()!);

        var inputPath = Path.Combine("inputs", $"day{day:00}.txt");

        if (File.Exists(inputPath) == false)
        {
            Console.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        string[] input = File.ReadAllLines(inputPath);

        if (Solvers.TryGetValue(day, out var solver))
        {
            solver(input);
        }
        else
        {
            Console.WriteLine("Day not implemented yet.");
        }
    }
}