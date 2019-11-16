using BinateCoveringProblem.Core.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace BinateCoveringProblem.Core.Algorithms.Reduction
{
    /// <summary>
    /// The reduction algorithm is sufficient to find the minimum coverage of the set with non-cyclic core
    /// </summary>
    public class ReductionAlgorithm : IAlgorithm<ReductionResult>
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
                tempSet = source;
                EssentialColumn();
                DominatedRow();
                DominatedColumn();
            }
            while (source.Any() && !source.Equals(tempSet));
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
                    RemoveAssociatedRows(essentialColumn);

                    // add an essential column index to the solution
                    UpdateSolution(essentialColumn);

                    continue;
                }
                break;
            }
        }

        private void RemoveAssociatedRows(int essentialColumn)
        {
            var associatedRows = new List<int>();

            foreach (var row in source)
            {
                if (row.Value.Contains(essentialColumn))
                {
                    associatedRows.Add(row.Key);
                }
            }

            foreach (var row in associatedRows)
            {
                source.Remove(row);
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
                    foreach (var rowB in revSource.Where(r => r.Key != rowA.Key && !rowA.Value.Except(r.Value).Any()))
                    {
                        revSource.Remove(rowA.Key);
                        source = revSource.Reverse();
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
