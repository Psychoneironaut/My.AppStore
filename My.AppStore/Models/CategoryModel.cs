using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace My.AppStore.Models
{
    public class CategoryModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public IEnumerable<ProductModel> Products { get; set; }
        public int ID { get; internal set; }
    }
}