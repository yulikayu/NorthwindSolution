using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Northwind.Domain.Models;
using Northwind.Persistence;

namespace Northwind.Web.Controllers
{
    public class ProductPhotoesServiceController : Controller
    {
        private readonly NorthwindContext _context;

        public ProductPhotoesServiceController(NorthwindContext context)
        {
            _context = context;
        }

        // GET: ProductPhotoesService
        public async Task<IActionResult> Index()
        {
            var northwindContext = _context.ProductPhotos.Include(p => p.PhotoProduct);
            return View(await northwindContext.ToListAsync());
        }

        // GET: ProductPhotoesService/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productPhoto = await _context.ProductPhotos
                .Include(p => p.PhotoProduct)
                .FirstOrDefaultAsync(m => m.PhotoId == id);
            if (productPhoto == null)
            {
                return NotFound();
            }

            return View(productPhoto);
        }

        // GET: ProductPhotoesService/Create
        public IActionResult Create()
        {
            ViewData["PhotoProductId"] = new SelectList(_context.Products, "ProductId", "ProductName");
            return View();
        }

        // POST: ProductPhotoesService/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PhotoId,PhotoFilename,PhotoFileSize,PhotoFileType,PhotoProductId,PhotoPrimary")] ProductPhoto productPhoto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(productPhoto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PhotoProductId"] = new SelectList(_context.Products, "ProductId", "ProductName", productPhoto.PhotoProductId);
            return View(productPhoto);
        }

        // GET: ProductPhotoesService/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productPhoto = await _context.ProductPhotos.FindAsync(id);
            if (productPhoto == null)
            {
                return NotFound();
            }
            ViewData["PhotoProductId"] = new SelectList(_context.Products, "ProductId", "ProductName", productPhoto.PhotoProductId);
            return View(productPhoto);
        }

        // POST: ProductPhotoesService/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PhotoId,PhotoFilename,PhotoFileSize,PhotoFileType,PhotoProductId,PhotoPrimary")] ProductPhoto productPhoto)
        {
            if (id != productPhoto.PhotoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productPhoto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductPhotoExists(productPhoto.PhotoId))
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
            ViewData["PhotoProductId"] = new SelectList(_context.Products, "ProductId", "ProductName", productPhoto.PhotoProductId);
            return View(productPhoto);
        }

        // GET: ProductPhotoesService/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productPhoto = await _context.ProductPhotos
                .Include(p => p.PhotoProduct)
                .FirstOrDefaultAsync(m => m.PhotoId == id);
            if (productPhoto == null)
            {
                return NotFound();
            }

            return View(productPhoto);
        }

        // POST: ProductPhotoesService/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productPhoto = await _context.ProductPhotos.FindAsync(id);
            _context.ProductPhotos.Remove(productPhoto);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductPhotoExists(int id)
        {
            return _context.ProductPhotos.Any(e => e.PhotoId == id);
        }
    }
}
