using BinateCoveringProblem.Core.Coverings;
using System.Collections.Generic;

namespace BinateCoveringProblem.Console
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // non cyclic
            var inputSet1 = new Dictionary<int, List<int>>()
            {
                // { 1, 0, 1, 0, 0, 1 }
                // { 0, 1, 0, 1, 0, 0 }
                // { 1, 0, 1, 1, 1, 1 }
                // { 0, 1, 0, 0, 0, 0 }
                // { 1, 0, 0, 0, 1, 1 }

                [1] = new List<int>() { 1, 3, 6 },
                [2] = new List<int>() { 2, 4 },
                [3] = new List<int>() { 1, 3, 4, 5, 6 },
                [4] = new List<int>() { 2 },
                [5] = new List<int>() { 1, 5, 6 }
            };

            // cyclic
            var inputSet2 = new Dictionary<int, List<int>>()
            {
                [1] = new List<int>() { 1, 3 },
                [2] = new List<int>() { 1, 2, 4 },
                [3] = new List<int>() { 2, 3 },
                [4] = new List<int>() { 5 }
            };

            // non cyclic
            var inputSet3 = new Dictionary<int, List<int>>()
            {
                [1] = new List<int>() { 1, 2 },
                [2] = new List<int>() { 2, 3 },
                [3] = new List<int>() { 3, 4 },
                [4] = new List<int>() { 4 },
                [5] = new List<int>() { 3, 5 },
                [6] = new List<int>() { 4, 5 },
                [7] = new List<int>() { 6 }
            };

            // cyclic
            var inputSet4 = new Dictionary<int, List<int>>()
            {
                [1] = new List<int>() { 1, 2, 4 },
                [2] = new List<int>() { 1, 3, 5 },
                [3] = new List<int>() { 1, 4 },
                [4] = new List<int>() { 2, 3, 4 },
                [5] = new List<int>() { 2, 4, 5 },
                [6] = new List<int>() { 1, 3, 4 }
            };

            var unateCovering = new UnateCovering(inputSet3);
            unateCovering.Run();

            var solution = unateCovering.PrintSolution();
            System.Console.WriteLine("Solution: " + solution);
            System.Console.ReadLine();
        }
    }
}
