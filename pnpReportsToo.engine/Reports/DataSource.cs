using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace reporting.Reports
{
    public class DataSource
    {
        public DataSource(string name, string connectionString)
        {
            this.name = name;
            this.connectionString = connectionString;
        }
        public string name { get; set; }
        public string connectionString { get; set; }
    }
}
