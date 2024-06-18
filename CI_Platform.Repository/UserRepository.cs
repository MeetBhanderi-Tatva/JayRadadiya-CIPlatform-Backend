using CI_Platform.Entity.RequestModel;
using CI_Platform.Entity.ResponseModel;
using CI_Platform.Entity;
using CI_Platform.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CI_Platform.Entity.DBContext;
using Microsoft.EntityFrameworkCore;
using CI_Platform.Entity.Models;
using System.Reflection;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using Npgsql;
using Dapper;

namespace CI_Platform.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;
        private readonly string _connectionString;
        public UserRepository(AppDbContext context, IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
            _context = context;
        }

        public IDbContextTransaction BeginTransaction()
        {
            return _context.Database.BeginTransaction();
        }
        public async Task<User> FindUserAsync(LoginRequest model)
        {
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == model.Email && x.Password == model.Password);
                return user;
            }
            catch
            {
                throw new Exception();
            }

        }

        public async Task<User> FindUserByEmailAsync(string email)
        {
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
                return user;
            }
            catch
            {
                throw new Exception();
            }
        }

        public async Task AddUserAsync(User user)
        {
            try
            {
                await _context.AddAsync(user);
                await _context.SaveChangesAsync();
            }
            catch
            {
                throw new Exception();
            }
        }

        public async Task Save()
        {
            try
            {
                await _context.SaveChangesAsync();
            }
            catch
            {
                throw new Exception();
            }
        }

        public async Task<ICollection<CountryViewModel>> GetCountries()
        {
            try
            {
                var countries = await (from con in _context.Countries
                                       select new CountryViewModel()
                                       {
                                           CountryId = con.CountryId,
                                           CountryName = con.CountryName,
                                       }).ToListAsync();
                return countries;
            }
            catch
            {
                throw new Exception();
            }
        }

        public async Task<ICollection<ThemeViewModel>> GetThemes()
        {
            try
            {
                var themes = await (from tm in _context.Themes
                                    select new ThemeViewModel()
                                    {
                                        ThemeId = tm.ThemeId,
                                        ThemeName = tm.ThemeName,
                                    }).ToListAsync();
                return themes;
            }
            catch
            {
                throw new Exception();
            }
        }

        public async Task<ICollection<SkillViewModel>> GetSkills()
        {
            try
            {
                var skills = await (from sk in _context.Skills
                                    select new SkillViewModel()
                                    {
                                        SkillId = sk.SkillId,
                                        SkillName = sk.Skills,
                                    }).ToListAsync();
                return skills;
            }
            catch
            {
                throw new Exception();
            }
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
            catch
            {
                throw new Exception();
            }
        }

        public async Task AddMission(Mission mission)
        {
            try
            {
                await _context.Missions.AddAsync(mission);
                await _context.SaveChangesAsync();
            }
            catch
            {
                throw new Exception();
            }
        }

        public async Task AddMissionMedia(MissionMedia missionMedia)
        {
            try
            {
                await _context.MissionMedias.AddAsync(missionMedia);
                await _context.SaveChangesAsync();
            }
            catch
            {
                throw new Exception();
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
            catch
            {
                throw new Exception();
            }
        }
        public async Task<Country?> GetCountryByName(string? name)
        {
            try
            {
                if (name == null)
                {
                    return null;
                }
                var country = await _context.Countries.FirstOrDefaultAsync(x => x.CountryName.ToLower().Equals(name.ToLower()));
                return country;
            }
            catch
            {
                throw new Exception();
            }
        }

        public async Task AddMissionSkills(List<MissionSkill> missionSkills)
        {
            try
            {
                await _context.AddRangeAsync(missionSkills);
                await _context.SaveChangesAsync();
            }
            catch
            {
                throw new Exception();
            }
        }

        public async Task<List<Missions>> GetAllMissions(MissionFilter model)
        {
            try
            {
                using (var connection = new NpgsqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    var parameters = new
                    {
                        search_value = model.SerachValue ?? (object)DBNull.Value,
                        city_ids = model.Cities?.Count > 0 ? (object)model.Cities.ToArray() : DBNull.Value,
                        country_id = model.Country,
                        theme_ids = model.Themes?.Count > 0 ? (object)model.Themes.ToArray() : DBNull.Value,
                        skill_ids = model.Skills?.Count > 0 ? (object)model.Skills.ToArray() : DBNull.Value,
                        sorting_option = model.SortingOption
                    };

                    var sql = "SELECT * FROM GetMissions(@search_value, @city_ids, @country_id, @theme_ids, @skill_ids, @sorting_option)";

                    var missions = (await connection.QueryAsync<Missions>(sql, parameters)).ToList();

                    return missions;
                }
                //return await _context.Missions
                //    .Include(o => o.MissionMedias)
                //    .Include(o => o.MissionSkills)
                //    .Where(o => (string.IsNullOrEmpty(model.SerachValue) || o.MissionTitle.ToLower().Trim().Contains(model.SerachValue.ToLower().Trim())) &&
                //                (model.Cities.Count == 0 || model.Cities.Contains(o.CityId)) &&
                //                (model.Themes.Count == 0 || model.Themes.Contains(o.ThemeId)) &&
                //                (model.Skills.Count == 0 || model.Skills.Any(item => o.MissionSkills.Select(x => x.SkillId).ToList().Contains(item))) &&
                //                (model.Country == 0 || model.Country == o.CountryId))
                //    .ToListAsync();  
            }
            catch
            {
                throw new Exception();
            }
        }

    }
}
