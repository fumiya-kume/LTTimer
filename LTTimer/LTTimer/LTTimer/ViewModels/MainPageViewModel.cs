using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Linq;
using LTTimer.Azure.Model;
using LTTimer.Model;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;

namespace LTTimer.ViewModels
{
    public class MainPageViewModel : BindableBase, INavigationAware
    {
        private readonly ITimerTableClient _timerTableClient;
        private readonly CountdownTimer CountdownTimer = new CountdownTimer();
        public ReactiveProperty<string> DataKey { get; set; } = new ReactiveProperty<string>("");
        public ReactiveProperty<DateTime> LtTime { get; set; }
        public ReactiveProperty<bool> IsEditableKey{ get; set; }
        public ReactiveCommand StartTimer { get; set; }
        public ReactiveCommand RefreshTime { get; set; }

        public MainPageViewModel(ITimerTableClient timerTableClient)
        {
            _timerTableClient = timerTableClient;
            StartTimer = DataKey
                .Select(s => !string.IsNullOrWhiteSpace(s))
                .ToReactiveCommand();

            StartTimer.Subscribe(async o =>
            {
                await timerTableClient.StartTimer(DataKey.Value);
                var timerresult = await _timerTableClient.GetTimer(DataKey.Value);
                var ts = timerresult.CreatedAt.AddMinutes(5.0) - DateTime.Now;
                CountdownTimer.Start((int)ts.TotalSeconds);
            });

            LtTime = CountdownTimer.Secounds
                .Select(status => CountdownTimer.Secounds.Count)
                .Select(i => new DateTime().AddSeconds(i))
                .ToReactiveProperty();
            
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {

        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
        }
    }
}
