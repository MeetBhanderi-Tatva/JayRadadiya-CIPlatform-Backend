using CI_Platform.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Platform.Entity.ResponseModel
{
    public class AddMissionViewModel
    {
        public ICollection<CountryViewModel> Countries { get; set; }

        public ICollection<ThemeViewModel> Themes { get; set; }

        public ICollection<SkillViewModel> Skills { get; set; }
    }
}
