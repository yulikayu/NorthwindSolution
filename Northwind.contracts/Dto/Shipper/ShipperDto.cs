using Northwind.Contracts.Dto.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Contracts.Dto.Shipper
{
    public class ShipperDto
    {
        public int ShipperId { get; set; }
        public string CompanyName { get; set; }
        public string Phone { get; set; }

        public virtual ICollection<OrderDto> Orders { get; set; }
    }
}
