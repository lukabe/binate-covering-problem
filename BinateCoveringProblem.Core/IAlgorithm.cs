namespace BinateCoveringProblem.Core
{
    public interface IAlgorithm<TResult>
    {
        void Steps();

        TResult Result { get; }
    }
}
