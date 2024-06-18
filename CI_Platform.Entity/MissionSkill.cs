using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Platform.Entity
{
    public class MissionSkill
    {
        [Key]
        [MaxLength(20)]
        public Int64 Id { get; set; }

        [MaxLength(10)]
        [ForeignKey("Skill")]
        public int SkillId { get; set; }

        [MaxLength(20)]
        [ForeignKey("Mission")]
        public Int64 MissionId { get; set; }

        public Skill Skill { get; set; }
        public Mission Mission { get; set; }
    }
}
