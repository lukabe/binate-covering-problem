using BinateCoveringProblem.Core.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace BinateCoveringProblem.Core.Coverings
{
    public abstract class CoveringBase : IAlgorithm
    {
        protected Dictionary<int, List<int>> source;
        protected List<int> currentSolution;
        protected List<int> boundarySolution;

        protected int UpperBound => boundarySolution.Count;

        public CoveringBase(Dictionary<int, List<int>> source, List<int> currentSolution = null)
        {
            this.source = source;
            this.currentSolution = currentSolution ?? new List<int>();
            this.boundarySolution = source.Reverse().Keys.ToList();
        }

        public void Run()
        {
            Steps();
        }

        public abstract void Steps();
    }
}
