using System.Collections.Generic;

namespace BinateCoveringProblem.Core
{
    public class ReductionResult
    {
        public Dictionary<int, List<int>> ReducedSource { get; }

        public List<int> UpdatedSolution { get; }

        public ReductionResult(Dictionary<int, List<int>> reducedSource, List<int> updatedSolution)
        {
            ReducedSource = reducedSource;
            UpdatedSolution = updatedSolution;
        }
    }
}
