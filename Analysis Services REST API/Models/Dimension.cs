using Microsoft.AnalysisServices.AdomdClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Analysis_Services_REST_API.Models
{
    public class Dimension
    {
        public Dimension() { }

        public Dimension(Microsoft.AnalysisServices.AdomdClient.Dimension serverDimension)
        {
            Name = serverDimension.Name;
            HierarchyList = new List<Models.Hierarchy>();

            foreach (Microsoft.AnalysisServices.AdomdClient.Hierarchy hierarchy in serverDimension.Hierarchies)
            {
                Models.Hierarchy newHierarchy = new Models.Hierarchy(hierarchy);
                
                HierarchyList.Add(newHierarchy);
            }
        }


        public string Name { get; set; }

        public List<Hierarchy> HierarchyList { get; set; }

    }
}
