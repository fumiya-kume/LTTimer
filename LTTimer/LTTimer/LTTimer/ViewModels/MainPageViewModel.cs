using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Linq;
using System.Reactive.Linq;
using LTTimer.Model;
using Microsoft.WindowsAzure.MobileServices;
using Reactive.Bindings;

namespace LTTimer.ViewModels
{
    public class MainPageViewModel : BindableBase, INavigationAware
    {
        private readonly MobileServiceClient _mobileServiceClient;
        private readonly CountdownTimer CountdownTimer = new CountdownTimer();
        public ReactiveProperty<string> DataKey { get; set; } = new ReactiveProperty<string>("");
        public ReactiveProperty<DateTime> LtTime { get; set; }
        public ReactiveProperty<bool> IsEditableKey { get; set; }
        public ReactiveProperty<int> TimerFontSize { get; set; } = new ReactiveProperty<int>(100);
        public ReactiveCommand StartTimer { get; set; }
        public ReactiveCommand RefreshTime { get; set; }

        public MainPageViewModel(MobileServiceClient mobileServiceClient)
        {
            _mobileServiceClient = mobileServiceClient;
            _mobileServiceClient = mobileServiceClient;
            StartTimer = DataKey
                .Select(s => !string.IsNullOrWhiteSpace(s))
                .ToReactiveCommand();

            StartTimer.Subscribe(async o =>
            {
                // Start LT Timer in Azure Mobile Apps
                await _mobileServiceClient
                    .GetTable<TimerTable>()
                    .InsertAsync(new TimerTable { Name = DataKey.Value });

                // Get LT Timer Value in Azure Mobile Apps
                var timerList = await _mobileServiceClient.GetTable<TimerTable>()
                        .Where(table => table.Name == DataKey.Value)
                        .ToListAsync();

                var ts = timerList.FirstOrDefault().CreatedAt.AddMinutes(5.0) - DateTime.Now;
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
