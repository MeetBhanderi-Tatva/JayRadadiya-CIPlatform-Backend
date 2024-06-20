using CI_Platform.Entity;
using CI_Platform.Entity.DBContext;
using CI_Platform.Entity.ResponseModel;
using CI_Platform.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Platform.Repository
{
    public class CityRepository : ICityRepository
    {
        private readonly AppDbContext _context;
        public CityRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<ICollection<CityViewModel>> GetCitiesByCountry(int countryId)
        {
            try
            {
                return await (from ct in _context.Cities
                              where ct.CountryId == countryId
                              select new CityViewModel()
                              {
                                  CityId = ct.CityId,
                                  CityName = ct.CityName,
                              }).ToListAsync();
            }
            catch(Exception)
            {
                throw;
            }
        }
        public async Task<City?> GetCityByName(string? name)
        {
            try
            {
                if (name == null)
                {
                    return null;
                }
                var city = await _context.Cities.FirstOrDefaultAsync(x => x.CityName.ToLower().Equals(name.ToLower()));
                return city;
            }
            catch(Exception)
            {
                throw;
            }
        }
    }
}
