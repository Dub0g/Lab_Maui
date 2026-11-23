using SleepHelper.ViewModels;

namespace SleepHelper.Views;

public partial class DiaryPage : ContentPage
{
	public DiaryPage(DiaryViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}
