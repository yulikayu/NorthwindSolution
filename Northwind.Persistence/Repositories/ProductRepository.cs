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
    internal class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        public ProductRepository(NorthwindContext dbContext) : base(dbContext)
        {
        }

        public void edit(Product product)
        {
            Update(product); 
        }

        public  async Task<IEnumerable<Product>> GetAllProduct(bool trackChanges)
        {
            return await FindAll(trackChanges)
                .OrderBy(c => c.ProductId)
                .Include(c => c.Category)
                .ToListAsync();
        }

        public async Task<Product> GetProductById(int ProductId, bool trackChanges)
        {
            return await FindByCondition(c => c.CategoryId.Equals(ProductId), trackChanges).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<Product>> GetProductIdandPaged(int ProductId, int pageIndex, int pageSize, bool trackChanges)
        {
            return await FindAll(trackChanges)
                .OrderBy(p => p.ProductId)
                .Include(c => c.Category)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductOnSales(bool trackChanges)
        {
            var products = await FindAll(trackChanges)
                .Include(x => x.ProductPhotos.SingleOrDefault())
                .ToListAsync();
            return products;
        }

        public async Task<IEnumerable<Product>> GetProductPaged(int pageIndex, int pageSize, bool trackChanges)
        {
            return await FindAll(trackChanges)
                .OrderBy(p=>p.ProductId)
                .Include(c=>c.Category)
                .Skip((pageIndex -1)*pageSize)
                .Take(pageSize).ToListAsync();


        }

        public void insert(Product product)
        {
            Create(product);
        }

        public void remove(Product product)
        {
            Delete(product);
        }
    }
}
