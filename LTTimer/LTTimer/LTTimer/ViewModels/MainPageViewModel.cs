using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Reactive.Linq;
using System.Threading.Tasks;
using LTTimer.Model;
using Reactive.Bindings;
using static LTTimer.Azure.Mobile_Apps.TimerTableClient;

namespace LTTimer.ViewModels
{
    public class MainPageViewModel : BindableBase, INavigationAware
    {
        private readonly CountdownTimer CountdownTimer = new CountdownTimer();
        public ReactiveProperty<string> DataKey { get; set; } = new ReactiveProperty<string>("");
        public ReactiveProperty<DateTime> LtTime { get; set; }
        public ReactiveProperty<bool> IsEditableKey { get; set; }
        public ReactiveProperty<int> TimerFontSize { get; set; } = new ReactiveProperty<int>(100);
        public ReactiveCommand StartTimerCommand { get; set; }
        public ReactiveCommand RefreshTimeCommand { get; set; }

        public MainPageViewModel()
        {
            StartTimerCommand = DataKey
                .Select(s => !string.IsNullOrWhiteSpace(s))
                .ToReactiveCommand();

            RefreshTimeCommand = DataKey
                .Select(s => !string.IsNullOrEmpty(s))
                .ToReactiveCommand();

            StartTimerCommand.Subscribe(async o =>
            {
                await StartTimer(DataKey.Value);

                await RefreshTimer();
            });

            RefreshTimeCommand.Subscribe(async o =>
            {
                await RefreshTimer();
            });

            LtTime = CountdownTimer.Secounds
                .Select(status => CountdownTimer.Secounds.Count)
                .Select(i => new DateTime().AddSeconds(i))
                .ToReactiveProperty();

        }

        private async Task RefreshTimer()
        {
            var timerData = await GetTimeFromTimerTable(DataKey.Value);
            var ts = timerData.AddMinutes(5f) - DateTime.Now;

            if (ts.Ticks < 0)
            {
                return;
            }

            CountdownTimer.Start((int) ts.TotalSeconds);
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {

        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
        }
    }
}
