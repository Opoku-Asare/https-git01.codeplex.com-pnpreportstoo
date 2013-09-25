using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace reporting.Reports
{
  public class Report
    {
      public Dictionary<string,DataSource> dataSources { get; set; }
      public IEnumerable<Query> queries { get; set; } 
    }
}
