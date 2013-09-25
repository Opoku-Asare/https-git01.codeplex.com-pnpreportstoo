using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace pnpReportsToo.engine.menu
{
    /// <summary>
    /// Represents a category or grouping of menu items
    /// </summary>
    public class Category
    {
        /// <summary>
        /// Gets or sets the title of the category.
        /// </summary>
        /// <value>
        /// The name of the category.
        /// </value>
        public string name { get; set; }
        /// <summary>
        /// Gets or sets the list items in this category.
        /// </summary>
        /// <value>
        /// The items.
        /// </value>
        public List<Item> items { get; set; }

        /// <summary>
        /// Read Categories from file.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        public static List<Category> FromFile(string path)
        {
            var context = File.ReadAllText(path);

            //read the json file and convert it to objects 
            // see documentation of JSON.Net
            var categories = JsonConvert.DeserializeObject<List<Category>>(context);
            return categories;
        }
    }
}