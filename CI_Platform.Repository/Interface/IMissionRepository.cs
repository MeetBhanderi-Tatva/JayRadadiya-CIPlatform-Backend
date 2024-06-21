using CI_Platform.Entity.RequestModel;
using CI_Platform.Entity.ResponseModel;
using CI_Platform.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage;

namespace CI_Platform.Repository.Interface
{
    public interface IMissionRepository
    {
        IDbContextTransaction BeginTransaction();

        Task Save();

        Task<ICollection<CountryViewModel>> GetCountries();

        Task<ICollection<ThemeViewModel>> GetThemes();

        Task<ICollection<SkillViewModel>> GetSkills();

        Task AddMission(Mission mission);

        Task AddMissionMedia(List<MissionMedia> missionMedias);

        Task<Country?> GetCountryByName(string? name);

        Task<List<Missions>> GetAllMissions(MissionFilter model,int userId);

        Task AddMissionSkills(List<MissionSkill> missionSkills);
    }
}
