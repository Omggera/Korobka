using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Korobka;

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

        //Блок расчета цены
        public int BoxPrice = 200;

        [ObservableProperty]
        int? boxAmount;

        [ObservableProperty]
        int? deliveryPrice;

        [ObservableProperty]
        int? finalAmount;

        [ObservableProperty]
        string adress;

        [ObservableProperty]
        string sam = "Привезет самостоятельно";

        [ObservableProperty]
        string vivoz = "Нужен вывоз";

        [ObservableProperty]
        string getMethod = "getMethod";

        [ObservableProperty]
        string selectionGetMethod = "Привезет самостоятельно";

        [ObservableProperty]
        string cash = "Наличные";

        [ObservableProperty]
        string card = "Карта";

        [ObservableProperty]
        string paymentMethod = "PaymentMethod";

        [ObservableProperty]
        string selectionPaymentMethod;


        [RelayCommand]
        void Add()
        {
            if (string.IsNullOrEmpty(Barcode))
                return;

            BarcodesList.Add(Barcode);

            Barcode = string.Empty;

            BarcodesListCount = BarcodesList.Count;

            CalculateFinalAmount();
        }

        [RelayCommand]
        void Delete(string s)
        {
            if (BarcodesList.Contains(s))
            {
                BarcodesList.Remove(s);
                CalculateFinalAmount();
            }

            BarcodesListCount = BarcodesList.Count;
        }

        [RelayCommand]
        void CalculateFinalAmount()
        {
            BoxAmount = BarcodesList.Count * BoxPrice;

            if ((Adress == null) | (GetMethod == Sam) | (Adress == ""))
            {
                DeliveryPrice = 0;
                Adress = null;
            }
            else DeliveryPrice = 400;

            if ((DeliveryPrice == null) | (DeliveryPrice == 0))
            {
                if (BoxAmount == 0)
                {
                    FinalAmount = 0;
                }
                else FinalAmount = BoxAmount + DeliveryPrice;
            }
            else FinalAmount = BoxAmount + DeliveryPrice;

            if (SelectionPaymentMethod == Card)
            {
                FinalAmount += (((BoxAmount + DeliveryPrice) * 2) / 100);
            }
        }
    }
}
