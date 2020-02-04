using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrpcServer.Model
{
    public class RootObject
    {
        public int per_page { get; set; }
        public int total { get; set; }
        public int total_pages { get; set; }
        public List<Movie> data { get; set; }
    }
}
