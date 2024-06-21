using CI_Platform.Entity.RequestModel;
using CI_Platform.Entity.ResponseModel;
using CI_Platform.Entity;
using CI_Platform.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CI_Platform.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace CI_Platform.Service
{
    public class MissionService : IMissionService
    {
        private readonly IMissionRepository _missionRepo;
        private readonly ICityRepository _ciryRepo;
        public IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private static readonly HashSet<string> ImageExtensions = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
    {
        ".jpg", ".jpeg", ".png", ".gif", ".bmp", ".tiff", ".ico", ".svg", ".webp"
    };

        private static readonly HashSet<string> VideoExtensions = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
    {
        ".mp4", ".mov", ".wmv", ".flv", ".avi", ".mkv", ".webm", ".mpeg", ".mpg", ".m4v"
    };
        private static readonly HashSet<string> DocumentExtensions = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
    {
        ".pdf", ".doc"
    };
        public MissionService(IMissionRepository missionRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor, ICityRepository ciryRepo)
        {
            _missionRepo = missionRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _ciryRepo = ciryRepo;
        }
        public int GetUserId()
        {
            try
            {
                var user = _httpContextAccessor.HttpContext?.User;
                return int.Parse(user?.FindFirst("userId")?.Value!);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<JsonResult> GetAddMissionView()
        {
            try
            {
                var countries = await _missionRepo.GetCountries();
                var themes = await _missionRepo.GetThemes();
                var skills = await _missionRepo.GetSkills();
                AddMissionViewModel model = new()
                {
                    Countries = countries,
                    Themes = themes,
                    Skills = skills,
                };
                return new JsonResult(new ApiResponse<AddMissionViewModel>
                {
                    Data = model,
                    Result = true,
                    Message = "",
                    StatusCode = HttpStatusCode.OK.ToString(),
                });
            }
            catch (Exception ex)
            {
                return new JsonResult(new ApiResponse<string>
                {
                    Result = false,
                    Message = ex.Message.ToString(),
                    StatusCode = HttpStatusCode.InternalServerError.ToString(),
                });
            }
        }
        public async Task<JsonResult> GetCitiesByCountry(int countryId)
        {
            try
            {
                var userId = GetUserId();
                var cities = await _ciryRepo.GetCitiesByCountry(countryId);
                return new JsonResult(new ApiResponse<ICollection<CityViewModel>>
                {
                    Data = cities,
                    Result = true,
                    Message = "",
                    StatusCode = HttpStatusCode.OK.ToString(),
                });
            }
            catch (Exception ex)
            {
                return new JsonResult(new ApiResponse<string>
                {
                    Result = false,
                    Message = ex.Message.ToString(),
                    StatusCode = HttpStatusCode.InternalServerError.ToString(),
                });
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns> </returns>
        public async Task<JsonResult> AddMission(CreateMissionModel model)
        {
            using (var transaction = _missionRepo.BeginTransaction())
                try
                {
                    Mission mission = _mapper.Map<Mission>(model);
                    List<MissionMedia> missionMedias = new();
                    if (model.Images?.Count != 0)
                    {
                        foreach (var file in model.Images!)
                        {
                            string extension = Path.GetExtension(file.FileName).ToLower();

                            if (ImageExtensions.Contains(extension))
                            {
                                using (var memoryStream = new MemoryStream())
                                {
                                    await file.CopyToAsync(memoryStream);
                                    MissionMedia missionMedia = new();
                                    missionMedia.Image = memoryStream.ToArray();
                                    missionMedias.Add(missionMedia);
                                }
                            }
                            else
                            {
                                return new JsonResult(new ApiResponse<string>
                                {
                                    Result = false,
                                    Message = "Please upload image files only",
                                    StatusCode = HttpStatusCode.BadRequest.ToString(),
                                });
                            }
                        }
                    }
                    if (model.Document?.Count != 0)
                    {
                        foreach (var file in model.Document!)
                        {
                            string extension = Path.GetExtension(file.FileName).ToLower();

                            if (DocumentExtensions.Contains(extension))
                            {
                                using (var memoryStream = new MemoryStream())
                                {
                                    await file.CopyToAsync(memoryStream);
                                    MissionMedia missionMedia = new();
                                    missionMedia.Document = memoryStream.ToArray();
                                    missionMedias.Add(missionMedia);
                                }
                            }
                            else
                            {
                                return new JsonResult(new ApiResponse<string>
                                {
                                    Result = false,
                                    Message = "Please upload pdf and doc file",
                                    StatusCode = HttpStatusCode.BadRequest.ToString(),
                                });
                            }
                        }
                    }

                    await _missionRepo.AddMission(mission);
                    foreach (var missionMedia in missionMedias)
                    {
                        missionMedia.MissionId = mission.MissionId;
                    }
                    if (model.MissionSkill.Any())
                    {
                        List<MissionSkill> missionSkills = new();
                        foreach (var skill in model.MissionSkill)
                        {
                            MissionSkill missionSkill = new()
                            {
                                MissionId = mission.MissionId,
                                SkillId = skill
                            };
                            missionSkills.Add(missionSkill);
                        }
                        await _missionRepo.AddMissionSkills(missionSkills);
                    }
                    await _missionRepo.AddMissionMedia(missionMedias);
                    transaction.Commit();
                    return new JsonResult(new ApiResponse<string>
                    {
                        Result = true,
                        Message = "Mission created successfully",
                        StatusCode = HttpStatusCode.OK.ToString(),
                    });
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return new JsonResult(new ApiResponse<string>
                    {
                        Result = false,
                        Message = ex.Message.ToString(),
                        StatusCode = HttpStatusCode.InternalServerError.ToString(),
                    });
                }

        }
        public async Task<JsonResult> GetAllMissions(MissionFilter model)
        {
            try
            {
                var userId = GetUserId();
                var missions = await _missionRepo.GetAllMissions(model,userId);
                return new JsonResult(new ApiResponse<ICollection<Missions>>
                {
                    Data = missions,
                    Result = true,
                    Message = "",
                    StatusCode = HttpStatusCode.OK.ToString(),
                });
            }
            catch (Exception ex)
            {
                return new JsonResult(new ApiResponse<string>
                {
                    Result = false,
                    Message = ex.Message.ToString(),
                    StatusCode = HttpStatusCode.InternalServerError.ToString(),
                });
            }
        }
    }
}
