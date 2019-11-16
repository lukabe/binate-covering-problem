using System.Collections.Generic;
using System.Linq;

namespace BinateCoveringProblem.Core
{
    public class MaximumClique
    {
        private readonly Dictionary<int, List<int>> source;

        public MaximumClique(Dictionary<int, List<int>> source)
        {
            this.source = source;
        }

        public int Length() => Find().Count;

        /// <summary>
        /// Find the maximum clique for the source set
        /// </summary>
        /// <returns>Returns the maximum clique</returns>
        public List<int> Find()
        {
            var maxClique = new List<int>();

            foreach (var rowA in source)
            {
                var temp = new List<KeyValuePair<int, List<int>>>() { rowA };

                foreach (var rowB in source.Where(r => r.Key != rowA.Key))
                {
                    var isIntersect = false;
                    foreach (var row in temp)
                    {
                        if (!row.Value.Intersect(rowB.Value).Any())
                        {
                            continue;
                        }
                        isIntersect = true;
                        break;
                    }

                    if (!isIntersect)
                    {
                        temp.Add(rowB);
                    }
                }


                var clique = temp.Select(x => x.Key).ToList();
                maxClique = clique.Count > maxClique.Count ? clique : maxClique;
            }

            return maxClique;
        }
    }
}
