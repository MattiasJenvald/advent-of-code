using System;
using System.Collections.Generic;

static class Day01
{
    public static void Solve(string[] input)
    {
        Console.WriteLine("Solving day 1...");

        Console.WriteLine($"Part 1: {SolvePart1(input)}");
        Console.WriteLine($"Part 2: {SolvePart2(input)}");
    }

    private static int SolvePart1(string[] input)
    {
        int result = 0;
        int dial = 50;

        foreach (string s in input)
        {
            if (string.IsNullOrEmpty(s)) { continue; }

            char direction = s[0];
            if (direction is not 'L' and not 'R') { continue; }

            if (int.TryParse(s.AsSpan(1), out int clicks))
            {
                int realClicks = clicks % 100;
                int multiplier = direction == 'L' ? -1 : 1;

                dial += multiplier * realClicks;

                if (dial < 0) { dial += 100; }
                else if (dial > 99) { dial -= 100; }

                if (dial == 0) { result++; }
            }
        }

        return result;
    }

    private static int SolvePart2(string[] input)
    {
        return 0;
    }
}