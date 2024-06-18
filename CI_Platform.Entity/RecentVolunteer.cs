using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Platform.Entity
{
    [Table("RecentVolunteer")]
    public class RecentVolunteer
    {
        [Key]
        [MaxLength(20)]
        public Int64 VolunteerId { get; set; }

        [Required]
        [ForeignKey("User")]
        public Int64 UserId { get; set; }

        [Required]
        [ForeignKey("Mission")]
        public Int64 MissionId { get; set; }

        public User User { get; set; }
        public Mission Mission { get; set; }
    }
}
