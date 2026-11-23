using SleepHelper.ViewModels;

namespace SleepHelper.Views;

public partial class AddEntryPage : ContentPage
{
	public AddEntryPage(AddEntryViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}

	private async void OnCloseClicked(object? sender, EventArgs e)
		=> await Shell.Current.GoToAsync("..");
}
