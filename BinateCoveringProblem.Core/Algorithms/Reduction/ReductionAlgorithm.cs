using BinateCoveringProblem.Core.Extensions;
using Serilog;
using System.Collections.Generic;
using System.Linq;

namespace BinateCoveringProblem.Core.Algorithms.Reduction
{
    /// <summary>
    /// The reduction algorithm is sufficient to find the minimum coverage of the set with non-cyclic core
    /// </summary>
    public class ReductionAlgorithm : IReductionAlgorithm
    {
        private Dictionary<int, List<int>> source;
        private List<int> currentSolution;

        public ReductionResult Result => new ReductionResult(source, currentSolution);

        public ReductionAlgorithm(Dictionary<int, List<int>> source, List<int> currentSolution)
        {
            this.source = source;
            this.currentSolution = currentSolution;

            Steps();
        }

        public void Steps()
        {
            Dictionary<int, List<int>> tempSet;
            do
            {
                tempSet = source.ToDictionary();
                EssentialColumn();
                DominatedRow();
                DominatedColumn();
            }
            while (source.Any() && !source.Compare(tempSet));
        }

        private bool IsEssentialColumn => source.Any(s => s.Value.Count.Equals(1));

        private void EssentialColumn()
        {
            while (true)
            {
                if (IsEssentialColumn)
                {
                    var essentialColumn = source.FirstOrDefault(s => s.Value.Count.Equals(1)).Value.FirstOrDefault();

                    // remove all rows associated with the essential column
                    source.RemoveAssociatedRows(essentialColumn);

                    // add an essential column index to the solution
                    UpdateSolution(essentialColumn);

                    Log.Information("Essential Column: " + source.Print());
                    Log.Information("Current Solution: " + currentSolution.Print());
                    continue;
                }
                break;
            }
        }

        private void DominatedRow()
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

        private void DominatedColumn()
        {
        Start:
            while (true)
            {
                var revSource = source.Reverse();
                foreach (var rowA in revSource)
                {
                    foreach (var rowB in revSource.Where(r => r.Key != rowA.Key && !r.Value.Except(rowA.Value).Any()))
                    {
                        revSource.Remove(rowB.Key);
                        source = revSource.Reverse();
                        Log.Information("Dominated Column: " + source.Print());
                        goto Start;
                    }
                }
                break;
            }
        }

        private void UpdateSolution(int essentialColumn)
        {
            currentSolution.Add(essentialColumn);
        }
    }
}
