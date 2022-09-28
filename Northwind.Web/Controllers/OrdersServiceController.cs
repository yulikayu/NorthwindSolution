using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Northwind.Contracts.Dto.Orders;
using Northwind.Domain.Models;
using Northwind.Persistence;
using Northwind.Services.Abstraction;
using X.PagedList;

namespace Northwind.Web.Controllers
{
    public class OrdersServiceController : Controller
    {
        private readonly NorthwindContext _context;
        private readonly IServiceManager _serviceContext;

        public OrdersServiceController(NorthwindContext context, IServiceManager serviceContext)
        {
            _context = context;
            _serviceContext = serviceContext;
        }



        // GET: OrdersService
        public async Task<IActionResult> Index(string sortOrder, string searchString, int? page, int? fetchSize)
        {
            /*ViewData["ShortBy"] = String.IsNullOrEmpty(sortOrder) ? "OrderDate" : "";
            ViewData["IniApa"] = sortOrder == "ShipAddress" ? "ShipName" : "RequiredDate";
            ViewData["CurrentFilter"] = searchString;*/
            var pageIndex = page ?? 1;
            var pageSize = fetchSize ?? 5;
            
            var productDtos = await _serviceContext.OrderService.GetOrderPaged(pageIndex,pageSize,false);
            var totalRows = productDtos.Count();
            ViewData["CurrentFilter"] = searchString;
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = string.IsNullOrEmpty(sortOrder) ? "ShipAddress" : "";
            ViewData["PriceSortParm"] = string.IsNullOrEmpty(sortOrder) ? "ShipName" : "";
            ViewData["DataSortParm"] = sortOrder == "Cate" ? "ShipName" : "Cate";

            var datas = from s in _context.Orders
                        select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                datas = datas.Where(s => s.ShipAddress.Contains(searchString) ||
                                      s.ShipName.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "OrderDate":
                    datas=datas.OrderBy(x => x.ShipAddress);
                    break;
                case "RequiredDate":
                    datas=datas.OrderByDescending(s=>s.RequiredDate);
                    break;
                case "ShipName":
                    datas = datas.OrderBy(s => s.ShipName);
                    break;
                default:
                    datas=datas.OrderBy(s => s.RequiredDate);
                    break;
            }
            var produtDtosPaged =
                new StaticPagedList<OrderDto>(productDtos, pageIndex + 1, pageSize - (pageSize - 1), totalRows);

            /*var northwindContext = _context.Orders.Include(o => o.Customer).Include(o => o.Employee).Include(o => o.ShipViaNavigation);
            return View(await northwindContext.ToListAsync());*/
            return View(produtDtosPaged);
            /*return View(await datas.AsNoTracking().ToListAsync());*/
           /* var orderDto = await _serviceContext.OrderService.GetAllOrder(false);
            return View(orderDto);*/
        }

        // GET: OrdersService/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.Employee)
                .Include(o => o.ShipViaNavigation)
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: OrdersService/Create
        public IActionResult Create()
        {
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustomerId");
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "FirstName");
            ViewData["ShipVia"] = new SelectList(_context.Shippers, "ShipperId", "CompanyName");
            return View();
        }

        // POST: OrdersService/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderId,CustomerId,EmployeeId,OrderDate,RequiredDate,ShippedDate,ShipVia,Freight,ShipName,ShipAddress,ShipCity,ShipRegion,ShipPostalCode,ShipCountry")] Order order)
        {
            if (ModelState.IsValid)
            {
                _context.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustomerId", order.CustomerId);
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "FirstName", order.EmployeeId);
            ViewData["ShipVia"] = new SelectList(_context.Shippers, "ShipperId", "CompanyName", order.ShipVia);
            return View(order);
        }

        // GET: OrdersService/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustomerId", order.CustomerId);
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "FirstName", order.EmployeeId);
            ViewData["ShipVia"] = new SelectList(_context.Shippers, "ShipperId", "CompanyName", order.ShipVia);
            return View(order);
        }

        // POST: OrdersService/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderId,CustomerId,EmployeeId,OrderDate,RequiredDate,ShippedDate,ShipVia,Freight,ShipName,ShipAddress,ShipCity,ShipRegion,ShipPostalCode,ShipCountry")] Order order)
        {
            if (id != order.OrderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.OrderId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustomerId", order.CustomerId);
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "FirstName", order.EmployeeId);
            ViewData["ShipVia"] = new SelectList(_context.Shippers, "ShipperId", "CompanyName", order.ShipVia);
            return View(order);
        }

        // GET: OrdersService/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.Employee)
                .Include(o => o.ShipViaNavigation)
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: OrdersService/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.OrderId == id);
        }
    }
}
