using AutoMapper;
using Northwind.Contracts.Dto.Orders;
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
    public class OrderService : IOrderService
    {
        private IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public OrderService(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public void edit(OrderDto orderDto)
        {
            var orderModel = _mapper.Map<Order>(orderDto);
            _repositoryManager.OrderRepository.edit(orderModel);
            _repositoryManager.Save();
        }

        public async Task<IEnumerable<OrderDto>> GetAllOrder(bool trackChanges)
        {
            var OrderMDL = await _repositoryManager.OrderRepository.GetAllOrder(trackChanges);
            //source= ProductMDL,targer CategoryDto
            var OrderDto = _mapper.Map<IEnumerable<OrderDto>>(OrderMDL);
            return OrderDto;
        }

        public async Task<OrderDto> GetOrderById(int OrderId, bool trackChanges)
        {
            var OrderMDL = await _repositoryManager.OrderRepository.GetOrderById(OrderId,trackChanges);
            //source= ProductMDL,targer CategoryDto
            var OrderDto = _mapper.Map<OrderDto>(OrderMDL);
            return OrderDto;
        }

        public void remove(OrderDto orderDto)
        {
            var orderModel = _mapper.Map<Order>(orderDto);
            _repositoryManager.OrderRepository.remove(orderModel);
            _repositoryManager.Save();
        }

        public async Task<IEnumerable<OrderDto>> GetOrderPaged(int pageIndex, int pageSize, bool trackChanges)
        {
            var ProductMDL = await _repositoryManager.OrderRepository.GetOrderPaged(pageIndex, pageSize, trackChanges);
            //source= ProductMDL,targer CategoryDto
            var productDto = _mapper.Map<IEnumerable<OrderDto>>(ProductMDL);
            return productDto;
        }
    }
}
