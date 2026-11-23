using SleepHelper.Views;

namespace SleepHelper;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
		// Модальне вікно «Додати запис»
		Routing.RegisterRoute("AddEntry", typeof(AddEntryPage));
	}
}
