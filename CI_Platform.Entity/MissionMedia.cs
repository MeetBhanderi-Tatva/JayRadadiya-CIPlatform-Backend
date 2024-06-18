using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Platform.Entity
{
    [Table("MissionMedia")]
    public class MissionMedia
    {
        [Key]
        [MaxLength(20)]
        public Int64 MediaId { get; set; }

        public byte[]? Image { get; set; }
        public byte[]? Document { get; set; }

        [Required]
        [ForeignKey("Mission")]
        public Int64 MissionId { get; set; }

        public Mission Mission { get; set; }

    }
}
