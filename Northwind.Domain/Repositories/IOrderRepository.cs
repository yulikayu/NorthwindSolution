using Northwind.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Domain.Repositories
{
    public interface IOrderRepository
    {
        //trackChanges => feature untuk mendekteksi peribahan data diobject category
        Task<IEnumerable<Order>> GetAllOrder(bool trackChanges);

        //craete 1 record with this code
        Task<Order> GetOrderById(int OrderId, bool trackChanges);
        Task<IEnumerable<Order>> GetOrderPaged(int pageIndex, int pageSize, bool trackChanges);
        void insert(Order order);
        void edit(Order order);
        void remove(Order order);
    }
}
