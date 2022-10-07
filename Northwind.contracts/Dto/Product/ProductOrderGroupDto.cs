﻿using Northwind.Contracts.Dto.Category;
using Northwind.Contracts.Dto.Orders;
using Northwind.Contracts.Dto.OrderDetail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Contracts.Dto.Product
{
    public class ProductOrderGroupDto
    {
        public ProductDto productDto { get; set; }
        public OrderForCreateDto orderForCreateDto { get; set; }
        public OrderDetailForCreateDto orderDetailForCreateDto { get; set; }
    }
}
