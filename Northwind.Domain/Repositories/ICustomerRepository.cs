using Northwind.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Domain.Repositories
{
    public interface ICustomerRepository
    {
        //trackChanges => feature untuk mendekteksi peribahan data diobject category
        Task<IEnumerable<Customer>> GetAllCustomer(bool trackChanges);

        //craete 1 record with this code
        Task<Customer> GetCustomerById(string customerId, bool trackChanges);
        void insert(Customer customer);
        void edit(Customer customer);
        void remove(Customer customer);

    }
}
