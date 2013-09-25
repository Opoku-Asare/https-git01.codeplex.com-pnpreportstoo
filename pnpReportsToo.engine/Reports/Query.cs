using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace reporting.Reports
{
  public  class Query
    {
      public string datasourceName { get; set; }
      public string commandText { get; set; }
      public IEnumerable<QueryParameter> parameters { get; set; }
    }
}
