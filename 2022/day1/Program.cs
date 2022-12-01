internal class Program
{
    private class Elf
    {
        public int Calories;
    }

    private class Expedition
    {
        private List<Elf> _elves = new List<Elf>();

        private void createNewElf()
        {
            _elves.Add(new Elf());
        }

        private void addCaloriesToLastElf(int newCalories)
        {
            Elf elf = _elves[_elves.Count - 1];

            elf.Calories += newCalories;
        }

        public void CalculateElfCalories(string[] input)
        {
            createNewElf();

            foreach (String s in input)
            {
                if (s == "")
                {
                    createNewElf();
                    continue;
                }

                addCaloriesToLastElf(Int32.Parse(s));
            }
        }

        public int getTotalCaloresFromTopNumberOfElves(int numberOfElvesToFind)
        {
            List<Elf> candidates = new List<Elf>(_elves);
            List<Elf> topElves = new List<Elf>();

            while (topElves.Count < numberOfElvesToFind)
            {
                Elf currentTopElf = getElfWithHighestCalories(candidates);

                topElves.Add(currentTopElf);
                candidates.Remove(currentTopElf);
            }

            int totalCalores = 0;
            foreach (Elf elf in topElves)
            {
                totalCalores += elf.Calories;
            }

            return totalCalores;
        }

        private Elf getElfWithHighestCalories(List<Elf> elves)
        {
            Elf topElf = new Elf();
            foreach (Elf elf in elves)
            {
                if (elf.Calories > topElf.Calories)
                {
                    topElf = elf;
                }
            }

            return topElf;
        }

    }

    private static void Main()
    {
        string[] input = File.ReadAllLines("input.txt");

        Expedition expedition = new Expedition();
        expedition.CalculateElfCalories(input);

        int part1 = expedition.getTotalCaloresFromTopNumberOfElves(1);
        int part2 = expedition.getTotalCaloresFromTopNumberOfElves(3);

        Console.WriteLine($"Part 1: {part1}");
        Console.WriteLine($"Part 2: {part2}");
    }

}