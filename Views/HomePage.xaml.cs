using SleepHelper.ViewModels;

namespace SleepHelper.Views;

public partial class HomePage : ContentPage
{
	public HomePage(HomeViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}
