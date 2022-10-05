using Northwind.Contracts.Dto.Shipper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Services.Abstraction
{
    public interface IShipperService
    {
        //trackChanges => feature untuk mendekteksi peribahan data diobject category
        Task<IEnumerable<ShipperDto>> GetAllShippers(bool trackChanges);

        //craete 1 record with this code
        Task<ShipperDto> GetShippersById(int ShipperId, bool trackChanges);
        Task<IEnumerable<ShipperDto>> GetShippersPaged(int pageIndex, int pageSize, bool trackChanges);
        void insert(ShipperForCreateDto shipperForCreateDto);
        void edit(ShipperDto shippers);
        void remove(ShipperDto shippers);
    }
}
