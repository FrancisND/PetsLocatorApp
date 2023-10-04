﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetsLocatorApp.Services;

namespace PetsLocatorApp.ViewModel
{
    public partial class PetsViewModel : BaseViewModel
    {
        IPetService petService;
        
        public ObservableCollection<Pet> Pets { get; } = new();

        public PetsViewModel(IPetService petService)
        {
            Title = "Pet Locator";
            petService = petService;
        }

        [RelayCommand]
        async Task GetPetsAsync()
        {
            if (IsBusy)
                return;

            try
            {
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
            }
        }
    }
}
