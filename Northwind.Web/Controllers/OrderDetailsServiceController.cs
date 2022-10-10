using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Northwind.Contracts.Dto.OrderDetail;
using Northwind.Domain.Models;
using Northwind.Persistence;
using Northwind.Services.Abstraction;

namespace Northwind.Web.Controllers
{
    public class OrderDetailsServiceController : Controller
    {
      

        private IServiceManager _context;

        public OrderDetailsServiceController(IServiceManager context)
        {
            _context = context;
        }

        public async Task<IActionResult> CartItem()
        {
            var custId = "ZZZZ";
            var cartItem=await _context.OrderDetailService.GetAllCartItem(custId,false);
            return View(cartItem);

            
        }


        // GET: OrderDetailsService
        public async Task<IActionResult> Index()
        {
            var orderDetail = _context.OrderDetailService.GetAllOrderDetail(false);
            return View(orderDetail);
        }

        // GET: OrderDetailsService/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderDetail = await _context.OrderDetailService.GetOrderDetailById
                ((int)id, false);
            return View(orderDetail);
            if (orderDetail == null)
            {
                return NotFound();
            }

            return View(orderDetail);
        }

        // GET: OrderDetailsService/Create
        public async Task<IActionResult> Create()
        {
            var allProduct = await _context.ProductService.GetAllProduct(false);
            var allOrder = await _context.OrderService.GetAllOrder(false);
            ViewData["OrderId"] = new SelectList(allOrder ,"OrderId", "OrderId");
            ViewData["ProductId"] = new SelectList(allProduct, "ProductId", "ProductName");
            return View();
        }

        // POST: OrderDetailsService/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderId,ProductId,UnitPrice,Quantity,Discount")] OrderDetailForCreateDto orderDetail)
        {
            if (ModelState.IsValid)
            {
                _context.OrderDetailService.Insert(orderDetail);
                return RedirectToAction(nameof(Index));
            }
            var allProduct = await _context.ProductService.GetAllProduct(false);
            var allOrder = await _context.OrderService.GetAllOrder(false);
            ViewData["OrderId"] = new SelectList(allOrder, "OrderId", "OrderId");
            ViewData["ProductId"] = new SelectList(allProduct, "ProductId", "ProductName");
            return View(orderDetail);
        }

        // GET: OrderDetailsService/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderDetail = await _context.OrderDetailService.GetOrderDetailById((int)id,false);
            if (orderDetail == null)
            {
                return NotFound();
            }
            var allProduct = await _context.ProductService.GetAllProduct(false);
            var allOrder = await _context.OrderService.GetAllOrder(false);
            ViewData["OrderId"] = new SelectList(allOrder, "OrderId", "OrderId");
            ViewData["ProductId"] = new SelectList(allProduct, "ProductId", "ProductName");
            return View(orderDetail);
        }

        // POST: OrderDetailsService/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderId,ProductId,UnitPrice,Quantity,Discount")] OrderDetailDto orderDetail)
        {
            if (id != orderDetail.OrderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.OrderDetailService.Edit(orderDetail);
                }
                catch (DbUpdateConcurrencyException)
                {
                    
                        throw;
                    
                }
                
            }
            var allProduct = await _context.ProductService.GetAllProduct(false);
            var allOrder = await _context.OrderService.GetAllOrder(false);
            ViewData["OrderId"] = new SelectList(allOrder, "OrderId", "OrderId");
            ViewData["ProductId"] = new SelectList(allProduct, "ProductId", "ProductName"); return View(orderDetail);
        }

        // GET: OrderDetailsService/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderDetail = await _context.OrderDetailService.GetOrderDetailById((int)id, false);
            if (orderDetail == null)
            {
                return NotFound();
            }

            return View(orderDetail);
        }

        // POST: OrderDetailsService/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var orderDetail = await _context.OrderDetailService.GetOrderDetailById((int)id, false);
            _context.OrderDetailService.Remove(orderDetail);
            return RedirectToAction(nameof(Index));
        }

       
    }
}
