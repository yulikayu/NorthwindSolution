using Northwind.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Domain.Repositories
{
    public interface IShipperRepository
    {
        //trackChanges => feature untuk mendekteksi peribahan data diobject category
        Task<IEnumerable<Shipper>> GetAllShippers(bool trackChanges);

        //craete 1 record with this code
        Task<Shipper> GetShippersById(int ShipperId, bool trackChanges);
        Task<IEnumerable<Shipper>> GetShippersPaged(int pageIndex, int pageSize, bool trackChanges);
        void insert(Shipper shippers);
        void edit(Shipper shippers);
        void remove(Shipper shippers);
    }
}
