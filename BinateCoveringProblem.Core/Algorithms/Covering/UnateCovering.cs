using BinateCoveringProblem.Core.Algorithms.Reduction;
using BinateCoveringProblem.Core.Extensions;
using BinateCoveringProblem.Core.Maths;
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
        reduce:
            // source set has non-cyclic core
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

        fork:
            // source set has cyclic core
            // choose column against whick will occur the fork
            var chosen = new WeightsCalculator(source).ChooseColumn();

        option1: // chosen column is added to the solution: chosen = 1
            var source1 = source.ToDictionary();
            var solution1 = currentSolution.ToList();

            source1.RemoveAssociatedRows(chosen);
            solution1.Add(chosen);

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

        option0: // chosen column is removed from the source set: chosen = 0
            var source0 = source.ToDictionary();
            source0.RemoveColumn(chosen);

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
