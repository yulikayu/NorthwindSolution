using Northwind.Domain.Base;
using Northwind.Domain.Repositories;
using Northwind.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Persistence.Base
{
    public class RepositoryManager : IRepositoryManager
    {
        private NorthwindContext _dbContext;
        private ICategoryRepository _categoryRepository;
        private ICustomerRepository _customerRepository;
        private IProductRepository _productRepository;
        private IOrderRepository _orderRepository;
         
        public RepositoryManager(NorthwindContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ICustomerRepository CustomerRepository
        {
            get
            {
                if (_customerRepository == null)
                {
                    _customerRepository = new CustomerRepository(_dbContext);
                }
                return _customerRepository;
            }
        }
        public ICategoryRepository CategoryRepository { 
            get { 
                if(_categoryRepository == null)
                {
                    _categoryRepository = new CategoryRepository(_dbContext);
                }    
                return _categoryRepository;
            }
        }

        public IProductRepository ProductRepository
        {
            get
            {
                if (_productRepository == null)
                {
                    _productRepository = new ProductRepository(_dbContext);
                }
                return _productRepository;
            }
        }

        public IOrderRepository OrderRepository
        {
            get
            {
                if (_orderRepository == null)
                {
                    _orderRepository = new OrderRepository(_dbContext);
                }
                return _orderRepository;
            }
        }

        public void Save()=> _dbContext.SaveChanges();

        public async Task SaveAsync()=>await _dbContext.SaveChangesAsync();

    }
}
