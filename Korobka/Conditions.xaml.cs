namespace Korobka;

public partial class Conditions : ContentPage
{
	public Conditions()
	{
		InitializeComponent();
	}

	private async void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
	{
        bool supportsUri = await Launcher.Default.CanOpenAsync("https://disk.yandex.ru/i/5cP2dW012nge1w");

        if (supportsUri)
            await Launcher.Default.OpenAsync("https://disk.yandex.ru/i/5cP2dW012nge1w");
    }

    private async void TapGestureRecognizer_Tapped_2(object sender, EventArgs e)
    {
        bool supportsUri = await Launcher.Default.CanOpenAsync("https://disk.yandex.ru/i/IrtwkJxpXqMdWg");

        if (supportsUri)
            await Launcher.Default.OpenAsync("https://disk.yandex.ru/i/IrtwkJxpXqMdWg");
    }

    private async void TapGestureRecognizer_Tapped_3(object sender, EventArgs e)
    {
        bool supportsUri = await Launcher.Default.CanOpenAsync("https://disk.yandex.ru/i/u_PKQ1s66cQvZQ");

        if (supportsUri)
            await Launcher.Default.OpenAsync("https://disk.yandex.ru/i/u_PKQ1s66cQvZQ");
    }

    private async void TapGestureRecognizer_Tapped_4(object sender, EventArgs e)
    {
        bool supportsUri = await Launcher.Default.CanOpenAsync("https://disk.yandex.ru/i/QTaZZR2aJr3H-w");

        if (supportsUri)
            await Launcher.Default.OpenAsync("https://disk.yandex.ru/i/QTaZZR2aJr3H-w");
    }
}