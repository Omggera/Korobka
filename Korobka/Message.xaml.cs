//using Android.Media;
using System.Windows.Input;

namespace Korobka;

public partial class Message : ContentPage
{
    public Message()
    {
        InitializeComponent();
    }

    public void TapGestureRecognizer_Tapped(object sender, EventArgs e)
    {
        if (PhoneDialer.Default.IsSupported)
            PhoneDialer.Default.Open("+7-901-141-8686");
    }
}