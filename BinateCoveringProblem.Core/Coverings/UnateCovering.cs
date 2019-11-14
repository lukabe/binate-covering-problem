using BinateCoveringProblem.Core.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BinateCoveringProblem.Core.Coverings
{
    public class UnateCovering : CoveringBase
    {
        public UnateCovering(Dictionary<int, List<int>> inputSet, List<int> currentSolution = null) : base(inputSet, currentSolution) { }

        public override void Steps()
        {
            Reduce();
        }

        /// <summary>
        /// Reduce algorithm is sufficient for the set with non-cyclic core
        /// </summary>
        private void Reduce()
        {
            Dictionary<int, List<int>> tempSet;
            do
            {
                tempSet = inputSet;
                EssentialColumn();
                DominatedRow();
                DominatedColumn();
            }
            while (inputSet.Any() && !inputSet.Equals(tempSet));
        }

        private bool IsEssentialColumn => inputSet.Any(s => s.Value.Count.Equals(1));

        private void EssentialColumn()
        {
            while (true)
            {
                if (IsEssentialColumn)
                {
                    var essentialColumn = inputSet.FirstOrDefault(s => s.Value.Count.Equals(1)).Value.FirstOrDefault();

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

        private void DominatedRow()
        {
        Start: 
            while (true)
            {
                foreach (var rowA in inputSet)
                {
                    foreach (var rowB in inputSet)
                    {
                        if (rowA.Key != rowB.Key && !rowA.Value.Except(rowB.Value).Any())
                        {
                            inputSet.Remove(rowB.Key);
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
                var revInputSet = inputSet.Reverse();
                foreach (var rowA in revInputSet)
                {
                    foreach (var rowB in revInputSet)
                    {
                        if (rowA.Key != rowB.Key && !rowA.Value.Except(rowB.Value).Any())
                        {
                            revInputSet.Remove(rowA.Key);
                            inputSet = revInputSet.Reverse();
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

        /// <summary>
        /// Returns column with the greatest weight
        /// </summary>
        /// <returns></returns>
        public int CalculateWeights()
        {
            var weights = new Dictionary<int, double>();
            var columns = inputSet.Reverse().Keys.ToList();

            foreach (var column in columns)
            {
                double weight = 0;

                foreach (var row in inputSet)
                {
                    if (row.Value.Contains(column))
                    {
                        weight += (double) 1 / row.Value.Count();
                    }
                }

                weights.Add(column, weight);
            }

            return weights.Where(x => x.Value == weights.Values.Max()).FirstOrDefault().Key;
        }
    }
}
