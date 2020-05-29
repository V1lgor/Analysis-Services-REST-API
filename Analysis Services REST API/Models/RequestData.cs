using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Analysis_Services_REST_API.Models
{
    public class RequestData
    {
        public String CubeName { get; set; }
        public List<RequestDimension> RowDimensionList { get; set; }
        public List<RequestDimension> ColumnDimensionList { get; set; }

    }
}
