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
        public PetDetailsViewModel()
        {
            
        }

        [ObservableProperty]
        Pet pet;
    }
}
