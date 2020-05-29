using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Analysis_Services_REST_API.Models
{
    public class RequestHierarchy
    {
        public String Name { get; set; }

        public List<RequestLevel> LevelDataList { get; set; }
    }
}
