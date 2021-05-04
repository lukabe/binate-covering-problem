using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace BinateCoveringProblem.App.Eventing
{
    public class EventStream : IEventStream
    {
        private readonly Subject<object> subject = new Subject<object>();

        public void Publish(object next)
        {
            subject.OnNext(next);
        }

        public void Subscribe<T>(Action<T> onNext)
        {
            subject.AsObservable()
                   .OfType<T>()
                   .Subscribe(onNext);
        }
    }
}