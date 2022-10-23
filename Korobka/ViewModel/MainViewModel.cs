using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Korobka;
using Korobka.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Korobka.ViewModel
{
    public partial class MainViewModel: ObservableObject
    {
        public MainViewModel()
        {
            City = new List<string>() { "Владимир", "Иваново", "Муром", "Ковров" };
            CitySelectedItem = "Владимир";

            BarcodesList = new ObservableCollection<string>();

            ValidNameEntry = false;
            IsReadOnlyNameEntry = false;

            SelectionWarehouse = "Коледино";
            SelectionGetMethod = "Привезет самостоятельно";
            SelectionPaymentMethod = "Наличные";

            ValidTelephoneEntry = false;
            IsReadOnlyTelephoneEntry = false;

            ValidCheckBox = false;
            ValidGlobal = false;

            AttentionLabel = false;

            CalculateFinalAmount();

            Mongo = new();

            DateNewOrderUTC = DateTime.Now;
        }

        [ObservableProperty]
        MongoDBConnect mongo;

        [ObservableProperty]
        long orderNum;

        [ObservableProperty]
        DateTimeOffset dateNewOrderUTC;

        //Блок выбора города
        [ObservableProperty]
        List<string> city;

        [ObservableProperty]
        string citySelectedItem;
        //Конец блока выбора города

        [ObservableProperty]
        string selectionWarehouse;

        [ObservableProperty]
        string nameEntry = "";

        [ObservableProperty]
        string telephoneEntry = "";

        //Валидация
        [ObservableProperty]
        bool validNameEntry;

        [ObservableProperty]
        bool isReadOnlyNameEntry;

        [ObservableProperty]
        bool isReadOnlyTelephoneEntry;

        [ObservableProperty]
        bool validTelephoneEntry;

        [ObservableProperty]
        bool validCheckBox;

        [ObservableProperty]
        bool validGlobal;
        //Конец валидации

        [ObservableProperty]
        string emailEntry;

        //Блок добавления ШК
        [ObservableProperty]
        ObservableCollection<string> barcodesList;

        [ObservableProperty]
        int barcodesListCount;

        [ObservableProperty]
        string barcode;
        //Конец блока добавления ШК

        //Блок расчета цены
        public int BoxPrice = 200;

        [ObservableProperty]
        string boxCount;

        [ObservableProperty]
        int? boxAmount;

        [ObservableProperty]
        int? deliveryPrice;

        [ObservableProperty]
        int? finalAmount;
        //Конец блока расчета цены

        //Блок метода полечения коробок
        [ObservableProperty]
        string adress;

        [ObservableProperty]
        string sam = "Привезет самостоятельно";

        [ObservableProperty]
        string selectionGetMethod;
        //Конец блока метода полечения коробок

        //Блок метода оплаты
        [ObservableProperty]
        string card = "Карта";

        [ObservableProperty]
        string selectionPaymentMethod;
        //Конец блока метода оплаты

        [ObservableProperty]
        bool attentionLabel;

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

            BoxCount = $"Коробки ({barcodesListCount} шт.)";

            if ((Adress == null) | (SelectionGetMethod == Sam) | (Adress == ""))
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

        [RelayCommand]
        void ValidationForm()
        {
            if((NameEntry != null) & (NameEntry != "")) ValidNameEntry = true;
            else ValidNameEntry = false;

            if ((TelephoneEntry != null) & (TelephoneEntry != "")) ValidTelephoneEntry = true;
            else ValidTelephoneEntry = false;

            if ((ValidNameEntry == true) & (ValidTelephoneEntry == true) & (ValidCheckBox == true))
            {
                ValidGlobal = true;
                IsReadOnlyNameEntry = true;
                IsReadOnlyTelephoneEntry = true;
                AttentionLabel = false;
            }
                
            else
            {
                ValidGlobal = false;
                IsReadOnlyNameEntry = false;
                IsReadOnlyTelephoneEntry = false;

                if ((ValidNameEntry == true) & (ValidTelephoneEntry == true) & (ValidCheckBox == false))
                    AttentionLabel = false;
                else AttentionLabel = true;

                ValidCheckBox = false;
            }  
        }

        [RelayCommand]
        void Create()
        {
            OrderNum = Mongo.AllDocCount() + 1;

            //Добавляем данные в БД
            BsonDocument doc = new BsonDocument
            {
            {"orderNum",$"{OrderNum}"},
            {"status","Новый"},
            {"dateOrder",$"{DateNewOrderUTC}"},
            {"city",$"{CitySelectedItem}"},
            {"dateDevivery",$"{SelectionWarehouse}"},
            {"clientName",$"{NameEntry}"},
            {"clientPhone",$"{TelephoneEntry}"},
            {"clientMail",$"{EmailEntry}"},
            {"getMethod",$"{SelectionGetMethod}"},
            {"clientAdress",$"{Adress}"},
            {"paymentMethod",$"{SelectionPaymentMethod}"},
            {"finalAmount",$"{FinalAmount}"},
            {"barcodes", new BsonArray(BarcodesList)}
            };

            mongo.AddDoc(doc).GetAwaiter();

            CitySelectedItem = "Владимир";
            SelectionGetMethod = "Привезет самостоятельно";
            SelectionPaymentMethod = "Наличные";
            NameEntry = "";
            TelephoneEntry = "";
            EmailEntry = "";
            Adress = "";
            ValidGlobal = false;
            IsReadOnlyNameEntry = false;
            IsReadOnlyTelephoneEntry = false;
            ValidCheckBox = false;
            AttentionLabel = false;
            BarcodesList.Clear();
            CalculateFinalAmount();

        }


        //[RelayCommand]
        //async void Alert()
        //{
        //    await DisplayAlert("Уведомление", "Пришло новое сообщение", "ОK");
        //}
    }
}
