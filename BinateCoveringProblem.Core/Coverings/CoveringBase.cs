using BinateCoveringProblem.Core.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace BinateCoveringProblem.Core.Coverings
{
    public abstract class CoveringBase : IAlgorithm
    {
        protected Dictionary<int, List<int>> inputSet;
        protected List<int> currentSolution;
        protected List<int> boundarySolution;

        protected int UpperBound => boundarySolution.Count;

        public CoveringBase(Dictionary<int, List<int>> inputSet, List<int> currentSolution = null)
        {
            this.inputSet = inputSet;
            this.currentSolution = currentSolution ?? new List<int>();
            this.boundarySolution = inputSet.Reverse().Keys.ToList();
        }

        public void Run()
        {
            Steps();
        }

        public abstract void Steps();
    }
}
