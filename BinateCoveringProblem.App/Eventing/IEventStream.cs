using System;

namespace BinateCoveringProblem.App.Eventing
{
    public interface IEventStream
    {
        void Publish(object next);
        void Subscribe<T>(Action<T> onNext);
    }
}