using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Platform.Entity
{
    [Table("Story")]
    public class Story
    {
        [Key]
        [MaxLength(20)]
        public Int64 StoryId { get; set; }

        [Required]
        [MaxLength(50)]
        public string StoryTitle { get; set; }

        [Required]
        [MaxLength(128)]
        public string MissionTitle { get; set; }

        [Required]
        [MaxLength(1000)]
        public string StoryDescription { get; set; }

        [Required]
        [ForeignKey("User")]
        public Int64 UserId { get; set; }

        public bool Status { get; set; }
        public User User { get; set; }

    }
}
