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
        public MissionService(IMissionRepository missionRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor, ICityRepository ciryRepo)
        {
            _missionRepo = missionRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _ciryRepo = ciryRepo;
        }
        public string GetUserId()
        {
            try
            {
                var user = _httpContextAccessor.HttpContext?.User;
                return user?.FindFirst("userId")?.Value;
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
                    MissionMedia missionMedia = new();
                    // Check if exactly 2 files are uploaded
                    if (model.Images.Count != 2)
                    {
                        return new JsonResult(new ApiResponse<string>
                        {
                            Result = false,
                            Message = "Please upload one image and one video",
                            StatusCode = HttpStatusCode.BadRequest.ToString(),
                        });
                    }
                    else
                    {
                        bool hasImage = false;
                        bool hasVideo = false;

                        foreach (var file in model.Images)
                        {
                            string extension = Path.GetExtension(file.FileName).ToLower();

                            if (ImageExtensions.Contains(extension))
                            {
                                hasImage = true;
                            }
                            else if (VideoExtensions.Contains(extension))
                            {
                                hasVideo = true;
                            }
                            else
                            {
                                return new JsonResult(new ApiResponse<string>
                                {
                                    Result = false,
                                    Message = "Invalid file type. Please upload only image and video files.",
                                    StatusCode = HttpStatusCode.BadRequest.ToString(),
                                });
                            }
                        }

                        if (!hasImage || !hasVideo)
                        {
                            return new JsonResult(new ApiResponse<string>
                            {
                                Result = false,
                                Message = "Please upload one image and one video",
                                StatusCode = HttpStatusCode.BadRequest.ToString(),
                            });
                        }
                        else
                        {
                            foreach (var file in model.Images)
                            {
                                string extension = Path.GetExtension(file.FileName).ToLower();

                                if (ImageExtensions.Contains(extension))
                                {
                                    using (var memoryStream = new MemoryStream())
                                    {
                                        await file.CopyToAsync(memoryStream);
                                        missionMedia.Image = memoryStream.ToArray();
                                    }
                                }
                                else if (VideoExtensions.Contains(extension))
                                {
                                    using (var memoryStream = new MemoryStream())
                                    {
                                        await file.CopyToAsync(memoryStream);
                                        mission.MissionVideo = memoryStream.ToArray();
                                    }
                                }
                                else
                                {
                                    return new JsonResult(new ApiResponse<string>
                                    {
                                        Result = false,
                                        Message = "Invalid file type. Please upload only image and video files.",
                                        StatusCode = HttpStatusCode.BadRequest.ToString(),
                                    });
                                }
                            }
                        }
                        await _missionRepo.AddMission(mission);
                        missionMedia.MissionId = mission.MissionId;

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

                        using (var memoryStream = new MemoryStream())
                        {
                            await model.Document[0].CopyToAsync(memoryStream);
                            missionMedia.Document = memoryStream.ToArray();
                        }
                        await _missionRepo.AddMissionMedia(missionMedia);
                        transaction.Commit();
                        return new JsonResult(new ApiResponse<string>
                        {
                            Result = true,
                            Message = "Mission created successfully",
                            StatusCode = HttpStatusCode.OK.ToString(),
                        });
                    }


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
                if (!Enum.IsDefined(typeof(SortingOption), model.SortingOption))
                {
                    return new JsonResult(new ApiResponse<string>
                    {
                        Result = false,
                        Message = "Bad request",
                        StatusCode = HttpStatusCode.BadRequest.ToString(),
                    });
                }
                var missions = await _missionRepo.GetAllMissions(model);
                //switch (model.SortingOption)
                //{
                //    case 1:
                //        missions = missions.OrderByDescending(x => x.CreatedAt).ToList(); break;
                //    case 2:
                //        missions = missions.OrderBy(x => x.CreatedAt).ToList(); break;
                //    case 3:
                //        missions = missions.OrderBy(x => x.TotalSeats - x.OccupiedSeats).ToList(); break;
                //    case 4:
                //        missions = missions.OrderByDescending(x => x.TotalSeats - x.OccupiedSeats).ToList(); break;
                //    case 5:
                //        var result = missions.Where(x => x.UserMissions.First().Favourite == 1).ToList();
                //        var result2 = missions.Where(x => x.UserMissions.First().Favourite == 0).ToList();
                //        missions = result;
                //        missions.AddRange(result2);
                //        break;
                //    case 6:
                //        missions = missions.Where(x => x.MissionRegistrationDeadline > DateOnly.FromDateTime(DateTime.Now)).OrderBy(x => x.MissionRegistrationDeadline.ToDateTime(new TimeOnly(0, 0)) - DateTime.Now).ToList(); break;
                //        //var query = missions.GroupBy(x => x.UserMissions.FirstOrDefault().Favourite, x => x, (value, ids) => new
                //        //{
                //        //    key = value,
                //        //    list = ids
                //        //});
                //        //missions = new List<Mission>();
                //        //foreach (var result in query)
                //        //{
                //        //    missions = missions.AddRange(result.list);
                //        //}

                //}

                //var modelMissions = _mapper.Map<List<Missions>>(missions);

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
