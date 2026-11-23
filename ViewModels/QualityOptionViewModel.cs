using CommunityToolkit.Mvvm.ComponentModel;
using SleepHelper.Models;

namespace SleepHelper.ViewModels;

/// Одна кнопка вибору якості сну у вікні «Додати запис».
public partial class QualityOptionViewModel : ObservableObject
{
	private static readonly Brush AccentBrush = new SolidColorBrush(Color.FromArgb("#FBBF24"));
	private static readonly Brush NoBrush = new SolidColorBrush(Colors.Transparent);
	private static readonly Color ActiveBg = Color.FromArgb("#1B2333");

	public int Index { get; }
	public string Emoji { get; }
	public string Label { get; }

	public QualityOptionViewModel(int index, Quality quality)
	{
		Index = index;
		Emoji = quality.Emoji;
		Label = quality.Label;
	}

	[ObservableProperty]
	[NotifyPropertyChangedFor(nameof(Opacity), nameof(FontSize), nameof(BorderBrush), nameof(Bg))]
	private bool isActive;

	public double Opacity => IsActive ? 1 : 0.55;
	public double FontSize => IsActive ? 28 : 22;
	public Brush BorderBrush => IsActive ? AccentBrush : NoBrush;
	public Color Bg => IsActive ? ActiveBg : Colors.Transparent;
}
