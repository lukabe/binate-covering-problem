using BinateCoveringProblem.Core.Extensions;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BinateCoveringProblem.Core.Algorithms.Reduction
{
    public class BinateReduction : ReductionBase
    {
        public BinateReduction(Dictionary<int, List<int>> source, List<int> currentSolution) : base(source, currentSolution) { }

        public override void Steps()
        {
            Dictionary<int, List<int>> tempSet;
            do
            {
                tempSet = source.ToDictionary();
                EssentialColumn();
                UnacceptableColumn();
                if (!source.IsTerminalCase())
                {
                    DominatedRow();
                    DominatedColumn();
                }
            }
            while (source.Any() && !source.Compare(tempSet));
        }

        protected override void EssentialColumn()
        {
            while (true)
            {
                if (source.IsEssentialColumn() && !source.IsTerminalCase())
                {
                    var essentialColumn = source.GetEssentialColumn();

                    // remove all rows associated with the essential column
                    source.RemoveAssociatedRows(essentialColumn);

                    // remove negation of essential column from all rows
                    source.RemoveColumn(-essentialColumn);

                    // add an essential column index to the solution
                    UpdateSolution(essentialColumn);

                    Log.Information($"Essential Column: {{{essentialColumn}}} {source.Print()}");
                    Log.Information($"Current Solution: {currentSolution.Print()}");
                    continue;
                }
                break;
            }
        }

        private void UnacceptableColumn()
        {
            while (true)
            {
                if (source.IsUnacceptableColumn() && !source.IsTerminalCase())
                {
                    var unacceptableColumn = source.GetUnacceptableColumn();

                    // remove all rows associated with the unacceptable column
                    source.RemoveAssociatedRows(unacceptableColumn);

                    // remove negation of unacceptable column from all rows
                    source.RemoveColumn(-unacceptableColumn);

                    Log.Information($"Unacceptable Column: {{{Math.Abs(unacceptableColumn)}}} {source.Print()}");
                    continue;
                }
                break;
            }
        }
    }
}
