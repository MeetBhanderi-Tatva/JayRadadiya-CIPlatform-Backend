using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Platform.Entity
{
    [Table("City")]
    public class City
    {
        [Key]
        public Int64 CityId { get; set; }

        [Required]
        [ForeignKey("Country")]
        public Int64 CountryId { get; set; }

        [Required]
        [Column("City")]
        public string? CityName {  get; set; }


        public Country Country { get; set; }
        public ICollection<User> Users { get; set; }

        public ICollection<Mission> Missions { get; set; }
    }
}
