using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Northwind.Domain.Models;
using Northwind.Persistence;
using Northwind.Services.Abstraction;

namespace Northwind.Web.Controllers
{
    public class OrdersController : Controller
    {
        private IServiceManager _context;

        public OrdersController(IServiceManager context)
        {
            _context = context;
        }



        // GET: Orders
        public async Task<IActionResult> Index()
        {
            var northwindContext = _context.OrderService.GetAllOrder(false);
            return View(northwindContext);
        }

        // GET: Orders/Details/5
        public ActionResult Details(int? id)
        {
            

            return View();
        }

        // GET: Orders/Create
        public IActionResult Create()
        {
           
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(IFormCollection collection)
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

        // GET: Orders/Edit/5
        public IActionResult Edit(int id)
        {
            
            return View();
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.OrderService.GetOrderById((int)id, true);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
           
            return View();
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id,IFormCollection collection)
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
