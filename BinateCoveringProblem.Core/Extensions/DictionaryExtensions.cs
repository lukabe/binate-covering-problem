using System;
using System.Collections.Generic;
using System.Linq;

namespace BinateCoveringProblem.Core.Extensions
{
    public static class DictionaryExtensions
    {
        public static Dictionary<int, List<int>> Reverse(this Dictionary<int, List<int>> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            var revSet = new Dictionary<int, List<int>>();

            foreach (var row in source)
            {
                foreach (var column in row.Value)
                {
                    if (revSet.ContainsKey(column))
                    {
                        revSet.FirstOrDefault(c => c.Key == column).Value.Add(row.Key);
                        continue;
                    }
                    revSet.Add(column, new List<int>() { row.Key });
                }
            }

            return revSet.OrderBy(x => x.Key).ToDictionary(x => x.Key, x => x.Value);
        }

        public static bool Compare(this Dictionary<int, List<int>> source, Dictionary<int, List<int>> input)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.Count.Equals(input.Count) && !source.Except(input).Any();
        }
    }
}
