using BinateCoveringProblem.Core.Extensions;
using BinateCoveringProblem.Core.Maths;
using System.Collections.Generic;
using System.Linq;

namespace BinateCoveringProblem.Core.Algorithms.Covering
{
    public abstract class CoveringBase : ICoveringAlgorithm
    {
        protected Dictionary<int, List<int>> source;
        protected List<int> currentSolution;
        protected List<int> boundarySolution;

        protected CoveringBase(Dictionary<int, List<int>> source, List<int> currentSolution = null, List<int> boundarySolution = null)
        {
            this.source = source;
            this.currentSolution = currentSolution ?? new List<int>();
            this.boundarySolution = boundarySolution ?? source.Reverse().Keys.ToList();

            Steps();
        }

        public abstract void Steps();

        public List<int> Result { get; set; }

        protected int UpperBound => boundarySolution.Count;

        protected int LowerBound()
        {
            // it's assumed that at least two
            var minimumLowerBound = 2;
            
            var maxCliqueLength = new MaximumClique(source).Length();
            var lowerBound = currentSolution.Count + maxCliqueLength;

            if (lowerBound > minimumLowerBound)
            {
                return lowerBound;
            }
            return minimumLowerBound;
        }
    }
}
