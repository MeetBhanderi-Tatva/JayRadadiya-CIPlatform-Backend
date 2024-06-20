using CI_Platform.Entity.RequestModel;
using CI_Platform.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CI_Platform.Backend.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : Controller
    {

        private readonly IMissionService _missionService;

        public HomeController(IMissionService missionService)
        {
            _missionService = missionService;
        }
        [HttpGet]
        [Route("mission")]
        public async Task<IActionResult> GetAddMissionView()
        {
            return await _missionService.GetAddMissionView();
        }

        /// <summary>
        /// get cities
        /// </summary>
        /// <param name="countryId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("cities/{countryId:int}")]
        public async Task<IActionResult> GetCitiesByCountry(int countryId)
        {
            return await _missionService.GetCitiesByCountry(countryId);
        }

        [HttpPost]
        [Route("mission")]
        public async Task<IActionResult> AddMission([FromForm] CreateMissionModel model)
        {
            return await _missionService.AddMission(model);
        }

        [HttpGet]
        [Route("missions")]
        public async Task<IActionResult> GetAllMissions([FromQuery]MissionFilter model)
        {
            return await _missionService.GetAllMissions(model);
        }
    }
}
