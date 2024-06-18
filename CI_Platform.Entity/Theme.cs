using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Platform.Entity
{
    [Table("Theme")]
    public class Theme
    {
        [Key]
        [MaxLength(10)]
        public int ThemeId { get; set; }

        [Required]
        [MaxLength(50)]
        [Column("Theme")]
        public string ThemeName { get; set; }

        [Required]
        public bool Status { get; set; }

        public ICollection<Mission> Missions { get; set; }
    }
}
