using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrpcServer.Model
{
    public class Movie
    {
        public string Title { get; set; }
        public int Year { get; set; }
        public string imdbID { get; set; }
    }
}
