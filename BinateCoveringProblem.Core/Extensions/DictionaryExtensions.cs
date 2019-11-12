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
                throw new System.ArgumentNullException(nameof(source));
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

            revSet.OrderBy(x => x.Key);

            return revSet;
        }
    }
}
