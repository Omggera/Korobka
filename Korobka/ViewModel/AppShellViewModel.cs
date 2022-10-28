using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Korobka.ViewModel
{
    public partial class AppShellViewModel: ObservableObject
    {
        public AppShellViewModel()
        {
            if (Application.Current.RequestedTheme == AppTheme.Light) IsToggled = true;
            if (Application.Current.RequestedTheme == AppTheme.Dark) IsToggled = false;

        }

        [ObservableProperty]
        bool isToggled;

        [RelayCommand]
        void Switch()
        {
            if (IsToggled == true) Application.Current.UserAppTheme = AppTheme.Light;
            if (IsToggled == false) Application.Current.UserAppTheme = AppTheme.Dark;
        }

    }
}
