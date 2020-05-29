using Microsoft.AnalysisServices.AdomdClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Analysis_Services_REST_API.Models
{
    public class Level
    {
        // Конструктор по умолчанию
        public Level() { }

        // Конструктор, создающий объект на основе полученного с сервера объекта Level
        public Level(Microsoft.AnalysisServices.AdomdClient.Level serverLevel)
        {
            Name = serverLevel.Name;
            MemberCount = serverLevel.MemberCount;
            MemberNameList = new List<string>();
            int memberIndex = 0;
            foreach(Member member in serverLevel.GetMembers())
            {
                memberIndex++;
                MemberNameList.Add(member.Name);
                if (memberIndex > 100) break;
            }
        }
        // Название уровня
        public String Name { get; set; }

        // 
        public long MemberCount { get; set; }

        public List<String> MemberNameList;
    }
}
