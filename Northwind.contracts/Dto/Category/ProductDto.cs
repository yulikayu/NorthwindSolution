using Microsoft.AspNetCore.Http;
using Northwind.Contracts.Dto.Product;
using Northwind.Contracts.Dto.Supplier;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Contracts.Dto.Category
{
    public class ProductDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int? SupplierId { get; set; }
        public int? CategoryId { get; set; }
        public string QuantityPerUnit { get; set; }
        public decimal? UnitPrice { get; set; }
        public short? UnitsInStock { get; set; }
        public short? UnitsOnOrder { get; set; }
        public short? ReorderLevel { get; set; }
        public bool Discontinued { get; set; }

        public string Photo { get; set; }

        public virtual CategoryDto Category { get; set; }
        public virtual SupplierDto Supplier { get; set; }

        public virtual ICollection<ProductPhotoDto> ProductPhotos { get; set; }
       

    }
}
