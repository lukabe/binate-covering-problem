using BinateCoveringProblem.Core.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BinateCoveringProblem.Core.Coverings
{
    public class UnateCovering : CoveringBase
    {
        public UnateCovering(Dictionary<int, List<int>> source, List<int> currentSolution = null) : base(source, currentSolution) { }

        public override void Steps()
        {
            Reduce();

            if (!source.Any())
            {
                return;
            }

            // is cyclic core
        }

        private void Reduce()
        {
            var reduction = new ReductionAlgorithm(source, currentSolution);
            reduction.Run();
            source = reduction.ReducedSource;
            currentSolution = reduction.UpdatedSolution;
        }

        public string PrintSolution()
        {
            var solution = new StringBuilder("{");
            foreach (var index in currentSolution)
            {
                solution.Append(string.Format(" x{0}", index));
            }
            solution.Append(" }");

            return solution.ToString();
        }

        /// <summary>
        /// Returns column with the greatest weight
        /// </summary>
        /// <returns></returns>
        public int CalculateWeights()
        {
            var weights = new Dictionary<int, double>();
            var columns = source.Reverse().Keys.ToList();

            foreach (var column in columns)
            {
                double weight = 0;

                foreach (var row in source)
                {
                    if (row.Value.Contains(column))
                    {
                        weight += (double) 1 / row.Value.Count();
                    }
                }

                weights.Add(column, weight);
            }

            return weights.Where(x => x.Value == weights.Values.Max()).FirstOrDefault().Key;
        }
    }
}
