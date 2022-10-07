using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Northwind.Contracts.Dto.Category;
using Northwind.Contracts.Dto.OrderDetail;
using Northwind.Contracts.Dto.Orders;
using Northwind.Contracts.Dto.Product;
using Northwind.Services.Abstraction;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Northwind.Web.Controllers
{
    public class ProductOnsaleController : Controller
    {
        private IServiceManager _serviceManager;
        private readonly IUtilityService _utilityService;


        public ProductOnsaleController(IServiceManager serviceManager, IUtilityService utilityService)
        {
            _serviceManager = serviceManager;
            _utilityService = utilityService;
        }

        // GET: ProductOnsaleController
        public async Task<ActionResult> Index()
        {
            var productOnsales = await _serviceManager.
                ProductService.GetProductOnSales(false);

            return View(productOnsales);
           
        }
        [HttpPost]
        public async Task<IActionResult> CreateOrder(ProductDto productDto)
        {
            if (ModelState.IsValid)
            {
                var products = productDto;
                var order = new OrderForCreateDto
                {
                    OrderDate = DateTime.Now,
                    RequiredDate = DateTime.Now.AddDays(3)
                };
                var orderDetail = new OrderDetailForCreateDto
                {
                    ProductId = products.ProductId,
                    UnitPrice = (decimal)products.UnitPrice,
                    Quantity = 1,
                    Discount = 0
                };
                _serviceManager.ProductService.CreateOrder(order, orderDetail);
                return RedirectToAction(nameof(Index));
            }

            return View(productDto);
        }

        // GET: ProductOnSale/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var product = await _serviceManager.ProductService.GetProductPhotoById((int)id, false);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }
        public async Task<ActionResult> Cart(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var product = await _serviceManager.ProductService.GetProductPhotoById((int)id, false);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        public async Task<ActionResult> Detailes(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _serviceManager.ProductService.GetProductPhotoById((int)id, false);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: ProductOnsaleController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductOnsaleController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
       

        // GET: ProductOnsaleController/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            /*var supplier = await _context.Suppliers.FindAsync(id);*/
            var supplier = await _serviceManager.SupplierService.GetSupplierById((int)id, true);
            if (supplier == null)
            {
                return NotFound();
            }
            return View(supplier);
        }
        // POST: ProductOnsaleController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductOnsaleController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ProductOnsaleController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
