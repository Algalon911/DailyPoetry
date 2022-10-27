namespace DailyPoetry.Views;

public partial class ResultPage : ContentPage
{
	public ResultPage(ResultPageViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}