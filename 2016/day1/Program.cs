using System;
using System.IO;
using System.Linq;

namespace day1
{
    class Program
    {
        private static string[] s_directions = { "North", "East", "South", "West" };

        static void Main()
        {
            string _input = File.ReadAllLines("input.txt")[0];
            string[] _instructions = _input.Split(',');

            for (int i = 0; i < _instructions.Length; i++)
            {
                _instructions[i] = _instructions[i].Trim();
            }

            int[] _finalPosition = GetNewPositionFromInstructions(_instructions);
            int _distanceOne = GetDistanceFromOrigin(_finalPosition);

            Console.WriteLine($"Part one: {_distanceOne}");
        }

        private static int[] GetNewPositionFromInstructions(string[] instructions)
        {
            int _x = 0;
            int _y = 0;
            string _currentDirection = s_directions[0];

            List<int[]> _visitedPositions = new List<int[]>();

            foreach (string _step in instructions)
            {
                string _turn = _step.Substring(0, 1);
                int _distance = Int32.Parse(_step.Remove(0, 1));

                _currentDirection = GetNewDirection(_currentDirection, _turn);

                switch (_currentDirection)
                {
                    case "North":
                        _y += _distance;
                        break;

                    case "East":
                        _x += _distance;
                        break;

                    case "South":
                        _y -= _distance;
                        break;

                    case "West":
                        _x -= _distance;
                        break;

                    default:
                        Console.WriteLine($"No case for {_currentDirection}!");
                        break;
                }

                _visitedPositions.Add(new int[] { _x, _y });
                FindIntersect(_visitedPositions);
            }

            return new int[] { _x, _y };
        }

        private static string GetNewDirection(string oldDirection, string turn)
        {
            int _oldIndex = Array.IndexOf(s_directions, oldDirection);
            int _newIndex = turn == "R" ? _oldIndex + 1 : _oldIndex - 1;

            if (_newIndex == s_directions.Length) { _newIndex = 0; }
            if (_newIndex == -1) { _newIndex = s_directions.Length - 1; }

            return s_directions[_newIndex];
        }

        private static int GetDistanceFromOrigin(int[] position)
        {
            int _dX = (int)MathF.Abs(position[0]);
            int _dY = (int)MathF.Abs(position[1]);

            return _dX + _dY;
        }

        private static void FindIntersect(List<int[]> visitedPositions)
        {
            if (visitedPositions.Count == 0) { return; }


        }
    }
}