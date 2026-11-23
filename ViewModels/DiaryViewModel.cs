using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SleepHelper.Models;
using SleepHelper.Services;

namespace SleepHelper.ViewModels;

public partial class DiaryViewModel : ObservableObject
{
	private readonly SleepStore _store;

	public DiaryViewModel(SleepStore store)
	{
		_store = store;
		_store.Entries.CollectionChanged += (_, _) => OnPropertyChanged(nameof(IsEmpty));
	}

	public ObservableCollection<SleepEntry> Entries => _store.Entries;
	public bool IsEmpty => _store.Entries.Count == 0;

	[RelayCommand]
	private Task AddEntry() => Shell.Current.GoToAsync("AddEntry");
}
