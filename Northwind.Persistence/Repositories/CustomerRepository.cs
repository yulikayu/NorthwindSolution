using Northwind.Domain.Repositories;
using Northwind.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Northwind.Persistence.Base;
using Microsoft.EntityFrameworkCore;

namespace Northwind.Persistence.Repositories
{
    internal class CustomerRepository : RepositoryBase<Customer>, ICustomerRepository
    {
        public CustomerRepository(NorthwindContext dbContext) : base(dbContext)
        {
        }

        public void edit(Customer customer)
        {
            Update(customer);
        }
        public void Edit(Customer customer)
        {
            Update(customer);
        }

        public async Task<IEnumerable<Customer>> GetAllCustomer(bool trackChanges)
        {
            return await FindAll(trackChanges).OrderBy(c => c.CustomerId).ToListAsync();
        }

        public async Task<Customer> GetCustomerById(string customerid, bool trackChanges)
        {
            return await FindByCondition(c => c.CustomerId.Equals(customerid), trackChanges).SingleOrDefaultAsync();
        }
        public void insert(Customer customer)
        {
            Create(customer);
        }

        public void remove(Customer customer)
        {
            Delete(customer);
        }
    }
}
