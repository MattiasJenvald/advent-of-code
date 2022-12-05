internal class Program
{
    private class LoadingBay
    {
        private Stack[] _stacks;

        public string GetCurrentTopLayer()
        {
            string result = "";

            foreach (Stack stack in _stacks)
            {
                result += stack.GetCrateAtTop();
            }

            return result;
        }

        public void CrateMover9000(List<String[]> instructions)
        {
            foreach (string[] s in instructions)
            {
                int amount = int.Parse(s[1]);
                int originIndex = int.Parse(s[3]) - 1;
                int targetIndex = int.Parse(s[5]) - 1;

                for (int i = 0; i < amount; i++)
                {
                    char crateToMove = _stacks[originIndex].GetCrateAtTop();

                    _stacks[targetIndex].AddToStack(crateToMove);
                    _stacks[originIndex].RemoveTopFromStack();
                }
            }
        }

        public void CrateMover9001(List<String[]> instructions)
        {
            foreach (string[] s in instructions)
            {
                int amount = int.Parse(s[1]);
                int originIndex = int.Parse(s[3]) - 1;
                int targetIndex = int.Parse(s[5]) - 1;

                List<char> crates = new List<char>();
                for (int i = 0; i < amount; i++)
                {
                    char crate = _stacks[originIndex].GetCrateAtTop();

                    crates.Add(crate);
                    _stacks[originIndex].RemoveTopFromStack();
                }

                crates.Reverse();

                foreach (char crate in crates)
                {
                    _stacks[targetIndex].AddToStack(crate);
                }
            }
        }

        #region setup

        public LoadingBay(string[] input)
        {
            _stacks = createEmptyStacks();
            string[] originState = GetOriginState(input);
            fillStacksToStartCondition(originState);
        }

        private void fillStacksToStartCondition(string[] originState)
        {
            for (int i = 0; i < originState.Length; i++)
            {
                string cratesOnLayer = "";

                for (int j = 1; j < originState[i].Length; j += 4)
                {
                    cratesOnLayer += originState[i][j];
                }

                for (int k = 0; k < _stacks.Length; k++)
                {
                    if (cratesOnLayer[k] == ' ') { continue; }
                    _stacks[k].AddToStack(cratesOnLayer[k]);
                }
            }

            foreach (Stack stack in _stacks)
            {
                stack.ReverseOrderOfStack();
            }
        }

        private static Stack[] createEmptyStacks()
        {
            List<Stack> stacks = new List<Stack>();
            for (int i = 0; i < 9; i++)
            {
                stacks.Add(new Stack());
            }

            return stacks.ToArray();
        }

        private static string[] GetOriginState(string[] input)
        {
            List<string> originState = new List<string>();

            foreach (string s in input)
            {
                foreach (char c in s)
                {
                    if (c == '[')
                    {
                        originState.Add(s);
                        break;
                    }
                }
            }

            return originState.ToArray();
        }

        #endregion setup
    }

    private class Stack
    {
        private List<char> _crates = new List<char>();

        public void AddToStack(char c)
        {
            _crates.Add(c);
        }

        public void RemoveTopFromStack()
        {
            int topIndex = _crates.Count - 1;
            _crates.RemoveAt(topIndex);
        }

        public char GetCrateAtTop()
        {
            return _crates[_crates.Count - 1];
        }

        public void ReverseOrderOfStack()
        {
            _crates.Reverse();
        }
    }

    private static void Main()
    {
        string[] input = File.ReadAllLines("input.txt");
        List<String[]> instructions = getInstructions(input);

        LoadingBay loadingBay = new LoadingBay(input);

        // loadingBay.CrateMover9000(instructions);
        // Console.WriteLine($"Part one: {loadingBay.GetCurrentTopLayer()}");

        loadingBay.CrateMover9001(instructions);
        Console.WriteLine($"Part two: {loadingBay.GetCurrentTopLayer()}");

    }

    private static List<String[]> getInstructions(string[] input)
    {
        List<string> instructions = new List<string>();

        foreach (string s in input)
        {
            if (s.Length == 0) { continue; }
            if (s[0] != 'm') { continue; }
            instructions.Add(s);
        }

        List<String[]> splitStrings = new List<String[]>();
        foreach (string s in instructions)
        {
            splitStrings.Add(s.Split(' '));
        }

        return splitStrings;
    }

}