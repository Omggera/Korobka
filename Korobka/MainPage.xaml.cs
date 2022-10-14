using Android.Test.Suitebuilder;
using Korobka.ViewModel;
using Microsoft.Maui.Controls.Internals;
using Microsoft.VisualBasic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Korobka;

public partial class MainPage : ContentPage
{
    DateTime date = DateTime.Now;
    DateTime dateWednesday = new DateTime();
    DateTime dateMonday = new DateTime();

    

    public MainPage(MainViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;

        // Дата отправки
        DateDelivery();
        koled.Content = $"Коледино, {dateMonday.ToString("ddd dd MMMM yyyy")}";
        stal.Content = $"Электросталь, {dateWednesday.ToString("ddd dd MMMM yyyy")}";
    }

    public void DateDelivery()
    {
        if ((date.ToString("dddd")) == "понедельник")
        {
            dateMonday = date;
            dateWednesday = date.AddDays(2);
        }
        else if ((date.ToString("dddd")) == "вторник")
        {
            dateMonday = date.AddDays(6);
            dateWednesday = date.AddDays(1);
        }
        else if ((date.ToString("dddd")) == "среда")
        {
            dateMonday = date.AddDays(5);
            dateWednesday = date;
        }
        else if ((date.ToString("dddd")) == "четверг")
        {
            dateMonday = date.AddDays(4);
            dateWednesday = date.AddDays(6);
        }
        else if ((date.ToString("dddd")) == "пятница")
        {
            dateMonday = date.AddDays(3);
            dateWednesday = date.AddDays(5);
        }
        else if ((date.ToString("dddd")) == "суббота")
        {
            dateMonday = date.AddDays(2);
            dateWednesday = date.AddDays(4);
        }
        else if ((date.ToString("dddd")) == "воскресенье")
        {
            dateMonday = date.AddDays(1);
            dateWednesday = date.AddDays(3);
        }
    }

	private void City_SelectedIndexChanged(object sender, EventArgs e)
	{

	}

	private void Warehouse_CheckedChanged(object sender, CheckedChangedEventArgs e)
	{
        RadioButton selectedWarehouse = ((RadioButton)sender);

    }

    private void GetMethod_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        RadioButton selectedGetMethod = ((RadioButton)sender);
        if (vivoz.IsChecked)
        {
            adress.IsVisible = true;
            adressFrame.IsVisible = true;
        }
        else
        {
            adress.IsVisible = false;
            adressFrame.IsVisible = false;
            adressText.Text = null;
        }
    }

    private void PaymentMethod_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {

    }
}

