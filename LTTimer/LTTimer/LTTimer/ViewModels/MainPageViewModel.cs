using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using LTTimer.Azure.Model;
using Prism.Services;
using Reactive.Bindings;

namespace LTTimer.ViewModels
{
    public class MainPageViewModel : BindableBase, INavigationAware
    {
        private readonly ITimerTableClient _timerTableClient;
        private readonly IPageDialogService _pageDialogService;
        public ReactiveProperty<string> DataKey { get; set; } = new ReactiveProperty<string>("");
        public ReactiveProperty<DateTime> LtTime { get; set; } = new ReactiveProperty<DateTime>();
        public ReactiveCommand StartTimer { get; set; }
        public ReactiveCommand RefreshTime { get; set; }

        public MainPageViewModel(ITimerTableClient timerTableClient, IPageDialogService pageDialogService)
        {
            _timerTableClient = timerTableClient;
            _pageDialogService = pageDialogService;
            StartTimer = DataKey
                .Where(s => !string.IsNullOrWhiteSpace(s))
                .Select(s => true)
                .ToReactiveCommand();

            StartTimer.Subscribe(o =>
            {
                KeyCheck();
                timerTableClient.StartTimer(DataKey.Value);
            });

            RefreshTime = DataKey
                .Select(s => !string.IsNullOrWhiteSpace(s))
                .ToReactiveCommand();
            
            RefreshTime.Subscribe(async o =>
            {
                var timerresult = await _timerTableClient.GetTimer(DataKey.Value);
                var ts = timerresult.CreatedAt.AddMinutes(5.0) - DateTime.Now;
                LtTime.Value = new DateTime().Add(ts);
            });

        }

        private void KeyCheck()
        {
            if (DataKey.Value == "") _pageDialogService.DisplayAlertAsync("Alert", "Key Not found", "OK");
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {

        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
        }
    }
}
