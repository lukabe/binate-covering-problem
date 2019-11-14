using BinateCoveringProblem.Core.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace BinateCoveringProblem.Core
{
    /// <summary>
    /// The reduction algorithm is sufficient to find the minimum coverage of the set with non-cyclic core
    /// </summary>
    public class ReductionAlgorithm : IAlgorithm
    {
        private Dictionary<int, List<int>> source;
        private List<int> currentSolution;

        public Dictionary<int, List<int>> ReducedSource => source;

        public List<int> UpdatedSolution => currentSolution;

        public ReductionAlgorithm(Dictionary<int, List<int>> source, List<int> currentSolution)
        {
            this.source = source;
            this.currentSolution = currentSolution;
        }

        public void Run()
        {
            Dictionary<int, List<int>> tempSet;
            do
            {
                tempSet = source;
                Steps();
            }
            while (source.Any() && !source.Equals(tempSet));
        }

        public void Steps()
        {
            EssentialColumn();
            DominatedRow();
            DominatedColumn();
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
                    foreach (var rowB in source)
                    {
                        if (rowA.Key != rowB.Key && !rowA.Value.Except(rowB.Value).Any())
                        {
                            source.Remove(rowB.Key);
                            goto Start;
                        }
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
                    foreach (var rowB in revSource)
                    {
                        if (rowA.Key != rowB.Key && !rowA.Value.Except(rowB.Value).Any())
                        {
                            revSource.Remove(rowA.Key);
                            source = revSource.Reverse();
                            goto Start;
                        }
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
