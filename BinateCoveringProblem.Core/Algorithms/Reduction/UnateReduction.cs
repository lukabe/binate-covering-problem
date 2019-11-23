using BinateCoveringProblem.Core.Extensions;
using Serilog;
using System.Collections.Generic;
using System.Linq;

namespace BinateCoveringProblem.Core.Algorithms.Reduction
{
    public class UnateReduction : ReductionBase
    {
        public UnateReduction(Dictionary<int, List<int>> source, List<int> currentSolution) : base(source, currentSolution) { }

        public override void Steps()
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

        protected override void EssentialColumn()
        {
            while (true)
            {
                if (source.IsEssentialColumn())
                {
                    var essentialColumn = source.GetEssentialColumn();

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

        protected override void DominatedColumn()
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
    }
}
