using BinateCoveringProblem.Core.Algorithms.Covering;
using BinateCoveringProblem.Core.Extensions;
using Serilog;
using System.Collections.Generic;

namespace BinateCoveringProblem.Console
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration().WriteTo.Console().CreateLogger();

            // non-cyclic
            var unate1 = new Dictionary<int, List<int>>()
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
            var unate2 = new Dictionary<int, List<int>>()
            {
                [1] = new List<int>() { 1, 3 },
                [2] = new List<int>() { 1, 2, 4 },
                [3] = new List<int>() { 2, 3 },
                [4] = new List<int>() { 5 }
            };

            // non-cyclic
            var unate3 = new Dictionary<int, List<int>>()
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
            var unate4 = new Dictionary<int, List<int>>()
            {
                [1] = new List<int>() { 1, 2, 4 },
                [2] = new List<int>() { 1, 3, 5 },
                [3] = new List<int>() { 1, 4 },
                [4] = new List<int>() { 2, 3, 4 },
                [5] = new List<int>() { 2, 4, 5 },
                [6] = new List<int>() { 1, 3, 4 }
            };

            // cyclic
            var unate5 = new Dictionary<int, List<int>>()
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

            // binate
            var binate1 = new Dictionary<int, List<int>>()
            {
                [1] = new List<int>() { 1, 2 },
                [2] = new List<int>() { 2 },
                [3] = new List<int>() { 3 },
                [4] = new List<int>() { 3, -4 },
                [5] = new List<int>() { -1, 4 }
            };

            var binate2 = new Dictionary<int, List<int>>()
            {
                [1] = new List<int>() { 1, -2 },
                [2] = new List<int>() { -1, 2 },
                [3] = new List<int>() { -2 },
                [4] = new List<int>() { -3, 4 }
            };

            // ***
            var binate3 = new Dictionary<int, List<int>>()
            {
                [1] = new List<int>() { 1, 3 },
                [2] = new List<int>() { 2, 4, 6 },
                [3] = new List<int>() { -3, 4, 5 },
                [4] = new List<int>() { -6 },
                [5] = new List<int>() { -1, -6 },
                [6] = new List<int>() { 3, -4, 5 }
            };

            var binate4 = new Dictionary<int, List<int>>()
            {
                [1] = new List<int>() { -1, 2, -3, 4, 5, -6 },
                [2] = new List<int>() { -1, -3, 4, 5, -6 },
                [3] = new List<int>() { 1, -2, 3, 5 },
                [4] = new List<int>() { 2, -3, -4, -5, 6 },
                [5] = new List<int>() { 1, 2, 3 }
            };

            var binate5 = new Dictionary<int, List<int>>()
            {
                [1] = new List<int>() { -1, 2, -4 },
                [2] = new List<int>() { 2, 3 },
                [3] = new List<int>() { -1, 2, 4 },
                [4] = new List<int>() { 1 },
                [5] = new List<int>() { 2, 4 },
                [6] = new List<int>() { 4 }
            };

            var binate6 = new Dictionary<int, List<int>>()
            {
                [1] = new List<int>() { 1, 2, 3, 4 },
                [2] = new List<int>() { 1, 3 },
                [3] = new List<int>() { 1, -2, 3 },
                [4] = new List<int>() { 1, 3, -4 }
            };

            var binate7 = new Dictionary<int, List<int>>()
            {
                [1] = new List<int>() { 1, 3 },
                [2] = new List<int>() { 1, 2, 3, 4 },
                [3] = new List<int>() { -4 },
                [4] = new List<int>() { -2 },
                [5] = new List<int>() { 1, 2 },
                [6] = new List<int>() { 1, 3 }
            };

            // terminal - rozwiazanie niemozliwe
            var binate8 = new Dictionary<int, List<int>>()
            {
                [1] = new List<int>() { 1, 3 },
                [2] = new List<int>() { 1, 2, 3, 4 },
                [3] = new List<int>() { -4 },
                [4] = new List<int>() { -2 },
                [5] = new List<int>() { 2, 4 },
                [6] = new List<int>() { 1, 3 }
            };

            // terminal
            var binate9 = new Dictionary<int, List<int>>()
            {
                [1] = new List<int>() { 1, 3 },
                [2] = new List<int>() { 1, 2, 3, 4 },
                [3] = new List<int>() { -4 },
                [4] = new List<int>() { -2 },
                [5] = new List<int>() { 2 },
                [6] = new List<int>() { 1, 3 }
            };

            Log.Information($"Source: {unate1.Print()}");
            Log.Information($"Solution: {new UnateCovering(unate1).Result.Print()}");
            System.Console.WriteLine();

            Log.Information($"Source: {unate2.Print()}");
            Log.Information($"Solution: {new UnateCovering(unate2).Result.Print()}");
            System.Console.WriteLine();

            Log.Information($"Source: {unate3.Print()}");
            Log.Information($"Solution: {new UnateCovering(unate3).Result.Print()}");
            System.Console.WriteLine();

            Log.Information($"Source: {unate4.Print()}");
            Log.Information($"Solution: {new UnateCovering(unate4).Result.Print()}");
            System.Console.WriteLine();

            Log.Information($"Source: {unate5.Print()}");
            Log.Information($"Solution: {new UnateCovering(unate5).Result.Print()}");
            System.Console.WriteLine();

            Log.Information($"Source: {binate1.Print()}");
            Log.Information($"Solution: {new BinateCovering(binate1).Result.Print()}");
            System.Console.WriteLine();

            Log.Information($"Source: {binate2.Print()}");
            Log.Information($"Solution: {new BinateCovering(binate2).Result.Print()}");
            System.Console.WriteLine();

            Log.Information($"Source: {binate3.Print()}");
            Log.Information($"Solution: {new BinateCovering(binate3).Result.Print()}");
            System.Console.WriteLine();

            Log.Information($"Source: {binate4.Print()}");
            Log.Information($"Solution: {new BinateCovering(binate4).Result.Print()}");
            System.Console.WriteLine();

            Log.Information($"Source: {binate5.Print()}");
            Log.Information($"Solution: {new BinateCovering(binate5).Result.Print()}");
            System.Console.WriteLine();

            Log.Information($"Source: {binate6.Print()}");
            Log.Information($"Solution: {new BinateCovering(binate6).Result.Print()}");
            System.Console.WriteLine();

            Log.Information($"Source: {binate7.Print()}");
            Log.Information($"Solution: {new BinateCovering(binate7).Result.Print()}");
            System.Console.WriteLine();

            Log.Information($"Source: {binate8.Print()}");
            Log.Information($"Solution: {new BinateCovering(binate8).Result.Print()}");
            System.Console.WriteLine();

            Log.Information($"Source: {binate9.Print()}");
            Log.Information($"Solution: {new BinateCovering(binate9).Result.Print()}");
            System.Console.WriteLine();

            System.Console.ReadLine();
        }
    }
}
