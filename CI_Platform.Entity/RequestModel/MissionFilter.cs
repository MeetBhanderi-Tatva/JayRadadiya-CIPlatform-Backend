using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Platform.Entity.RequestModel
{
    public class MissionFilter
    {
        public string? SerachValue { get; set; }

        public List<Int64> Cities { get; set; } = new List<Int64>();

        public Int64 Country { get; set; } = 0;

        public List<int> Themes {  get; set; } = new List<int>();
        
        public List<int> Skills { get; set; } = new List<int>();

        public int SortingOption { get; set; } = 1;
    }

    public enum SortingOption
    {
        Newest = 1,
        Oldest,
        LowestAvailableSeats,
        HighestAvailableSeats,
        MyFavorites,
        RegistrationDeadline
    }
}
