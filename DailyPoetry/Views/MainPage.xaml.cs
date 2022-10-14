namespace DailyPoetry.Views;

public partial class MainPage : ContentPage
{
	public MainPage(PoetryViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
    }
}