using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AnalysisServices.AdomdClient;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Analysis_Services_REST_API.Utilities;
using Analysis_Services_REST_API.Models;

namespace Analysis_Services_REST_API.Controllers
{
    [Route("api/cube/{cubeId}/dimension")]
    [ApiController]
    public class DimensionController : ControllerBase
    {
        public string GetCubeDimensions(int cubeId)
        {
            using (AdomdConnection mdConn = new AdomdConnection())
            {
                mdConn.ConnectionString = "provider=msolap;Data Source=V1LGORPC\\ASMAIN;initial catalog=AdventureWorksDW2014Multidimensional-EE;";
                mdConn.Open();
                
                List<CubeDef> realCubeList = Utilities.Utilities.GetRealCubeList(mdConn.Cubes);

                CubeDef cubeDef = realCubeList[cubeId];

                List<string> dimensionList = new List<string>();

                foreach (Microsoft.AnalysisServices.AdomdClient.Dimension dimension in cubeDef.Dimensions)
                {
                    dimensionList.Add(dimension.Name);
                }

                return JsonConvert.SerializeObject(dimensionList, Formatting.Indented);
            }
        }

        [Route("{dimensionId}")]
        public string GetCubeDimensionById(int cubeId, int dimensionId)
        {
            using (AdomdConnection mdConn = new AdomdConnection())
            {
                mdConn.ConnectionString = "provider=msolap;Data Source=V1LGORPC\\ASMAIN;initial catalog=AdventureWorksDW2014Multidimensional-EE;";
                mdConn.Open();

                List<CubeDef> realCubeList = Utilities.Utilities.GetRealCubeList(mdConn.Cubes);

                CubeDef cubeDef = realCubeList[cubeId];

                List<string> dimensionList = new List<string>();

                Microsoft.AnalysisServices.AdomdClient.Dimension serverDimension = cubeDef.Dimensions[dimensionId];

                Models.Dimension dimension = new Models.Dimension(serverDimension);                

                return JsonConvert.SerializeObject(dimension, Formatting.Indented);
            }
        }
    }
}