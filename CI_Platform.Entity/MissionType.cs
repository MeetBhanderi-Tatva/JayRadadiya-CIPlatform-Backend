using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Platform.Entity
{
    public class MissionType
    {
        [Key]
        [MaxLength(10)]
        public int Id { get; set; }

        [Required]
        public string Type { get; set; } = null!;

        public ICollection<Mission> Missions { get; set; } = new HashSet<Mission>();

    }
}
