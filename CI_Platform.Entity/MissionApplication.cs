using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Platform.Entity
{
    [Table("MissionApplication")]
    public class MissionApplication
    {
        [Key]
        [MaxLength(20)]
        public Int64 ApplicationId { get; set; }

        [Required]
        [ForeignKey("User")]
        public Int64 UserId { get; set; }

        [Required]
        [ForeignKey("Mission")]
        public Int64 MissionId { get; set; }

        [Required]
        [Column(TypeName = "timestamp without time zone")]
        public DateTime CreatedAt { get; set; }

        [Column(TypeName = "timestamp without time zone")]
        public DateTime? UpdatedAt { get; set; }

        [Column(TypeName = "timestamp without time zone")]
        public DateTime? DeletedAt { get; set; }

        public User User { get; set; }
        public Mission Mission { get; set; }
    }
}
