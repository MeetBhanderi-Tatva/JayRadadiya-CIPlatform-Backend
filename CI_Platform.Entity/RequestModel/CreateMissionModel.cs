using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Platform.Entity.RequestModel
{
    public class CreateMissionModel
    {
        [Required]
        public Int64 CountryId { get; set; }

        [Required]
        public Int64 CityId { get; set; }

        [Required]
        public string MissionTitle { get; set; }

        [Required]
        public string MissionShortDescription { get; set; }

        [Required]
        public string MissionDescription { get; set; }

        [Required]
        public string MissionOrganisationName { get; set; }

        [Required]
        public string MissionOrganisationDetail { get; set; }

        public DateOnly MissionStartDate { get; set; }

        public DateOnly MissionEndDate { get; set; }


        public int TotalSeats { get; set; }

        [Required]
        public int TotalGoal { get; set; }

        public string? GoalObject { get; set; }

        public DateOnly MissionRegistrationDeadline { get; set; }

        [Required]
        public int ThemeId { get; set; }

        public List<int> MissionSkill { get; set; }

        [Required]
        public List<IFormFile>? Images { get; set; } //one video and one image

        public List<IFormFile>? Document { get; set; }

        [Required]
        public int MissionAvailability { get; set; }
    }

  
}
//public enum MissionAvailibility
//{

//}
//public enum SortingOption
//{
//    Newest = 1,
//    Oldest,
//    LowestAvailableSeats,
//    HighestAvailableSeats,
//    MyFavorites,
//    RegistrationDeadline
//}