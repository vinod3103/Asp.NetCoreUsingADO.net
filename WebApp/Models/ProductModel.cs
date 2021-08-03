using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class ProductModel
    {
        [Display(Name = "Product Id")]
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        [Display(Name = "Unit Price")]
        public decimal? UnitPrice { get; set; }
        public int CategoryId { get; set; }

        [Display(Name = "Category Name")]
        public string CategoryName { get; set; }
    }
}
