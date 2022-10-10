using Northwind.Contracts.Dto.Category;
using Northwind.Contracts.Dto.OrderDetail;
using Northwind.Contracts.Dto.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Contracts.Dto.Product
{
    public class ProductOrdeForEditDto
    {
        public ProductDto productDto { get; set; }
        public OrderDto order { get; set; }
        public OrderDetailDto orderDetailDto { get; set; }
    }
}
