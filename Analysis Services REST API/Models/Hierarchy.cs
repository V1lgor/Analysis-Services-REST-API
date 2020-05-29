using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Analysis_Services_REST_API.Models
{
    public class Hierarchy
    {
        public Hierarchy() { }

        public Hierarchy(Microsoft.AnalysisServices.AdomdClient.Hierarchy serverHierarchy)
        {
            Name = serverHierarchy.Name;
            LevelList = new List<Models.Level>();

            foreach (Microsoft.AnalysisServices.AdomdClient.Level level in serverHierarchy.Levels)
            {
                Models.Level newLevel = new Models.Level(level);

                LevelList.Add(newLevel);
            }
        }
        public String Name { get; set; }
        public List<Level> LevelList { get; set; }
    }
}
