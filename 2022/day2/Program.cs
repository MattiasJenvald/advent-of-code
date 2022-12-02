internal class Program
{
    private static Shape getShapeByCurrent(Char character, Shape[] shapes)
    {
        foreach (Shape shape in shapes)
        {
            if (shape._current == character) { return shape; }
        }

        Console.WriteLine($"Hittar ingen shape med char {character}");
        return new Shape('-', '-', '-', '-');
    }

    private class Shape
    {
        public char _current;
        public char _win;
        public char _draw;
        public char _lose;

        public Shape(char current, char win, char draw, char lose)
        {
            _current = current;
            _win = win;
            _draw = draw;
            _lose = lose;
        }

        public int getScoreFromSelection()
        {
            if (_current == 'X' || _current == 'A') { return 1; }
            if (_current == 'Y' || _current == 'B') { return 2; }
            if (_current == 'Z' || _current == 'C') { return 3; }

            Console.WriteLine($"{_current} is not a valid shape.");
            return -1;
        }

        public int getScoreFromOutcome(char opponentShape)
        {
            if (opponentShape == _win) { return 6; }
            if (opponentShape == _draw) { return 3; }
            if (opponentShape == _lose) { return 0; }

            Console.WriteLine($"{opponentShape} is not a valid shape.");
            return -1;
        }
    }

    private static void Main()
    {
        string[] input = File.ReadAllLines("input.txt");

        Console.WriteLine($"part 1: {getPartOne(input)}\nPart 2: {getPartTwo(input)}");
    }

    private static int getPartOne(string[] input)
    {
        Shape Rock = new Shape('X', 'C', 'A', 'B');
        Shape Paper = new Shape('Y', 'A', 'B', 'C');
        Shape Scissors = new Shape('Z', 'B', 'C', 'A');

        Shape[] shapes = { Rock, Paper, Scissors };

        int totalScore = 0;

        foreach (string s in input)
        {
            char opponent = s[0];
            char player = s[2];

            Shape playerShape = getShapeByCurrent(player, shapes);

            totalScore += playerShape.getScoreFromSelection();
            totalScore += playerShape.getScoreFromOutcome(opponent);
        }

        return totalScore;
    }

    private static int getPartTwo(string[] input)
    {
        Shape Rock = new Shape('A', 'C', 'A', 'B');
        Shape Paper = new Shape('B', 'A', 'B', 'C');
        Shape Scissors = new Shape('C', 'B', 'C', 'A');

        Shape[] shapes = { Rock, Paper, Scissors };

        int totalScore = 0;

        foreach (string s in input)
        {
            char opponent = s[0];
            char desiredOutcome = s[2];

            Shape elfShape = getShapeByCurrent(opponent, shapes);
            Shape playerShape = new Shape(',', ',', ',', ',');

            switch (desiredOutcome)
            {
                case 'X':
                    playerShape = getShapeByCurrent(elfShape._win, shapes);
                    break;

                case 'Y':
                    playerShape = getShapeByCurrent(elfShape._draw, shapes);
                    break;

                case 'Z':
                    playerShape = getShapeByCurrent(elfShape._lose, shapes);
                    break;
            }

            totalScore += playerShape.getScoreFromSelection();
            totalScore += playerShape.getScoreFromOutcome(opponent);
        }

        return totalScore;
    }
}