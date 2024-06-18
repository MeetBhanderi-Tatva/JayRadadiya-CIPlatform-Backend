using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Platform.Entity
{
    [Table("CMSPrivacyPolicy")]
    public class CMSPrivacyPolicy
    {
        [Key]
        [MaxLength(20)]
        public Int64 CMSId { get; set; }

        [Required]
        [MaxLength(50)]
        public string PageTitle { get; set; }
        
        [Required]
        [MaxLength(2048)]
        public string PageDescription { get; set; }
        
        [Required]
        [MaxLength(1000)]
        public string Slug { get; set; }

        [Required]
        public bool Status { get; set; }


    }
}
