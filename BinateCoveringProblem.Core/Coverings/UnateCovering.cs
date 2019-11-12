using BinateCoveringProblem.Core.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BinateCoveringProblem.Core.Coverings
{
    public class UnateCovering : CoveringBase
    {
        private readonly List<int> currentSolution = new List<int>();

        public UnateCovering(Dictionary<int, List<int>> inputSet)
        {
            this.inputSet = inputSet;
        }

        public override void Steps()
        {
            if (IsEssentialColumn())
            {
                OnEssentialColumn();
            }
            RemoveDominatedRow();
            RemoveDominatedColumn();
        }

        private bool IsEssentialColumn()
        {
            return inputSet.Any(s => s.Value.Count.Equals(1));
        }

        private void OnEssentialColumn()
        {
            var essentialColumn = inputSet.FirstOrDefault(s => s.Value.Count.Equals(1)).Value.FirstOrDefault();

            // remove all rows associated with the essential column
            RemoveAssociatedRows(essentialColumn);

            // add an essential column index to the solution
            UpdateSolution(essentialColumn);
        }

        private void RemoveAssociatedRows(int essentialColumn)
        {
            var associatedRows = new List<int>();

            foreach (var row in inputSet)
            {
                if (row.Value.Contains(essentialColumn))
                {
                    associatedRows.Add(row.Key);
                }
            }

            foreach (var row in associatedRows)
            {
                inputSet.Remove(row);
            }
        }

        private void RemoveDominatedRow()
        {
            foreach (var rowA in inputSet)
            {
                foreach (var rowB in inputSet)
                {
                    if (rowA.Key != rowB.Key && !rowA.Value.Except(rowB.Value).Any())
                    {
                        inputSet.Remove(rowB.Key);
                        return;
                    }
                }
            }
        }

        private void RemoveDominatedColumn()
        {
            var revInputSet = inputSet.Reverse();
            foreach (var rowA in revInputSet)
            {
                foreach (var rowB in revInputSet)
                {
                    if (rowA.Key != rowB.Key && !rowA.Value.Except(rowB.Value).Any())
                    {
                        revInputSet.Remove(rowA.Key);
                        inputSet = revInputSet.Reverse();
                        return;
                    }
                }
            }
        }

        private void UpdateSolution(int essentialColumn)
        {
            currentSolution.Add(essentialColumn);
        }

        public string PrintSolution()
        {
            var solution = new StringBuilder("{");
            foreach (var index in currentSolution)
            {
                solution.Append(string.Format(" x{0}", index));
            }
            solution.Append(" }");

            return solution.ToString();
        }
    }
}
