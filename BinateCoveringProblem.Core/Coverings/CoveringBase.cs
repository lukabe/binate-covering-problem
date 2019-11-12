using System.Collections.Generic;
using System.Linq;

namespace BinateCoveringProblem.Core.Coverings
{
    public abstract class CoveringBase : IAlgorithm
    {
        protected Dictionary<int, List<int>> inputSet;

        public void Run()
        {
            do
            {
                Steps();
            }
            while (inputSet.Any());
        }

        public abstract void Steps();
    }
}
