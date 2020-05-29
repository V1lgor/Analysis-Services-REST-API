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
        public string Get(bool withDimensions = true)
        {
            if (withDimensions) return GetFullCubeList();
            else return GetCubeNameList();
        }

        private string GetFullCubeList()
        {
            using(AdomdConnection mdConn = new AdomdConnection())
            {
                mdConn.ConnectionString = "provider=msolap;Data Source=V1LGORPC\\ASMAIN;initial catalog=AdventureWorksDW2014Multidimensional-EE;";
                mdConn.Open();

                List<Cube> cubeList = new List<Cube>();

                foreach(CubeDef cube in mdConn.Cubes)
                {
                    if (cube.Name.StartsWith('$')) continue;

                    Cube newCube;

                    newCube = new Cube(cube);

                    cubeList.Add(newCube);          
                }

                return JsonConvert.SerializeObject(cubeList, Formatting.Indented);
            }
        }

        // Возвращает список имен всех кубов
        private String GetCubeNameList()
        {
            using (AdomdConnection mdConn = new AdomdConnection())
            {
                mdConn.ConnectionString = "provider=msolap;Data Source=V1LGORPC\\ASMAIN;initial catalog=AdventureWorksDW2014Multidimensional-EE;";
                mdConn.Open();

                List<Cube> cubeList = new List<Cube>();

                foreach (CubeDef cube in mdConn.Cubes)
                {
                    if (cube.Name.StartsWith('$')) continue;

                    Cube newCube = new Cube(cube.Name, null);

                    cubeList.Add(newCube);
                }

                return JsonConvert.SerializeObject(cubeList, Formatting.Indented);
            }
        }


        [Route("{id}")]
        public string Get(int id)
        {
            // Создаем соединение с Analysis Services
            using (AdomdConnection mdConn = new AdomdConnection())
            {
                // Определяем строку подключения: указываем экземпляр Analysis Services и необходимую БД
                mdConn.ConnectionString = "provider=msolap;Data Source=V1LGORPC\\ASMAIN;initial catalog=AdventureWorksDW2014Multidimensional-EE;";

                // Открываем соединение
                mdConn.Open();

                // Создаем список кубов
                List<Cube> cubeList = new List<Cube>();

                // Проходим по всем кубам, полученным из Analysis Services
                foreach (CubeDef cube in mdConn.Cubes)
                {
                    // Пропускаем скрытые кубы
                    if (cube.Name.StartsWith('$')) continue;

                    // Создаем новый объект Cube
                    Cube newCube = new Cube(cube);

                    // Добавляем его в список кубов
                    cubeList.Add(newCube);
                }

                // Возвращаем JSON-представление куба с номером id
                return JsonConvert.SerializeObject(cubeList[id], Formatting.Indented);
            }
        }

        
    }
}