using Microsoft.EntityFrameworkCore;
using Northwind.Domain.Models;
using Northwind.Domain.Repositories;
using Northwind.Persistence.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Persistence.Repositories
{
    internal class OrderRepository : RepositoryBase<Order>, IOrderRepository
    {
        public OrderRepository(NorthwindContext dbContext) : base(dbContext)
        {
        }

        public void edit(Order order)
        {
            edit(order);
        }

        public async Task<IEnumerable<Order>> GetAllOrder(bool trackChanges)
        {
            return await FindAll(trackChanges).OrderBy(c => c.OrderId).ToListAsync();
        }

        public async Task<Order> GetOrderById(int OrderId, bool trackChanges)
        {
            return await FindByCondition(c => c.OrderId.Equals(OrderId), trackChanges).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<Order>> GetOrderPaged(int pageIndex, int pageSize, bool trackChanges)
        {
            return await FindAll(trackChanges)
                .OrderBy(c => c.OrderId)
                .Include(c => c.CustomerId)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize).ToListAsync();
        }
         

        public void insert(Order order)
        {
            Create(order);
        }

        public void remove(Order order)
        {
           Delete(order);
        }
    }
}
