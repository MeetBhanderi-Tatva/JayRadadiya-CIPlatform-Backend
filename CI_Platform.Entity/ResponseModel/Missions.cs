using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Platform.Entity.ResponseModel
{
    public class Missions
    {
        public Int64 MissionId { get; set; }

        public string? MissionTitle { get; set; }

        public string? MissionShortDescription { get; set; }

        public string? MissionDescription { get; set; }

        public Int64 CityId { get; set; }

        public int ThemeId { get; set; }

        public string? MissionOrganisationName { get; set; }

        public int MissionType { get; set; }

        public DateTime? MissionStartDate { get; set; }

        public DateTime? MissionEndDate { get; set; }

        public int TotalSeats { get; set; }

        public int? OccupiedSeats { get; set; }

        public int? AchievedGoal { get; set; }
        public int TotalGoal { get; set; }
        public int? MissionRating { get; set; }

        public string? GoalObject { get; set; }
        public DateTime? MissionRegistrationDeadline { get; set; }

        public byte[]? Image { get; set; }

        public int Favourite {  get; set; }

    }
}
