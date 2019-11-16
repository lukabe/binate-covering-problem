﻿using BinateCoveringProblem.Core.Algorithms.Covering;
using System.Collections.Generic;

namespace BinateCoveringProblem.Console
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // non-cyclic
            var source1 = new Dictionary<int, List<int>>()
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
            var source2 = new Dictionary<int, List<int>>()
            {
                [1] = new List<int>() { 1, 3 },
                [2] = new List<int>() { 1, 2, 4 },
                [3] = new List<int>() { 2, 3 },
                [4] = new List<int>() { 5 }
            };

            // non-cyclic
            var source3 = new Dictionary<int, List<int>>()
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
            var source4 = new Dictionary<int, List<int>>()
            {
                [1] = new List<int>() { 1, 2, 4 },
                [2] = new List<int>() { 1, 3, 5 },
                [3] = new List<int>() { 1, 4 },
                [4] = new List<int>() { 2, 3, 4 },
                [5] = new List<int>() { 2, 4, 5 },
                [6] = new List<int>() { 1, 3, 4 }
            };

            // cyclic
            var source5 = new Dictionary<int, List<int>>()
            {
                [1] = new List<int>() { 7, 8 },
                [2] = new List<int>() { 2, 6 },
                [3] = new List<int>() { 3, 4 },
                [4] = new List<int>() { 4, 5 },
                [5] = new List<int>() { 1, 4 },
                [6] = new List<int>() { 1, 7 },
                [7] = new List<int>() { 3, 6 },
                [8] = new List<int>() { 3, 5 },
                [9] = new List<int>() { 1, 8 }
            };

            System.Console.WriteLine("Solution: " + new UnateCovering(source1).PrintSolution());
            System.Console.WriteLine("Solution: " + new UnateCovering(source2).PrintSolution());
            System.Console.WriteLine("Solution: " + new UnateCovering(source3).PrintSolution());
            System.Console.WriteLine("Solution: " + new UnateCovering(source4).PrintSolution());
            System.Console.WriteLine("Solution: " + new UnateCovering(source5).PrintSolution());
            System.Console.ReadLine();
        }
    }
}
