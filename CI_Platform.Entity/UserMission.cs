using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Platform.Entity
{
    public enum UserStatus
    {
        Inactive = 0,
        Active = 1
    }

    public enum Favourite
    {
        No = 0,
        Yes = 1
    }

    public class UserMission
    {
        [Key]
        [MaxLength(20)]
        public Int64 Id { get; set; }

        [Required]
        [ForeignKey("User")]
        [StringLength(20)]
        public Int64 UserId { get; set; }

        [Required]
        [ForeignKey("Mission")]
        public Int64 MissionId { get; set; }

        [Column(TypeName = "int")]
        [StringLength(10)]
        public int? Ratings { get; set; }

        [Required]
        public int UserStatus { get; set; } = 1;

        [Required]
        public bool Favourite { get; set; } = false;

        public Mission Mission { get; set; }
        public User User { get; set; }
    }
}
