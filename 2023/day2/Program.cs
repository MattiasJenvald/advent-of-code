using System.Text.RegularExpressions;

internal class Program
{
    private class GameData
    {
        private int _gameID = 0;
        private List<Dictionary<string, int>> _rounds = new List<Dictionary<string, int>>();

        public void SetID(int id)
        {
            _gameID = id;
        }

        public int GetID() { return _gameID; }

        public void SetRounds(List<Dictionary<string, int>> rounds)
        {
            _rounds = rounds;
        }

        public List<Dictionary<string, int>> GetRounds()
        {
            return _rounds;
        }
    }

    private static Dictionary<string, int> constraints = new Dictionary<string, int> { { "red", 12 }, { "green", 13 }, { "blue", 14 } };
    private static Regex colorReg = new Regex(@"(\d+) (red|green|blue)");

    private static void Main()
    {
        string[] input = File.ReadAllLines("input.txt");

        var allGamesdata = ProcessAllGames(input);

        int result_1 = CalculatePartOne(allGamesdata);
        int result_2 = calculatePartTwo(allGamesdata);

        Console.WriteLine($"Part one: {result_1}");
        Console.WriteLine($"Part two: {result_2}");
    }

    private static int CalculatePartOne(List<GameData> gameData)
    {
        int result = 0;

        foreach (var game in gameData)
        {
            bool validGame = true;

            foreach (var round in game.GetRounds())
            {
                if (ValidRound(round) == false)
                {
                    validGame = false;
                    break;
                }
            }

            if (validGame)
            {
                result += game.GetID();
            }
        }

        return result;
    }

    private static int calculatePartTwo(List<GameData> allGamesData)
    {
        int result = 0;

        foreach (GameData game in allGamesData)
        {
            Dictionary<string, int> maxAmount = new Dictionary<string, int> { { "red", 0 }, { "green", 0 }, { "blue", 0 } };

            foreach (Dictionary<string, int> round in game.GetRounds())
            {
                foreach (string color in round.Keys)
                {
                    if (maxAmount.ContainsKey(color) == false || round[color] > maxAmount[color])
                    {
                        maxAmount[color] = round[color];
                    }
                }
            }

            result += maxAmount["red"] * maxAmount["green"] * maxAmount["blue"];
        }

        return result;
    }

    private static bool ValidRound(Dictionary<string, int> roundData)
    {
        foreach (var kvp in roundData)
        {
            string color = kvp.Key;
            int number = kvp.Value;

            if (constraints.ContainsKey(color) == false || constraints[color] < number)
            {
                return false;
            }
        }

        return true;
    }

    private static List<GameData> ProcessAllGames(string[] input)
    {
        var allGamesData = new List<GameData>();
        Regex idReg = new Regex(@"Game (\d+):");

        foreach (string game in input)
        {
            int id = int.Parse(idReg.Match(game).Groups[1].Value);

            var gameRounds = new List<Dictionary<string, int>>();
            string[] rounds = game.Split(";");

            foreach (string round in rounds)
            {
                var roundData = ProcessRound(round);
                gameRounds.Add(roundData);
            }

            GameData gameData = new GameData();
            gameData.SetID(id);
            gameData.SetRounds(gameRounds);

            allGamesData.Add(gameData);
        }

        return allGamesData;
    }

    private static Dictionary<string, int> ProcessRound(string round)
    {
        var matches = colorReg.Matches(round);
        Dictionary<string, int> roundData = new Dictionary<string, int>();

        foreach (Match match in matches)
        {
            string color = match.Groups[2].Value;
            int number = int.Parse(match.Groups[1].Value);

            if (roundData.ContainsKey(color) == false || roundData[color] < number)
            {
                roundData[color] = number;
            }
        }

        return roundData;
    }
}