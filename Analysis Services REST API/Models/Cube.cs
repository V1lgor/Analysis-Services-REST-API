using Microsoft.AnalysisServices.AdomdClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Analysis_Services_REST_API.Models
{
    public class Cube
    {
        public Cube()
        {

        }

        public Cube(String name, List<Models.Dimension> dimensionList)
        {
            Name = name;
            DimensionList = dimensionList;
        }
        public Cube(CubeDef cubeDef)
        {
            Name = cubeDef.Name;
            DimensionList = new List<Models.Dimension>();

            foreach (Microsoft.AnalysisServices.AdomdClient.Dimension dimension in cubeDef.Dimensions)
            {
                Models.Dimension currentDimension = new Models.Dimension(dimension);
                
                DimensionList.Add(currentDimension);
            }
        }
        public String Name { get; set; }

        public List<Dimension> DimensionList { get; set; }
    }
}
