using Microsoft.EntityFrameworkCore;
using Northwind.Domain.Dto;
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
                .Include(od=>od.OrderDetails)
                .ToListAsync();
        }

        public async Task<Product> GetProductById(int ProductId, bool trackChanges)
        {
            return await FindByCondition(p => p.ProductId.Equals(ProductId), trackChanges)
                    .Include(c =>c.Category)
                    .Include(s => s.Supplier)
                    .Include(Od =>Od.OrderDetails)
                    .SingleOrDefaultAsync();

            //return await FindByCondition(c => c.CategoryId.Equals(ProductId), trackChanges).SingleOrDefaultAsync();
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
            var products = await _dbContext.Products
                            .Where(x => x.ProductPhotos
                            .Any(y => y.PhotoProductId == x.ProductId))
                            .Include(p => p.ProductPhotos)
                            .ToListAsync();
            return products;
                


            /*base in query method
             * var test = from p in _dbContext.Products
                       where p.ProductPhotos.Any(x => x.PhotoProductId == p.ProductId)
                       select p;


            var products = await FindAll(trackChanges)
                .Include(x => x.ProductPhotos.SingleOrDefault())
                .ToListAsync();
            return products;
            */
        }

        public async Task<Product> GetProductOrderOnSalesById(int productId, bool trackChanges)
        {
            var products = await FindByCondition(x => x.ProductId.Equals(productId), trackChanges)
               .Where(y => y.ProductPhotos.Any(p => p.PhotoProductId == productId))
               .Include(c => c.Category)
               .Include(s => s.Supplier)
               .Include(a => a.ProductPhotos)
               .SingleOrDefaultAsync();
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

        public async Task<Product> GetProductPhotoById(int ProductId, bool trackChanges)
        {
            var products = await FindByCondition(p=>p.ProductId.Equals(ProductId),trackChanges)
                            .Where(x => x.ProductPhotos
                            .Any(y => y.PhotoProductId == ProductId))
                            .Include(p => p.ProductPhotos)
                            .Include(c=>c.Category)
                            .Include(s => s.Supplier)
                            .Include(od => od.OrderDetails)
                            .SingleOrDefaultAsync();
            return products;
        }

        public  IEnumerable<TotalProductByCategory> GetTotalProductByCategory()
        {
            var rawSql =  _dbContext.TotalProductByCategorySQl
                .FromSqlRaw("select c.CategoryName, COUNT(p.productId) TotalProduct " +
                 "from Products p join Categories c on p.CategoryID = c.CategoryID " +
                 "group by c.CategoryName")
                .Select(x => new TotalProductByCategory
                {
                    CategoryName = x.CategoryName,
                    TotalProduct = x.TotalProduct
                })
                .OrderBy(x=>x.TotalProduct)
                .ToList();
            return rawSql;
        }
       // gausah pakai service

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
