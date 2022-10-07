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
    internal class ShipperRepository : RepositoryBase<Shipper>, IShipperRepository
    {
        public ShipperRepository(NorthwindContext dbContext) : base(dbContext)
        {
        }

        public void edit(Shipper shippers)
        {
            Update(shippers);
        }

        public async Task<IEnumerable<Shipper>> GetAllShippers(bool trackChanges)
        {
            return await FindAll(trackChanges).OrderBy(c => c.ShipperId).ToListAsync();
        }

        public async Task<Shipper> GetShippersById(int ShipperId, bool trackChanges)
        {
            return await FindByCondition(c => c.ShipperId.Equals(ShipperId), trackChanges).SingleOrDefaultAsync();
        }

        public  Task<IEnumerable<Shipper>> GetShippersPaged(int pageIndex, int pageSize, bool trackChanges)
        {
            throw new NotImplementedException();
        }

        public void insert(Shipper shippers)
        {
            Create(shippers);
        }

        public void remove(Shipper shippers)
        {
            Delete(shippers);
        }
    }
}
