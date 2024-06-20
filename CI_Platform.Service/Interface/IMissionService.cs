using CI_Platform.Entity.RequestModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Platform.Service.Interface
{
    public interface IMissionService
    {
        Task<JsonResult> GetAddMissionView();

        Task<JsonResult> GetCitiesByCountry(int countryId);

        Task<JsonResult> AddMission(CreateMissionModel model);

        Task<JsonResult> GetAllMissions(MissionFilter model);
    }
}
