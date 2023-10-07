using PetsLocatorApp.ViewModel;

namespace PetsLocatorApp.View;

public partial class DetailsPage : ContentPage
{
	public DetailsPage(PetDetailsViewModel petDetailsViewModel)
	{
		InitializeComponent();
		BindingContext = petDetailsViewModel;
	}

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
    }
}