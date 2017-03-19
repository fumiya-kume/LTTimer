using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using Reactive.Bindings;

namespace LTTimer.ViewModels
{
    public class SettingPageViewModel : BindableBase
    {
        public ReactiveProperty<bool> IsSoundWhenTimerFisnish { get; set; } = new ReactiveProperty<bool>(true);
        public SettingPageViewModel()
        {

        }
    }
}
