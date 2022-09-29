using Northwind.Contracts.Dto.Category;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Contracts.Dto.Product
{
    public class ProductForCreatDto
    {
        [Display(Name="Product Name")]
        [Required]
        [StringLength(50,ErrorMessage ="Product name cannot be longger than 50")]
        public string ProductName { get; set; }
        [Display(Name = "Supplier")]
        public int? SupplierId { get; set; }
        [Display(Name = "Category")]
        public int? CategoryId { get; set; }
        public string QuantityPerUnit { get; set; }
        [Display(Name = "Price")]
        public decimal? UnitPrice { get; set; }
        public short? UnitsInStock { get; set; }
        public short? UnitsOnOrder { get; set; }
        public short? ReorderLevel { get; set; }
        public bool Discontinued { get; set; }

        public virtual CategoryDto CategoryDto { get; set; }
    }
}
