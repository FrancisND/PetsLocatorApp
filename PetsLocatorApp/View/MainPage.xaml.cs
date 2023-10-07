using PetsLocatorApp.ViewModel;

namespace PetsLocatorApp.View;

public partial class MainPage : ContentPage
{
	//int count = 0;

	public MainPage(PetsViewModel petsViewModel)
	{
		InitializeComponent();
		BindingContext = petsViewModel;

    }

	private void OnCounterClicked(object sender, EventArgs e)
	{
		//count++;

		//if (count == 1)
		//	CounterBtn.Text = $"Clicked {count} time";
		//else
		//	CounterBtn.Text = $"Clicked {count} times";

		//SemanticScreenReader.Announce("You pressed " + CounterBtn.Text + " times");
	}
}

