using System;
using System.Reactive.Linq;
using Reactive.Bindings;
using Reactive.Bindings.Notifiers;

namespace LTTimer.Model
{
    public class CountdownTimer
    {
        public ReactiveTimer Timer { get; set; } = new ReactiveTimer(TimeSpan.FromSeconds(1.0));
        public CountNotifier Secounds { get; set; } = new CountNotifier(60 * 5);

        public CountdownTimer()
        {
            Timer.Subscribe(l =>
            {
                if (Secounds.Count <= 1) Timer.Stop();
                Secounds.Decrement();
            });
        }

        public void Start(int limit)
        {
            if (limit <= 0) throw new ArgumentOutOfRangeException(nameof(limit));
            Secounds.Increment(limit);
            Timer.Start();
        }
    }
}
