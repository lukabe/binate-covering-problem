using System;
using System.Collections.Generic;

namespace BinateCoveringProblem.Core.Algorithms.Reduction
{
    public class BinateReduction : ReductionBase
    {
        public BinateReduction(Dictionary<int, List<int>> source, List<int> currentSolution) : base(source, currentSolution) { }

        public override void Steps()
        {
            throw new NotImplementedException();
        }

        protected override bool IsEssentialColumn => throw new NotImplementedException();

        protected override void EssentialColumn()
        {
            throw new NotImplementedException();
        }

        private bool IsUnacceptableColumn => throw new NotImplementedException();

        private void UnacceptableColumn()
        {
            throw new NotImplementedException();
        }

        protected override void DominatedColumn()
        {
            throw new NotImplementedException();
        }
    }
}
