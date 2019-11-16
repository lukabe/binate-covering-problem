using System;
using System.Collections.Generic;
using System.Text;

namespace BinateCoveringProblem.Core.Extensions
{
    public static class ListExtensions
    {
        public static string Print(this List<int> solution)
        {
            if (solution is null)
            {
                throw new ArgumentNullException("Solution is null");
            }

            var output = new StringBuilder("{");
            foreach (var index in solution)
            {
                output.Append(string.Format(" x{0},", index));
            }
            output.Replace(",", " }", output.Length - 1, 1);

            return output.ToString();
        }
    }
}
