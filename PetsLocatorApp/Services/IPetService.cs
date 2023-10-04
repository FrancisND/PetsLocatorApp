using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetsLocatorApp.Services
{
    public interface IPetService
    {
        Task<IList<Pet>> GetPets();
    }
}
