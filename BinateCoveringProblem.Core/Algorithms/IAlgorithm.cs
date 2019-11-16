namespace BinateCoveringProblem.Core.Algorithms
{
    public interface IAlgorithm<TResult>
    {
        void Steps();

        TResult Result { get; }
    }
}
