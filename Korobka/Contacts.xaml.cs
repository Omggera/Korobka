namespace Korobka;

public partial class Contacts : ContentPage
{
	public Contacts()
	{
		InitializeComponent();
	}

    public void TapGestureRecognizer_Tapped(object sender, EventArgs e)
    {
        if (PhoneDialer.Default.IsSupported)
            PhoneDialer.Default.Open("+7-901-141-8686");
    }

    private async void TapGestureRecognizer_Tapped_WhastApp(object sender, EventArgs e)
    {
        bool supportsUri = await Launcher.Default.CanOpenAsync("https://wa.me/79011418686");

        if (supportsUri)
            await Launcher.Default.OpenAsync("https://wa.me/79011418686");
    }

    private async void TapGestureRecognizer_Tapped_VK(object sender, EventArgs e)
    {
        bool supportsUri = await Launcher.Default.CanOpenAsync("https://vk.com/dostavkanamarket");

        if (supportsUri)
            await Launcher.Default.OpenAsync("https://vk.com/dostavkanamarket");
    }

    private async void TapGestureRecognizer_Tapped_Telegram(object sender, EventArgs e)
    {
        bool supportsUri = await Launcher.Default.CanOpenAsync("https://t.me/Dostavkanamarket");

        if (supportsUri)
            await Launcher.Default.OpenAsync("https://t.me/Dostavkanamarket");
    }

    private async void TapGestureRecognizer_Tapped_Viber(object sender, EventArgs e)
    {
        bool supportsUri = await Launcher.Default.CanOpenAsync("viber://chat?number=%2B79011418686");

        if (supportsUri)
            await Launcher.Default.OpenAsync("viber://chat?number=%2B79011418686");
    }

    private async void TapGestureRecognizer_Tapped_Mail(object sender, EventArgs e)
    {
        if (Email.Default.IsComposeSupported)
        {

            string subject = "Вопрос по отправке";
            string body = "";
            string[] recipients = new[] { "dostavkanamarket@yandex.ru" };

            var message = new EmailMessage
            {
                Subject = subject,
                Body = body,
                BodyFormat = EmailBodyFormat.PlainText,
                To = new List<string>(recipients)
            };

            await Email.Default.ComposeAsync(message);
        }
    }
}