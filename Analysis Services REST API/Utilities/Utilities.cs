using Analysis_Services_REST_API.Models;
using Microsoft.AnalysisServices.AdomdClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Analysis_Services_REST_API.Utilities
{
    public class Utilities
    {
        public static List<CubeDef> GetRealCubeList(CubeCollection cubeCollection)
        {
            List<CubeDef> realCubeList = new List<CubeDef>();
            foreach (CubeDef cube in cubeCollection)
            {
                if (cube.Name.StartsWith('$')) continue;
                realCubeList.Add(cube);
            }

            return realCubeList;
        }

        public static String BuildMDXRequest(RequestData requestData)
        {
            String request = "SELECT {";

            List<RequestDimension> rowDimensions = requestData.RowDimensionList;
            List<RequestDimension> columnDimensions = requestData.ColumnDimensionList;

            String rowMemberList = BuildMemberList(rowDimensions);
            String columnMemberList = BuildMemberList(columnDimensions);

            request += rowMemberList + "} ON ROWS,";
            request += "{" + columnMemberList + "} ON COLUMNS FROM " + WrapIntoSquareBrackets(requestData.CubeName);
            return request;
        }

        private static String BuildMemberList(List<RequestDimension> dimensions)
        {
            String memberList = "";
            foreach (RequestDimension dimension in dimensions)
            {
                foreach (RequestHierarchy hierarchy in dimension.HierarchyList)
                {
                    foreach (RequestLevel level in hierarchy.LevelDataList)
                    {
                        foreach (String member in level.MemberList)
                        {
                            memberList += GetMemberPath(dimension.Name, hierarchy.Name, level.Name, member) + ",";
                        }
                    }
                }
            }
            memberList = memberList.Remove(memberList.Length - 1);
            return memberList;
        }

        private static String GetMemberPath(String dimension, String hierarchy, String level, String member)
        {
            String wDim = WrapIntoSquareBrackets(dimension);
            String wHier = WrapIntoSquareBrackets(hierarchy);
            String wLvl = WrapIntoSquareBrackets(level);
            String wMem = WrapIntoSquareBrackets(member);

            return WrapIntoCircleBrackets(wDim + "." + wHier + "." + wLvl + "." + wMem);
        }

        private static String WrapIntoSquareBrackets(String str)
        {
            str = str.Insert(0, "[");
            str = str.Insert(str.Length, "]");
            return str;
        }

        private static String WrapIntoCircleBrackets(String str)
        {
            str = str.Insert(0, "(");
            str = str.Insert(str.Length, ")");
            return str;
        }
    }
}
