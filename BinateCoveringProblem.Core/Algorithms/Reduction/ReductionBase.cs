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
                        Log.Information($"Dominated Row: {{{rowB.Key}}} {source.Print()}");
                        goto Start;
                    }
                }
                break;
            }
        }

        protected void DominatedColumn()
        {
        Start:
            while (true)
            {
                var revSource = source.Reverse();
                foreach (var rowA in revSource)
                {
                    foreach (var rowB in revSource.Where(r => r.Key != rowA.Key && (!r.Value.Except(rowA.Value).Any() || r.Value.Except(rowA.Value).All(v => v < 0)) && !rowA.Value.Any(v => v < 0)))
                    {
                        revSource.Remove(rowB.Key);
                        source = revSource.Reverse();
                        Log.Information($"Dominated Column: {{{rowB.Key}}} {source.Print()}");
                        goto Start;
                    }
                }
                break;
            }
        }

        protected void UpdateSolution(int essentialColumn)
        {
            currentSolution.Add(essentialColumn);
        }
    }
}
