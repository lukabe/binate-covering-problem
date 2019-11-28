using BinateCoveringProblem.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BinateCoveringProblem.Core.Maths
{
    public class WeightsCalculator
    {
        private readonly Dictionary<int, List<int>> source;

        public WeightsCalculator(Dictionary<int, List<int>> source)
        {
            this.source = source;
        }

        /// <summary>
        /// Returns column with the greatest weight
        /// </summary>
        public int ChooseColumn()
        {
            var weights = CalculateWeights();
            return weights.Where(x => x.Value == weights.Values.Max()).FirstOrDefault().Key;
        }

        /// <summary>
        /// Calculate weight for each column
        /// </summary>
        /// <returns>Returns columns indexes with weights</returns>
        private Dictionary<int, double> CalculateWeights()
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
                        weight += (double)1 / row.Value.Count();
                    }
                }

                weights.Add(column, weight);
            }

            return weights;
        }
    }
}
