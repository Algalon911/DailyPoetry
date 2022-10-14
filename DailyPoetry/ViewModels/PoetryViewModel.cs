using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace DailyPoetry.ViewModels;

public partial class PoetryViewModel : ObservableObject
{
    [ObservableProperty]
    string text = string.Empty;

    [RelayCommand]
    void SetText()
    {
        Text = "123";
    }
}
