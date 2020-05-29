using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Analysis_Services_REST_API.Models;
using Microsoft.AnalysisServices.AdomdClient;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Analysis_Services_REST_API.Controllers
{
    [Route("api/cube/{cubeId}/dimension/{dimensionId}/hierarchy")]
    [ApiController]
    public class HierarchyController : ControllerBase
    {
        public string GetHierarchies(int cubeId, int dimensionId)
        {
            using (AdomdConnection mdConn = new AdomdConnection())
            {
                mdConn.ConnectionString = "provider=msolap;Data Source=V1LGORPC\\ASMAIN;initial catalog=AdventureWorksDW2014Multidimensional-EE;";
                mdConn.Open();

                List<CubeDef> realCubeList = Utilities.Utilities.GetRealCubeList(mdConn.Cubes);

                CubeDef cubeDef = realCubeList[cubeId];

                Microsoft.AnalysisServices.AdomdClient.Dimension dimension = cubeDef.Dimensions[dimensionId];

                List<Models.Hierarchy> hierarchyList = new List<Models.Hierarchy>();

                foreach(Microsoft.AnalysisServices.AdomdClient.Hierarchy hierarchy in dimension.Hierarchies)
                {
                    Models.Hierarchy newHierarchy = new Models.Hierarchy(hierarchy);
                    
                    hierarchyList.Add(newHierarchy);
                }

                return JsonConvert.SerializeObject(hierarchyList, Formatting.Indented);
            }
        }
    }
}