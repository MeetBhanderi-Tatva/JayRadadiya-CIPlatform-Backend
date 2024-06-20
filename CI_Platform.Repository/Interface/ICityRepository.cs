using CI_Platform.Entity;
using CI_Platform.Entity.ResponseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Platform.Repository.Interface
{
    public interface ICityRepository
    {
        Task<ICollection<CityViewModel>> GetCitiesByCountry(int countryId);
        Task<City?> GetCityByName(string? name);

    }
}
