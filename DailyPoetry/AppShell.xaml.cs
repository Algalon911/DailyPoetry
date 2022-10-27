using DailyPoetry.Views;

namespace DailyPoetry;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
		//增加飞出页
		Items.Add(
			new FlyoutItem
			{
				Title = nameof(ResultPage),
				Route = nameof(ResultPage),
				Items =
				{
					new ShellContent
					{
						ContentTemplate = new DataTemplate(typeof(ResultPage))
					}
				}
			}	
		);
	}
}
