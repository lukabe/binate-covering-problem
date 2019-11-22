using BinateCoveringProblem.Core.Extensions;
using Serilog;
using System.Collections.Generic;
using System.Linq;

namespace BinateCoveringProblem.Core.Algorithms.Reduction
{
    /// <summary>
    /// The reduction algorithm is sufficient to find the minimum coverage of the set with non-cyclic core
    /// </summary>
    public abstract class ReductionBase : IReductionAlgorithm
    {
        protected Dictionary<int, List<int>> source;
        protected List<int> currentSolution;

        public ReductionResult Result => new ReductionResult(source, currentSolution);

        protected ReductionBase(Dictionary<int, List<int>> source, List<int> currentSolution)
        {
            this.source = source;
            this.currentSolution = currentSolution;

            Steps();
        }

        public abstract void Steps();

        protected abstract bool IsEssentialColumn { get; }

        protected abstract void EssentialColumn();

        protected void DominatedRow()
        {
        Start:
            while (true)
            {
                foreach (var rowA in source)
                {
                    foreach (var rowB in source.Where(r => r.Key != rowA.Key && !rowA.Value.Except(r.Value).Any()))
                    {
                        source.Remove(rowB.Key);
                        Log.Information("Dominated Row: " + source.Print());
                        goto Start;
                    }
                }
                break;
            }
        }

        protected abstract void DominatedColumn();

        protected void UpdateSolution(int essentialColumn)
        {
            currentSolution.Add(essentialColumn);
        }
    }
}
