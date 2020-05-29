using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Analysis_Services_REST_API.Models
{
    public class MdxResultDataSet
    {
        public List<String> RowNameList { get; set; }
        public List<String> ColumnNameList { get; set; }

        public List<List<String>> Cells { get; set; }

        public MdxResultDataSet()
        {
            RowNameList = new List<string>();
            ColumnNameList = new List<string>();

            Cells = new List<List<String>>();
        }
    }
}
