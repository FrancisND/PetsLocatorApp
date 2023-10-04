using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace PetsLocatorApp.Services
{
    public class PetService : IPetService
    {
        HttpClient httpClient;

        public PetService()
        {
            httpClient = new HttpClient();
        }


        IList<Pet> petList = new List<Pet>();

        public async Task<IList<Pet>> GetPets()
        {
            if(petList?.Count > 0)
                return petList;

            var url = "https://raw.githubusercontent.com/FrancisND/PetsLocatorApp/master/pets-data/petsdata.json";
            var response = await httpClient.GetAsync(url);

            if(response.IsSuccessStatusCode)
            {
                petList = await response.Content.ReadFromJsonAsync<List<Pet>>();
            }

            //using var stream = await FileSystem.OpenAppPackageFileAsync("petsdata.json");
            //using var reader = new StreamReader(stream);
            //var contents = await reader.ReadToEndAsync();
            //petList = JsonSerializer.Deserialize(contents, PetContext.Default.ListPet);

            return petList;
        }
    }
}
