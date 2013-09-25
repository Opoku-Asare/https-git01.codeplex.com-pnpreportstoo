using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using pnpReportsToo.engine.menu;

namespace pnpREportsToo.App
{
    public class Processor
    {
        /// <summary>
        /// Gets the tree nodes from path.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <returns></returns>
        public static List<TreeNode> GetTreeNodesFromPath(string filePath)
        {
            var categories = Category.FromFile(filePath);
            var treeNodes = new List<TreeNode>();
            foreach (var category in categories)
            {
                var categoryNode = new TreeNode { Name = category.name, Text = category.name };
                if (category.items.Any())
                {
                    foreach (var item in category.items)
                    {
                        var itemNode = new TreeNode { Name = item.name, Text = item.name, ToolTipText = item.description, Tag = item };
                        categoryNode.Nodes.Add(itemNode);
                    }
                }
                treeNodes.Add(categoryNode);
            }
            return treeNodes;
        }



    }
}
