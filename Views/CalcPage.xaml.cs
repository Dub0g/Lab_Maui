using SleepHelper.ViewModels;

namespace SleepHelper.Views;

public partial class CalcPage : ContentPage
{
	public CalcPage(CalcViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}
