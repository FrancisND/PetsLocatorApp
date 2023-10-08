using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetsLocatorApp.ViewModel
{
    [QueryProperty("Pet", "Pet")]
    public partial class PetDetailsViewModel : BaseViewModel
    {
        IMap map;

        public PetDetailsViewModel(IMap map)
        {
            this.map = map;
        }


        [ObservableProperty]
        Pet pet;


        [RelayCommand]
        async Task OpenMapAsync()
        {
            try
            {
                await map.OpenAsync(Pet.Latitude, Pet.Longitude,
                    new MapLaunchOptions
                    {
                        Name = Pet.Name,
                        NavigationMode = NavigationMode.None
                    });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                await Shell.Current.DisplayAlert("Error!",
                    $"Unable to open map: {ex.Message}", "OK");
            }
        }
    }
}
