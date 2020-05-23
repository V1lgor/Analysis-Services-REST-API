using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AnalysisServices.AdomdClient;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Analysis_Services_REST_API.Models;

namespace Analysis_Services_REST_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CubeController : ControllerBase
    {
        public string Get()
        {
            using(AdomdConnection mdConn = new AdomdConnection())
            {
                mdConn.ConnectionString = "provider=msolap;Data Source=V1LGORPC\\ASMAIN;initial catalog=AdventureWorksDW2014Multidimensional-EE;";
                mdConn.Open();

                List<Cube> cubeNameList = new List<Cube>();

                foreach(CubeDef cube in mdConn.Cubes)
                {
                    if (cube.Name.StartsWith('$')) continue;

                    cubeNameList.Add(new Cube { Name = cube.Name });
                }

                return JsonConvert.SerializeObject(cubeNameList, Formatting.Indented);
            }
        }
    }
}