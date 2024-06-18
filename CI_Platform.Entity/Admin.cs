using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Platform.Entity
{
    [Table("Admin")]
    public class Admin
    {
        [Key]
        [MaxLength(20)]
        public Int64 AdminId { get; set; }

        [MaxLength(16)]
        public string? FirstName { get; set; }

        [MaxLength(16)]
        public string? LastName { get; set; }

        [Required]
        [MaxLength(128)]
        public string? Email { get; set; }

        [Required]
        [MaxLength(255)]
        public string? Password { get; set; }


        [MaxLength(2048)]
        public string? AdminAvatar { get; set; }

        [Required]
        [Column(TypeName = "timestamp without time zone")]
        public DateTime CreatedAt { get; set; }

        [Column(TypeName = "timestamp without time zone")]
        public DateTime? UpdatedAt { get; set; }

        [Column(TypeName = "timestamp without time zone")]
        public DateTime? DeletedAt { get; set; }

    }
}
