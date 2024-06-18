using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Platform.Entity
{
    [Table("Country")]
    public class Country
    {

        [Key]
        public Int64 CountryId { get; set; }

        [Required]
        [Column("Country")]
        public string CountryName {  get; set; }

        public ICollection<User> Users { get; set; }

        public ICollection<City> Cities { get; set; }

        public ICollection<Mission> Missions { get; set; }
    }
}
