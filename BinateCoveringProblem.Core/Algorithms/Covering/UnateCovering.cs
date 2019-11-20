using BinateCoveringProblem.Core.Algorithms.Reduction;
using BinateCoveringProblem.Core.Extensions;
using BinateCoveringProblem.Core.Maths;
using Serilog;
using System.Collections.Generic;
using System.Linq;

namespace BinateCoveringProblem.Core.Algorithms.Covering
{
    public class UnateCovering : CoveringBase
    {
        public UnateCovering(Dictionary<int, List<int>> source, List<int> currentSolution = null, List<int> boundarySolution = null) 
            : base(source, currentSolution, boundarySolution) { }

        public override void Steps()
        {
            /// <summary>
            /// Reduction:
            /// </summary>
            // source set has non-cyclic core
            Log.Information("Reduction");
            Reduce();

            if (!source.Any())
            {
                // source set was completely reduced
                Result = currentSolution;
                return;
            }
            
            var lowerBound = LowerBound();
            if (lowerBound >= UpperBound)
            {
                // there is currently no better solution
                Result = boundarySolution;
                return;
            }

            /// <summary>
            /// Fork: choose column against whick will occur the fork
            /// </summary>
            // source set has cyclic core
            var chosen = new WeightsCalculator(source).ChooseColumn();
            Log.Information("Is occur the fork against column " + chosen);
            
            /// <summary>
            /// Option 1: chosen column is added to the solution; chosen = 1
            /// </summary>
            Log.Information(string.Format("Option 1: column {0} is added to the solution", chosen));

            var source1 = source.ToDictionary();
            source1.RemoveAssociatedRows(chosen);
            Log.Information("Select Chosen Column: " + source1.Print());

            var solution1 = currentSolution.ToList();
            solution1.Add(chosen);
            Log.Information("Current Solution: " + solution1.Print());

            var result1 = new UnateCovering(source1, solution1, boundarySolution).Result;

            if (result1.Count < UpperBound)
            {
                boundarySolution = result1;
                if (UpperBound == lowerBound)
                {
                    Result = boundarySolution;
                    return;
                }
            }

            /// <summary>
            /// Option 0: chosen column is removed from the source set; chosen = 0
            /// </summary>
            Log.Information(string.Format("Option 0: column {0} is removed from the source set", chosen));

            var source0 = source.ToDictionary();
            source0.RemoveColumn(chosen);
            Log.Information("Remove Chosen Column: " + source0.Print());

            var result0 = new UnateCovering(source0, currentSolution, boundarySolution).Result;

            if (result0.Count < UpperBound)
            {
                boundarySolution = result0;
            }

            Result = boundarySolution;
            return;
        }

        private void Reduce()
        {
            var reduction = new ReductionAlgorithm(source, currentSolution);
            source = reduction.Result.ReducedSource;
            currentSolution = reduction.Result.UpdatedSolution;
        }
    }
}
