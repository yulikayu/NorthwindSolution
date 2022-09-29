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
    internal class ProductPhotoRepository : RepositoryBase<ProductPhoto>, IProductPhotoRepository
    {
        public ProductPhotoRepository(NorthwindContext dbContext) : base(dbContext)
        {
        }

        public void edit(ProductPhoto productPhoto)
        {
            Update(productPhoto);
        }

        public async Task<IEnumerable<ProductPhoto>> GetAllProductPhoto(bool trackChanges)
        {
            return await FindAll(trackChanges)
                .OrderBy(p => p.PhotoId)
                .Include(s => s.PhotoProduct)
                .ToListAsync();
        }

       

        public async Task<ProductPhoto> GetProductPhotoById(int ProductPhotoId, bool trackChanges)
        {
            return await FindByCondition(p => p.PhotoId.Equals(ProductPhotoId), trackChanges)
                .Include(s => s.PhotoProduct)
                .SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<ProductPhoto>> GetProductPhotoPaged(int pageIndex, int pageSize, bool trackChanges)
        {
            return await FindAll(trackChanges)
                .OrderBy(c => c.PhotoProductId)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize).ToListAsync();
        }

        public void insert(ProductPhoto productPhoto)
        {
            Create(productPhoto);
        }

        public void remove(ProductPhoto productPhoto)
        {
            remove(productPhoto);
        }
    }
}
