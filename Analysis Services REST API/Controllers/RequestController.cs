using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Analysis_Services_REST_API.Models;
using Microsoft.AnalysisServices.AdomdClient;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Analysis_Services_REST_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestController : ControllerBase
    {
        [HttpPost]
        public string HandleRequest([FromBody] RequestData requestData)
        {
            String requestText = Utilities.Utilities.BuildMDXRequest(requestData);

            using(AdomdConnection mdConn = new AdomdConnection())
            {
                mdConn.ConnectionString = "provider=msolap;Data Source=V1LGORPC\\ASMAIN;initial catalog=AdventureWorksDW2014Multidimensional-EE;";
                mdConn.Open();

                AdomdCommand mdCommand = mdConn.CreateCommand();
                mdCommand.CommandText = requestText;  // << MDX Query
                CellSet cs;
                try
                {
                    // work with CellSet
                    cs = mdCommand.ExecuteCellSet();
                }
                catch(AdomdErrorResponseException e)
                {
                    return e.Message;
                }

                MdxResultDataSet resultDataSet = new MdxResultDataSet();
                
                // our method supports only 2-Axes CellSets
                if (cs.Axes.Count != 2) return "Request error: axes count > 2.";

                TupleCollection tuplesOnColumns = cs.Axes[0].Set.Tuples;
                TupleCollection tuplesOnRows = cs.Axes[1].Set.Tuples;

                
                for (int row = 0; row < tuplesOnRows.Count; row++)
                {
                    resultDataSet.RowNameList.Add(tuplesOnRows[row].Members[0].Caption);
                }

                for (int col = 0; col < tuplesOnColumns.Count; col++)
                {
                    resultDataSet.ColumnNameList.Add(tuplesOnColumns[col].Members[0].Caption);
                    resultDataSet.Cells.Add(new List<String>());

                    for (int row = 0; row < tuplesOnRows.Count; row++)
                    {
                        if (cs.Cells[col, row].Value == null)
                        {
                            resultDataSet.Cells[col].Add("null");
                        }
                        else
                        {
                            resultDataSet.Cells[col].Add(cs.Cells[col, row].Value.ToString());
                        }
                    }
                }
                return JsonConvert.SerializeObject(resultDataSet, Formatting.Indented);
                //return requestText;
            }

        }
    }
}