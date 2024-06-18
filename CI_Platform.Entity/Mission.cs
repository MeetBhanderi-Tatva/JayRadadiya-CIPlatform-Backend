using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace CI_Platform.Entity
{
    [Table("Mission")]
    public class Mission
    {
        [Key]
        [MaxLength(20)]
        public Int64 MissionId { get; set; }

        [Required]
        [MaxLength(128)]
        public string MissionTitle { get; set; }

        [MaxLength(256)]
        public string? MissionShortDescription { get; set; }

        [Required]
        [MaxLength(2048)]
        public string MissionDescription { get; set; }

        [Required]
        [MaxLength(20)]
        [ForeignKey("Country")]
        public Int64 CountryId { get; set; }
        
        [Required]
        [MaxLength(20)]
        [ForeignKey("City")]
        public Int64 CityId { get; set; }

        [Required]
        [MaxLength(10)]
        [ForeignKey("Theme")]
        public int ThemeId { get; set; }


        [Required]
        [MaxLength(50)]
        public string MissionOrganisationName { get; set; }
        [Required]
        [MaxLength(2048)]
        public string MissionOrganisationDetail{ get; set; }

        [Required]
        [Column(TypeName = "date")]
        public DateOnly MissionStartDate { get; set; }

        [Required]
        [Column(TypeName = "date")]
        public DateOnly MissionEndDate { get; set; }


        [Required]
        [MaxLength(50)]
        public int MissionType { get; set; }

        [Required]
        [MaxLength(10)]
        public int TotalSeats { get; set; }

        [MaxLength(10)]
        public int OccupiedSeats { get; set; }
        
        [Required]
        [MaxLength(10)]
        public int TotalGoal { get; set; }

        [MaxLength(10)]
        public int AchievedGoal { get; set; }

        public string? GoalObject { get; set; }

        [MaxLength(20)]
        public int? MissionRating { get; set; }

        [MaxLength(50)]
        public int? MissionRatingCount { get; set; }

        [Required]
        [Column(TypeName = "date")]
        public DateOnly MissionRegistrationDeadline { get; set; }

        [Required]
        public int MissionAvailability { get; set; }

        
        public byte[]? MissionVideo { get; set; }

        [Required]
        [Column(TypeName = "timestamp without time zone")]
        public DateTime CreatedAt { get; set; }

        [Column(TypeName = "timestamp without time zone")]
        public DateTime? UpdatedAt { get; set; }

        [Column(TypeName = "timestamp without time zone")]
        public DateTime? DeletedAt { get; set; }

        public ICollection<MissionMedia> MissionMedias { get; set; }
        public ICollection<MissionApplication> MissionApplications { get; set; }
        public ICollection<RecentVolunteer> RecentVolunteers { get; set; }

        public ICollection<VolunteeringTimesheet> VolunteeringTimesheets { get; set; }

        public ICollection<StoryMedia> StoryMedia { get; set; }

        public ICollection<UserMission> UserMissions { get; set; }

        public ICollection<MissionSkill> MissionSkills { get; set; }

        public ICollection<MissionType> MissionTypes { get; set; }

        public City City { get; set; }

        public Country Country { get; set; }

        public Theme Theme { get; set; }
    }
}
