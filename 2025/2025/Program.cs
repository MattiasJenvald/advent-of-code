using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    private static readonly Dictionary<int, Action<string>> Solvers = new();
    static void Main(string[] args)
    {
        Console.WriteLine("Enter day number: ");
        int day = int.Parse(Console.ReadLine()!);

        var inputPath = Path.Combine("inputs", $"day{day:00}.txt}");
        if (File.Exists(inputPath) == false)
        {
            Console.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        var input = File.ReadAllText(inputPath);


    }
}