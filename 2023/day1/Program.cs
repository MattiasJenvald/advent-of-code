using System.Text.RegularExpressions;

internal class Program
{
    private static void Main(string[] args)
    {
        string[] lines = File.ReadAllLines("input.txt");

        var digitDictionary = new Dictionary<string, int>
        {
            {"zero", 0}, {"one", 1}, {"two",2}, {"three",3},
            {"four", 4}, {"five", 5}, {"six", 6},
            {"seven", 7}, {"eight", 8}, {"nine", 9}
        };

        string pattern1 = "\\d|(" + string.Join("|", digitDictionary.Values) + ")";
        string pattern2 = "\\d|(" + string.Join("|", digitDictionary.Keys) + ")";

        Regex regex1 = new Regex(pattern1);
        Regex regex2 = new Regex(pattern2);

        int result1 = 0;
        int result2 = 0;

        try
        {
            foreach (string line in lines)
            {
                result1 += ProcessLine(line, regex1, digitDictionary);
                result2 += ProcessLine(line, regex2, digitDictionary);
            }

            Console.WriteLine($"part one: {result1}");
            Console.WriteLine($"part two: {result2}");
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error: {e.Message}");
        }
    }

    private static int ProcessLine(string line, Regex regex, Dictionary<string, int> dictionary)
    {
        var matches = regex.Matches(line);
        if (matches.Count == 0) { Console.WriteLine($"No matches found on line: {line}"); return 0; }

        int? firstLeftMatch = ConvertMatchToInt(matches[0].Value, dictionary);
        int? firstRightMatch = ConvertMatchToInt(matches[matches.Count - 1].Value, dictionary);

        return ParseNumber(firstLeftMatch.ToString(), firstRightMatch.ToString());
    }

    private static int? ConvertMatchToInt(string s, Dictionary<string, int> dictionary)
    {
        // input is digit
        if (int.TryParse(s, out int number)) { return number; }

        // input is string
        if (dictionary.TryGetValue(s, out number)) { return number; }

        // invalid input
        Console.WriteLine("Invalid input when converting match to int!");
        return null;
    }

    private static int ParseNumber(string? first, string? second)
    {
        if (first == null) { Console.WriteLine("First digit is invalid!"); return 0; }
        if (second == null) { Console.WriteLine("Second digit is invalid!"); return 0; }

        return int.Parse($"{first}{second}");
    }
}