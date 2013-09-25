using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Linq;


namespace reporting.Reports
{
    public class DataExtrator
    {
        public Dictionary<string, DataSource> GetReportDataSources(string reportPath) {
            var doc = XDocument.Load(reportPath);
            var dataSources = (
                                  from element in doc.Descendants()
                                  where element.Name.LocalName == "DataSource"
                                  select element).ToList();

            var dataSourceDic = new Dictionary<string, DataSource>();

            foreach (var dataSource in dataSources) {
                var constring = dataSource.Descendants().FirstOrDefault(e => e.Name.LocalName == "ConnectString").Value;
                var name = dataSource.Attribute("Name").Value;
                dataSourceDic.Add(name, new DataSource(name, constring));
            }
            return dataSourceDic;
        }

        public Dictionary<string, ReportParameter> GetReportParameters(string reportPath) {
            var doc = XDocument.Load(reportPath);
            var reportParameters = new Dictionary<string, ReportParameter>();
            var paramters = (
                                from element in doc.Descendants()
                                where element.Name.LocalName == "ReportParameter"
                                select element).ToList();
            foreach (var paramter in paramters) {
                var reportParameter = new ReportParameter();
                reportParameter.name = paramter.Attribute("Name").Value;
                var paramType = paramter.Descendants().FirstOrDefault(x => x.Name.LocalName == "DataType").Value;
                if (paramType.Equals("Integer")) {
                    reportParameter.type = ReportParamerType.Integer;
                } else if (paramType.Equals("String")) {
                    reportParameter.type = ReportParamerType.Text;
                } else if (paramType.Equals("DateTime")) {
                    reportParameter.type = ReportParamerType.DateTime;
                } else if (paramType.Equals("Boolean")) {
                    reportParameter.type = ReportParamerType.Boolean;
                } else if (paramType.Equals("Float")) {
                    reportParameter.type = ReportParamerType.Float;
                } else {
                    reportParameter.type = ReportParamerType.Text;
                }
                reportParameters.Add(reportParameter.name, reportParameter);
            }
            return reportParameters;
        }

        public IEnumerable<DataSet> GetReportDataSets(string reportPath) {
            var doc = XDocument.Load(reportPath);
            var dataSets = (from elements in doc.Descendants()
                            where elements.Name.LocalName == "DataSet"
                            select elements).ToList();

            var dataSetList = new List<DataSet>();
            foreach (var dataset in dataSets) {
                var dataSetEntry = new DataSet();
                dataSetEntry.name = dataset.Attribute("Name").Value;
                var dataSourceType =
                    dataset.Descendants().FirstOrDefault(e => e.Name.LocalName == "ObjectDataSourceType");

                if (dataSourceType == null) {
                    dataSetEntry.dataSourceType = DataSourceType.DataBase;
                } else {
                    dataSetEntry.dataSourceType = DataSourceType.ObjectDataSource;
                }
                dataSetEntry.query = GetDataSetQuery(dataset);
                dataSetList.Add(dataSetEntry);
            }
            return dataSetList;
        }

        public Query GetDataSetQuery(XElement dataSetElement) {
            var queryElement = (from elements in dataSetElement.Descendants()
                                where elements.Name.LocalName == "Query"
                                select elements).FirstOrDefault();


            var first = queryElement.Descendants().FirstOrDefault(e => e.Name.LocalName == "CommandText");
            var commandText = first.Value;
            var dataSourceName =
                queryElement.Descendants().FirstOrDefault(e => e.Name.LocalName == "DataSourceName").Value;
            var query = new Query {
                                      commandText = commandText,
                                      datasourceName = dataSourceName,
                                  };


            var parameters = queryElement.Descendants().Where(e => e.Name.LocalName == "QueryParameter").ToList();
            query.parameters = GetQueryParameters(parameters);

            return query;
        }

        public IEnumerable<Query> GetDataSetQueries(string reportPath) {
            var doc = XDocument.Load(reportPath);
            var dataSets = (from elements in doc.Descendants()
                            where elements.Name.LocalName == "DataSet"
                            select elements).ToList();

            var queryList = new List<Query>();

            foreach (var dataset in dataSets) {
                var commandText = dataset.Descendants().FirstOrDefault(e => e.Name.LocalName == "CommandText").Value;
                var dataSourceName =
                    dataset.Descendants().FirstOrDefault(e => e.Name.LocalName == "DataSourceName").Value;

                var query = new Query {
                                          commandText = commandText,
                                          datasourceName = dataSourceName,
                                      };


                var parameters = dataset.Descendants().Where(e => e.Name.LocalName == "QueryParameter").ToList();
                query.parameters = GetQueryParameters(parameters);
                queryList.Add(query);
            }
            return queryList;
        }

        public IEnumerable<QueryParameter> GetQueryParameters(IEnumerable<XElement> parameters) {
            return (from p in parameters
                    let name = p.Attribute("Name").Value
                    let value = p.Descendants().FirstOrDefault(e => e.Name.LocalName == "Value").Value
                    select new QueryParameter
                    {
                                                    name = name,
                                                    value = value
                                                }).ToList();
        }

        public string GetConnectionString(string reportPath) {
            //var xmlText = File.ReadAllText(reportPath);
            var xDoc = XDocument.Load(reportPath);
            var connString = (from element in xDoc.Descendants()
                              where element.Name.LocalName == "ConnectString"
                              select element.Value).FirstOrDefault();
            return connString;
        }

        public string GetQuery(string reportPath) {
            var xDoc = XDocument.Load(reportPath);
            var query = (from element in xDoc.Descendants()
                         where element.Name.LocalName == "CommandText"
                         select element.Value).FirstOrDefault();
            return query;
        }

        public DataTable GetDataFromDataSource(Query query, DataSource dataSource) {
            using (var connection = new SqlConnection(dataSource.connectionString)) {
                var command = new SqlCommand(query.commandText, connection);
                //if(query.)
                connection.Open();
                var dataTable = new DataTable();
                var reader = command.ExecuteReader();
                dataTable.Load(reader, LoadOption.OverwriteChanges);
                return dataTable;
            }
        }

        public DataTable GetDataFromDataSource(string query, string connectionString) {
            using (var connection = new SqlConnection(connectionString)) {
                var command = new SqlCommand(query, connection);
                connection.Open();
                var dataTable = new DataTable();
                var reader = command.ExecuteReader();
                dataTable.Load(reader, LoadOption.OverwriteChanges);
                return dataTable;
            }
        }

        /// <summary>
        /// Gets the data by executing a Function .
        /// </summary>
        /// <param name="dataFnx">The data function.</param>
        /// <param name="code">The code.</param>
        /// <returns></returns>
        public DataTable GetDataFromDataSource(Func<DataTable> dataFnx) {
            return dataFnx();
        }

        public DataTable GetDataFromDataSource(Query query, string _connectinoString)
        {
            using (var connection = new SqlConnection(_connectinoString))
            {
                var command = new SqlCommand(query.commandText, connection);
                //if(query.)
                connection.Open();
                var dataTable = new DataTable();
                var reader = command.ExecuteReader();
                dataTable.Load(reader, LoadOption.OverwriteChanges);
                return dataTable;
            }
            throw new NotImplementedException();
        }
    }
}