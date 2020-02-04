using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrpcServer.Model
{
    public class YearSearch
    {
        public int year { get; set; }
        public int movies { get; set; }

        public YearSearch(int year, int total)
        {
            this.year = year;
            this.movies = total;
        }

    }


    public class YearSearchList
    {
        public List<YearSearch> moviesByYear = new List<YearSearch>();
        public int total { get; set; }

        public void getTotalSum()
        {            
            this.total = moviesByYear.Sum(x => x.movies);
        }
    }

}
