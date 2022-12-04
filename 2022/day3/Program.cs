internal class Program
{
    public static char[] Alphabet = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };
    public static Dictionary<char, int> Priorities = new Dictionary<char, int>();

    private class Group
    {
        private Rucksack[] _rucksacks;

        public Group(Rucksack r1, Rucksack r2, Rucksack r3)
        {
            _rucksacks = new Rucksack[] { r1, r2, r3 };
        }

        public string getIdentificationBadge()
        {
            string firstCheck = getCharsFromAinB(_rucksacks[0].TotalSupplies, _rucksacks[1].TotalSupplies);
            string secondCheck = getCharsFromAinB(firstCheck, _rucksacks[2].TotalSupplies);

            return secondCheck;
        }

        private string getCharsFromAinB(string a, string b)
        {
            string result = "";

            foreach (char c in a)
            {
                if (result.Contains(c)) { continue; }
                if (b.Contains(c) == false) { continue; }

                result += c;
            }

            return result;
        }
    }

    private class Rucksack
    {
        private string _compartment1;
        private string _compartment2;

        public string TotalSupplies { get { return _compartment1 + _compartment2; } }

        private char[] _duplicates;

        public Rucksack(string items)
        {
            int half = items.Length / 2;

            _compartment1 = items.Substring(0, half);
            _compartment2 = items.Substring(half, half);
            _duplicates = findDuplicates();
        }

        private char[] findDuplicates()
        {
            List<char> foundDuplicates = new List<char>();

            foreach (char item in _compartment1)
            {
                bool alreadyFound = foundDuplicates.Contains(item);
                if (alreadyFound) { continue; }

                bool duplicateFound = _compartment2.Contains(item);
                if (duplicateFound == false) { continue; }

                foundDuplicates.Add(item);
            }

            return foundDuplicates.ToArray();
        }

        public int getPriorityOfDuplicates()
        {
            int total = 0;

            foreach (char item in _duplicates)
            {
                total += Priorities[item];
            }

            return total;
        }
    }

    public static void createDictionary()
    {
        for (int i = 0; i < 26; i++)
        {
            Priorities.Add(Alphabet[i], i + 1);
        }

        for (int i = 26; i < 52; i++)
        {
            Priorities.Add(Char.ToUpper(Alphabet[i - 26]), i + 1);
        }
    }

    private static void Main()
    {
        createDictionary();
        string[] input = File.ReadAllLines("input.txt");

        List<Rucksack> rucksacks = new List<Rucksack>();

        foreach (string supplies in input)
        {
            Rucksack rucksack = new Rucksack(supplies);
            rucksacks.Add(rucksack);
        }

        solvePartOne(rucksacks);
        solvePartTwo(rucksacks);
    }

    private static void solvePartOne(List<Rucksack> rucksacks)
    {
        int totalPriority = 0;
        foreach (Rucksack rucksack in rucksacks)
        {
            totalPriority += rucksack.getPriorityOfDuplicates();
        }

        Console.WriteLine($"Part one: {totalPriority}");
    }

    private static void solvePartTwo(List<Rucksack> rucksacks)
    {
        int totalPriority = 0;

        for (int i = 0; i < rucksacks.Count; i += 3)
        {
            Group group = new Group(rucksacks[i], rucksacks[i + 1], rucksacks[i + 2]);

            totalPriority += Priorities[group.getIdentificationBadge()[0]];
        }

        Console.WriteLine($"Part two: {totalPriority}");
    }
}