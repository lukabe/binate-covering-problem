using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BinateCoveringProblem.Core.Coverings
{
    public class UnateCovering : CoveringBase
    {
        public UnateCovering(Dictionary<int, List<int>> source, List<int> currentSolution = null) : base(source, currentSolution) { }

        public override void Steps()
        {
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

            // source set has cyclic core

            var choosen = new WeightsCalculator(source).ChooseColumn();

            // TODO: rozwidlenie
        }

        private void Reduce()
        {
            var reduction = new ReductionAlgorithm(source, currentSolution);
            source = reduction.Result.ReducedSource;
            currentSolution = reduction.Result.UpdatedSolution;
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
