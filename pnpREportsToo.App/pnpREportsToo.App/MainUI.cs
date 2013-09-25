using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using Newtonsoft.Json;
using pnpReportsToo.engine.menu;
using reporting.Reports;

namespace pnpREportsToo.App
{
    public partial class MainUI : Form

    {
        private String _connectinoString;
        public MainUI()
        {
            InitializeComponent();
        }

        private void MainUI_Load(object sender, EventArgs e)
        {
            LoadTreeMenu();
            var settings = JsonConvert.DeserializeAnonymousType(File.ReadAllText("settings.json"),new {connectionString=""});
            _connectinoString =(string) settings.connectionString;
            //this.reportViewer.RefreshReport();
        }

        /// <summary>
        /// Loads the tree menues.
        /// </summary>
        private void LoadTreeMenu()
        {
            menuTreeView.Nodes.Clear();
            var treeNodes = Processor.GetTreeNodesFromPath("Reports/reports.json");
            menuTreeView.Nodes.AddRange(treeNodes.ToArray());
            menuTreeView.ExpandAll();
            menuTreeView.SelectedNode = null;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (reportViewer.LocalReport.ReportPath == null)
            {
                MessageBox.Show("Please select a report to generate in the report menu by double clicking");
                return;
            }
            GenerateReport(reportViewer.LocalReport.ReportPath);
        }

        private void menuTreeView_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (menuTreeView.SelectedNode.Tag != null)
            {
                var item = (Item)menuTreeView.SelectedNode.Tag;
                GenerateReport(item.path);
            }
        }

        public void GenerateReport(string reportPath = null)
        {
            try
            {
                //Note that this reportPath is relative to build Path
                var dataExtrator = new DataExtrator();
                var dataSources = dataExtrator.GetReportDataSources(reportPath);
                var dataSets = dataExtrator.GetReportDataSets(reportPath);
                if (reportPath != null)
                    reportViewer.LocalReport.ReportPath = reportPath;

                foreach (var dataSet in dataSets)
                {
                    var dataSource = dataSources[dataSet.query.datasourceName];
                    if (dataSet.dataSourceType == DataSourceType.DataBase)
                    {

                        var data = dataExtrator.GetDataFromDataSource(dataSet.query,_connectinoString);
                        reportViewer.LocalReport.DataSources.Add(new ReportDataSource(dataSet.name, data));
                    }
                    else if (dataSet.dataSourceType == DataSourceType.ObjectDataSource)
                    {
                        //todo find out how to load a c# funtion from file and execute at runtime. for now, just using this stab
                        var data = dataExtrator.GetDataFromDataSource(() => new DataTable());
                        reportViewer.LocalReport.DataSources.Add(new ReportDataSource(dataSet.name, data));
                    }
                }


                reportViewer.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void menuTreeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            //if(menuTreeView.SelectedNode.Tag==null&&!menuTreeView.SelectedNode.IsExpanded) {
            //    menuTreeView.SelectedNode.Expand();
            //}
        }
    }
}
