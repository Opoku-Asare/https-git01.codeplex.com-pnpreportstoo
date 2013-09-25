using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace reporting.Reports
{
   public class DataSet
    {
       public DataSourceType dataSourceType { get; set; }
       public Query query { get; set; }
       public string name { get; set; }
    }
}
