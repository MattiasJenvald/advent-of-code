using System.Text.RegularExpressions;

internal class Program
{
    private static void Main()
    {
        Regex symbolRegex = new Regex(@"[^\d.]");
        string[] input = File.ReadAllLines("input.txt");

        var visitedIndexes = new HashSet<string>();
        int result_1 = 0;
        int result_2 = 0;

        for (int i = 0; i < input.Length; i++)
        {
            MatchCollection matches = symbolRegex.Matches(input[i]);

            int[] dx = { -1, 0, 1, -1, 0, 1, -1, 0, 1 };
            int[] dy = { 1, 1, 1, 0, 0, 0, -1, -1, -1 };

            foreach (Match match in matches)
            {
                bool isGear = input[i][match.Index] == '*';
                List<int> numbersFound = new List<int>();

                for (int j = 0; j < 9; j++)
                {
                    int column = match.Index + dx[j];
                    int row = i + dy[j];

                    if (row < 0 || row >= input.Length || column < 0 || column >= input[i].Length) { continue; }
                    if (char.IsDigit(input[row][column]) == false) { continue; }
                    if (visitedIndexes.Contains($"{row},{column}")) { continue; }

                    visitedIndexes.Add($"{row},{column}");
                    List<char> charList = new List<char> { input[row][column] };

                    int left = column - 1;
                    while (left >= 0)
                    {
                        if (char.IsDigit(input[row][left]) == false) { break; }

                        visitedIndexes.Add($"{row},{left}");
                        charList.Insert(0, input[row][left]);
                        left--;
                    }

                    int right = column + 1;
                    while (right < input[i].Length)
                    {
                        if (char.IsDigit(input[row][right]) == false) { break; }

                        visitedIndexes.Add($"{row},{right}");
                        charList.Add(input[row][right]);
                        right++;
                    }

                    string numString = new string(charList.ToArray());
                    int number = Int32.Parse(numString);
                    result_1 += number;
                    numbersFound.Add(number);
                }

                if (isGear && numbersFound.Count == 2)
                {
                    result_2 += numbersFound[0] * numbersFound[1];
                }
            }
        }

        Console.WriteLine($"Part one: {result_1}");
        Console.WriteLine($"Part twoo: {result_2}");
    }
}