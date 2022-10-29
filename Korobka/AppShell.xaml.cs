using Korobka.ViewModel;

namespace Korobka;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

		BindingContext = new AppShellViewModel();

        Routing.RegisterRoute(nameof(Message), typeof(Message));

    }

}
