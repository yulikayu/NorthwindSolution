using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Northwind.Services.Abstraction;
using System.Threading.Tasks;

namespace Northwind.Web.Controllers
{
    public class ProductOnsaleController : Controller
    {
        private IServiceManager _serviceManager;

        public ProductOnsaleController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        // GET: ProductOnsaleController
        public async Task<ActionResult> Index()
        {
            var productOnsales = await _serviceManager.
                ProductService.GetProductOnSales(false);

            return View(productOnsales);
           
        }

        // GET: ProductOnsaleController/Details/5
        public async Task<ActionResult> Details(int id)
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
