using Northwind.Contracts.Dto.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Services.Abstraction
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderDto>> GetAllOrder(bool trackChanges);

        //craete 1 record with this code
        Task<OrderDto> GetOrderById(int OrderId, bool trackChanges);
        Task<IEnumerable<OrderDto>> GetOrderPaged(int pageIndex, int pageSize, bool trackChanges);
        void edit(OrderDto orderDto);
        void remove(OrderDto orderDto);
    }
}
