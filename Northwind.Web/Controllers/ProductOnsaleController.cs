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
        public async Task<IActionResult> CreateOrderr(ProductDto productDto)
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
        [HttpPost]
        public async Task<IActionResult> CreateOrder(ProductDto productDto)
        {
            if (ModelState.IsValid)
            {
                var products = productDto;
                var order = new OrderForCreateDto
                {
                    OrderDate = DateTime.Now,
                    RequiredDate = DateTime.Now.AddDays(3),
                    CustomerId = "ZZZZ"
                };
                var orders = await _serviceManager.OrderService.FilterCustId(order.CustomerId, false);
                if (orders == null)
                {
                    var createOrder = _serviceManager.OrderService.CreateOrderId(order);
                    var orderDetail = new OrderDetailForCreateDto
                    {
                        ProductId = products.ProductId,
                        OrderId = createOrder.OrderId,
                        UnitPrice = (decimal)products.UnitPrice,
                        Quantity = Convert.ToInt16(products.QuantityPerUnit),
                        Discount = 0
                    };
                    _serviceManager.OrderDetailService.Insert(orderDetail);
                    return RedirectToAction("Checkout",new {id=createOrder.OrderId});
                }
                //Create order lagi if product null
                else
                {
                    OrderDetailDto orderDetails = new OrderDetailDto();
                    orderDetails = await _serviceManager.OrderDetailService.GetOrderDetail(orders.OrderId, products.ProductId, false);
                    if (orders.ShippedDate == null)
                    {
                        var orderDetail = new OrderDetailForCreateDto
                        {
                            ProductId = products.ProductId,
                            OrderId = orders.OrderId,
                            UnitPrice = (decimal)products.UnitPrice * Convert.ToInt16(products.QuantityPerUnit),
                            Quantity = Convert.ToInt16(products.QuantityPerUnit),
                            Discount = 0
                        };

                        if (orderDetails != null)
                        {
                            //Melakukan edit jika product yang di order sama
                            if (orderDetails.ProductId == products.ProductId)
                            {
                                var newJumlah = Convert.ToInt16(products.QuantityPerUnit);
                                orderDetails.OrderId = orderDetails.OrderId;
                                orderDetails.ProductId = orderDetails.ProductId;
                                orderDetails.Quantity += newJumlah;
                                orderDetails.UnitPrice += (decimal)products.UnitPrice + newJumlah;
                                _serviceManager.OrderDetailService.Edit(orderDetails);
                                return RedirectToAction("CartItem", "OrderDetailsService", new { area = "" });
                            }
                        }
                        else
                        {
                            _serviceManager.OrderDetailService.Insert(orderDetail);
                            return RedirectToAction("CartItem", "OrderDetailsService", new { area = "" });
                        }

                        _serviceManager.OrderDetailService.Insert(orderDetail);
                        return RedirectToAction("CartItem", "OrderDetailsService", new { area = "" });
                    }

                    else
                    {
                        var creatOrder = _serviceManager.OrderService.CreateOrderId(order);
                        var orderdetail = new OrderDetailForCreateDto
                        {
                            ProductId=products.ProductId,
                            OrderId=creatOrder.OrderId,
                            UnitPrice=(decimal)products.UnitPrice,
                            Quantity= Convert.ToInt16(products.QuantityPerUnit),
                            Discount=0
                        };
                        _serviceManager.OrderDetailService.Insert(orderdetail);
                        return RedirectToAction("CartItem", "OrderDetailsService", new { area = "" });
                    }
                }
              
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
