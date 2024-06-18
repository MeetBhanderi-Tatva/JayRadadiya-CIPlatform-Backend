using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Platform.Entity
{
    [Table("Comment")]
    public class Comment
    {
        [Key]
        [MaxLength(20)]
        public Int64 CommentId { get; set; }

        [Required]
        [MaxLength(50)]
        public string MissionTitle { get; set; }

        [Required]
        [ForeignKey("User")]
        public Int64 UserId { get; set; }

        [Required]
        [MaxLength(50)]
        public string? UserName { get; set; }

        [MaxLength(256)]
        [Column("Comment")]
        public string? CommentContent { get; set; }

        [Required]
        [Column(TypeName = "timestamp without time zone")]
        public DateTime CreatedAt { get; set; }

        [Column(TypeName = "timestamp without time zone")]
        public DateTime? UpdatedAt { get; set; }

        [Column(TypeName = "timestamp without time zone")]
        public DateTime? DeletedAt { get; set; }

        public User User { get; set; }
    }
}
