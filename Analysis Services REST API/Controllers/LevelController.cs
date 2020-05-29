using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AnalysisServices.AdomdClient;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Analysis_Services_REST_API.Controllers
{
    [Route("api/cube/{cubeId}/dimension/{dimensionId}/hierarchy/{hierarchyId}/level")]
    [ApiController]
    public class LevelController : ControllerBase
    {
        public String GetLevelList(int cubeId, int dimensionId, int hierarchyId)
        {
            using (AdomdConnection mdConn = new AdomdConnection())
            {
                mdConn.ConnectionString = "provider=msolap;Data Source=V1LGORPC\\ASMAIN;initial catalog=AdventureWorksDW2014Multidimensional-EE;";
                mdConn.Open();

                List<CubeDef> realCubeList = Utilities.Utilities.GetRealCubeList(mdConn.Cubes);

                CubeDef cubeDef = realCubeList[cubeId];

                Microsoft.AnalysisServices.AdomdClient.Hierarchy hierarchy = cubeDef.Dimensions[dimensionId].Hierarchies[hierarchyId];

                List<Models.Level> levelList = new List<Models.Level>();

                foreach (Microsoft.AnalysisServices.AdomdClient.Level level in hierarchy.Levels)
                {
                    Models.Level newLevel = new Models.Level(level);

                    levelList.Add(newLevel);
                }

                return JsonConvert.SerializeObject(levelList, Formatting.Indented);
            }
        }
        [Route("{levelId}")]
        public String GetLevelById(int cubeId, int dimensionId, int hierarchyId, int levelId)
        {
            using (AdomdConnection mdConn = new AdomdConnection())
            {
                mdConn.ConnectionString = "provider=msolap;Data Source=V1LGORPC\\ASMAIN;initial catalog=AdventureWorksDW2014Multidimensional-EE;";
                mdConn.Open();

                List<CubeDef> realCubeList = Utilities.Utilities.GetRealCubeList(mdConn.Cubes);

                CubeDef cubeDef = realCubeList[cubeId];

                Microsoft.AnalysisServices.AdomdClient.Level serverLevel 
                    = cubeDef.Dimensions[dimensionId]
                    .Hierarchies[hierarchyId]
                    .Levels[levelId];

                Models.Level level = new Models.Level(serverLevel);

                return JsonConvert.SerializeObject(level, Formatting.Indented);
            }
        }
    }
}