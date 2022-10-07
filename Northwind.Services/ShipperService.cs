using AutoMapper;
using Northwind.Contracts.Dto.Shipper;
using Northwind.Domain.Base;
using Northwind.Domain.Models;
using Northwind.Services.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Services
{
    public class ShipperService : IShipperService
    {
        private IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public ShipperService(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public void edit(ShipperDto shippers)
        {
            var edit = _mapper.Map<Shipper>(shippers);
            _repositoryManager.ShipperRepository.edit(edit);
            _repositoryManager.Save();
        }

        public async Task<IEnumerable<ShipperDto>> GetAllShippers(bool trackChanges)
        {
            var supplierModel = await _repositoryManager.ShipperRepository.GetAllShippers(trackChanges);
            var supplierDto = _mapper.Map<IEnumerable<ShipperDto>>(supplierModel);
            return supplierDto;
        }

        public async Task<ShipperDto> GetShippersById(int ShipperId, bool trackChanges)
        {
            var supplierModel = await _repositoryManager.ShipperRepository.GetShippersById(ShipperId, trackChanges);
            var supplierDto = _mapper.Map<ShipperDto>(supplierModel);
            return supplierDto;
        }

        public Task<IEnumerable<ShipperDto>> GetShippersPaged(int pageIndex, int pageSize, bool trackChanges)
        {
            throw new NotImplementedException();
        }

        public void insert(ShipperForCreateDto shipperForCreateDto)
        {
            var edit = _mapper.Map<Shipper>(shipperForCreateDto);
            _repositoryManager.ShipperRepository.insert(edit);
            _repositoryManager.Save();
        }

        public void remove(ShipperDto shippers)
        {
            var remove = _mapper.Map<Shipper>(shippers);
            _repositoryManager.ShipperRepository.remove(remove);
            _repositoryManager.Save();
        }
    }
}
