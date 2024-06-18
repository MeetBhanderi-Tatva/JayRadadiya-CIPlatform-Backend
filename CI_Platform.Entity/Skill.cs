using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Platform.Entity
{
    [Table("Skill")]
    public class Skill
    {
        [Key]
        [MaxLength(10)]
        public int SkillId { get; set; }

        [Required]
        [MaxLength(20)]
        public string Skills { get; set; }

        [Required]
        public bool Status { get; set; }

        public ICollection<MissionSkill> MissionSkills { get; set; }
    }
}
