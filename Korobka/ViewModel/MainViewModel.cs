using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Korobka.ViewModel
{
    public partial class MainViewModel: ObservableObject
    {
        public MainViewModel()
        {
            BarcodesList = new ObservableCollection<string>();
            
        }

        [ObservableProperty]
        ObservableCollection<string> barcodesList;

        [ObservableProperty]
        int barcodesListCount;

        [ObservableProperty]
        string barcode;

        [RelayCommand]
        void Add()
        {
            if (string.IsNullOrEmpty(Barcode))
                return;

            BarcodesList.Add(Barcode);

            Barcode = string.Empty;

            BarcodesListCount = BarcodesList.Count;
        }

        [RelayCommand]
        void Delete(string s)
        {
            if (BarcodesList.Contains(s))
            {
                BarcodesList.Remove(s);
            }

            BarcodesListCount = BarcodesList.Count;
        }
    }
}
