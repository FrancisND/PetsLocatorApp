using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetsLocatorApp.Services;
using PetsLocatorApp.View;

namespace PetsLocatorApp.ViewModel
{
    public partial class PetsViewModel : BaseViewModel
    {
        IPetService petService;

        IConnectivity connectivity;
        IGeolocation geolocation;

        public ObservableCollection<Pet> Pets { get; } = new();


        public PetsViewModel(IPetService _petService, IConnectivity connectivity, IGeolocation geolocation)
        {
            Title = "Pet Locator";
            petService = _petService;
            this.connectivity = connectivity;
            this.geolocation = geolocation;
        }

        [ObservableProperty]
        bool isRefreshing;

        [RelayCommand]
        async Task GetPetsAsync()
        {
            if (IsBusy)
                return;

            try
            {
                if (IsNetworkConnectivity())
                {
                    await Shell.Current.DisplayAlert("Internet issue!", $"Oops! Check your internet and try again.", "OK");
                    return;
                }

                IsBusy = true;
                var pets = await petService.GetPets();

                if (Pets.Count != 0)
                    Pets.Clear();

                foreach (var pet in pets)
                    Pets.Add(pet);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                await Shell.Current.DisplayAlert("Error!", $"Unable to get pets : {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
                IsRefreshing = false;
            }
        }

        [RelayCommand]
        async Task GoToDetailsAsync(Pet pet)
        {
            if (pet is null) return;

            await Shell.Current.GoToAsync($"{nameof(DetailsPage)}", true,
                new Dictionary<string, object>
                {
                    {"Pet", pet}
                });
        }

        [RelayCommand]
        async Task GetClosestPetAsync()
        {
            if(IsBusy || !Pets.Any()) return;

            try
            {
                var location = await geolocation.GetLastKnownLocationAsync();
                if(location is null)
                {
                    location = await geolocation.GetLocationAsync(
                        new GeolocationRequest
                        {
                            DesiredAccuracy = GeolocationAccuracy.Medium,
                            Timeout = TimeSpan.FromSeconds(30),
                        });
                }

                if (location is null)
                    return;

                var first = Pets.OrderBy(p => location.CalculateDistance(p.Latitude, p.Longitude, DistanceUnits.Kilometers)).FirstOrDefault();

                if(first is null) return;

                await Shell.Current.DisplayAlert("Closest Pet",
                    $"{first.Name} in {first.Location}", "OK");



            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                await Shell.Current.DisplayAlert("Error!",
                    $"Unable to get closest pet : {ex.Message}", "OK");
            }
        }

        bool IsNetworkConnectivity() => (connectivity.NetworkAccess != NetworkAccess.Internet);
        //{
            //if(connectivity.NetworkAccess != NetworkAccess.Internet)
            //{
            //    return false;
            //}

            //return true;
        //}
    }
}
