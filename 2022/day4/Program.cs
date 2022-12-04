internal class Program
{
    private class Elf
    {
        private int[] _endPoints = new int[2];
        public int[] EndPoints { get { return _endPoints; } }

        public Elf(string sections)
        {
            string[] endPoints = sections.Split('-');

            _endPoints[0] = int.Parse(endPoints[0]);
            _endPoints[1] = int.Parse(endPoints[1]);
        }

    }

    private class Pair
    {
        private Elf[] _elves = new Elf[2];

        public Pair(string assignments)
        {
            string[] sections = assignments.Split(',');

            _elves[0] = new Elf(sections[0]);
            _elves[1] = new Elf(sections[1]);
        }

        private Elf getElfWithLowestStart()
        {
            if (_elves[0].EndPoints[0] < _elves[1].EndPoints[0]) { return _elves[0]; }
            return _elves[1];
        }

        private Elf getElfWithHighestEnd()
        {
            if (_elves[0].EndPoints[1] > _elves[1].EndPoints[1]) { return _elves[0]; }
            return _elves[1];
        }

        private bool isAnyEndpointEqual()
        {
            if (_elves[0].EndPoints[0] == _elves[1].EndPoints[0]) { return true; }
            if (_elves[0].EndPoints[1] == _elves[1].EndPoints[1]) { return true; }

            return false;
        }


        public bool doesFullyOverlap()
        {
            if (isAnyEndpointEqual()) { return true; }

            return getElfWithLowestStart() == getElfWithHighestEnd();
        }

        public bool doesOverlappAtAll()
        {
            if (isAnyEndpointEqual()) { return true; }

            if (_elves[0].EndPoints[1] < _elves[1].EndPoints[0]) { return false; }
            if (_elves[1].EndPoints[1] < _elves[0].EndPoints[0]) { return false; }

            return true;
        }

    }

    private static void Main()
    {
        string[] input = File.ReadAllLines("input.txt");

        List<Pair> pairs = new List<Pair>();

        foreach (string assignment in input)
        {
            Pair pair = new Pair(assignment);
            pairs.Add(pair);
        }

        solvePartOne(pairs);
        solvePartTwo(pairs);
    }

    private static void solvePartOne(List<Pair> pairs)
    {
        List<Pair> fullyOverlappingPairs = new List<Pair>();

        foreach (Pair pair in pairs)
        {
            if (pair.doesFullyOverlap() == false) { continue; }
            fullyOverlappingPairs.Add(pair);
        }

        Console.WriteLine($"Part one: {fullyOverlappingPairs.Count}");
    }

    private static void solvePartTwo(List<Pair> pairs)
    {
        List<Pair> partlyOverlappingPairs = new List<Pair>();

        foreach (Pair pair in pairs)
        {
            if (pair.doesOverlappAtAll() == false) { continue; }
            partlyOverlappingPairs.Add(pair);
        }

        Console.WriteLine($"Part two: {partlyOverlappingPairs.Count}");
    }
}