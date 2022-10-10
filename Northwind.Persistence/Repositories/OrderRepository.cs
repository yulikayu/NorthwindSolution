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

        public async Task<Order> FilterCustId(string custId, bool trackChanges)
        {
            return await FindByCondition(x => x.CustomerId.Equals(custId), trackChanges)
                 .Where(a => a.CustomerId == custId && a.ShippedDate == null)
                 .Include(c => c.Customer)
                 .Include(e => e.Employee)
                 .Include(od => od.OrderDetails)
                 .SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<Order>> GetAllOrder(bool trackChanges)
        {
            return await FindAll(trackChanges)
                 .OrderBy(x => x.OrderId)
                 .Include(c => c.Customer)
                 .Include(e => e.Employee)
                 .Include(od => od.OrderDetails)
                 .ToListAsync();
        }

        public async Task<Order> GetOrderById(int OrderId, bool trackChanges)
        {
            return await FindByCondition(x => x.OrderId.Equals(OrderId), trackChanges)
                .Include(c => c.Customer)
                .Include(e => e.Employee)
                .Include(od => od.OrderDetails)
                .SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<Order>> GetOrderPaged(int pageIndex, int pageSize, bool trackChanges)
        {
            return await FindAll(trackChanges)
                .OrderBy(c => c.OrderId)
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
