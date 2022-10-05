using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Northwind.Contracts.Dto.Product;
using Northwind.Domain.Models;
using Northwind.Persistence;
using Northwind.Services.Abstraction;
using X.PagedList;

namespace Northwind.Web.Controllers
{
    public class ProductsDtoServiceController : Controller
    {
        private readonly NorthwindContext _context;
        private readonly IServiceManager _serviceContext;
        private readonly IUtilityService _utilityService;

        public ProductsDtoServiceController(NorthwindContext context, IServiceManager serviceContext)
        {
            _context = context;
            _serviceContext = serviceContext;
        }

        public List<SelectListItem> GetSupplierDrowdown()
        {
            return _context.Suppliers.Select(supplier => new SelectListItem()
            {
                Value = supplier.SupplierId.ToString(),
                Text = supplier.CompanyName
            }).ToList();
        }

        public List<SelectListItem> GetCategoryDropdown()
        {
            return _context.Categories.Select(supplier => new SelectListItem()
            {
                Value = supplier.CategoryId.ToString(),
                Text = supplier.CategoryName
            }).ToList();
        }

        // GET: ProductsDtoService
        public async Task<IActionResult> Index(string searchString, string currentFilter,
            int? page)
        {
            /*  var northwindContext = _context.Products.Include(p => p.Category).Include(p => p.Supplier);*/
            var pageNumber =page?? 1;
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter=searchString;

            var productDto = await _serviceContext.ProductService.GetAllProduct(false);

            if (!String.IsNullOrEmpty(searchString))
            {
                productDto = productDto
                    .Where(p => p.ProductName.ToLower().Contains(searchString));
            }

            return View(productDto.ToPagedList(pageNumber,5));
        }

        // GET: ProductsDtoService/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Supplier)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: ProductsDtoService/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName");
            ViewData["SupplierId"] = new SelectList(_context.Suppliers, "SupplierId", "CompanyName");
            return View();
        }

        // POST: ProductsDtoService/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,ProductName,SupplierId,CategoryId,QuantityPerUnit,UnitPrice,UnitsInStock,UnitsOnOrder,ReorderLevel,Discontinued")] Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName", product.CategoryId);
            ViewData["SupplierId"] = new SelectList(_context.Suppliers, "SupplierId", "CompanyName", product.SupplierId);
            return View(product);
        }
        [HttpPost]
        public async Task<IActionResult> CreateProductPhoto(ProductPhotoGroupDto productPhotoGroupDto)
        {

            if (ModelState.IsValid)
            {
                var productPhotoGroup = productPhotoGroupDto;
                var listPhoto = new List<ProductPhotoForCreateDto>();
                /* var photo1 = _utilityService.UploadSingleFile(productPhotoGroup.Photo1);
                 var photo2 = _utilityService.UploadSingleFile(productPhotoGroup.Photo2);
                 var photo3 = _utilityService.UploadSingleFile(productPhotoGroup.Photo3);*/
                //var productPhotoGroup = productPhotoGroupDto;
                foreach (var item in productPhotoGroup.AllPhoto)
                {
                    var fileName = _utilityService.UploadSingleFile(item);
                    var convertSize = (Int16)item.Length;
                    var photo = new ProductPhotoForCreateDto
                    {
                        PhotoFilename = fileName,
                        PhotoFileSize = (byte)convertSize,
                        PhotoFileType = item.ContentType
                    };
                    listPhoto.Add(photo);
                }
                _serviceContext.ProductService.CreateProductManyPhoto
                    (productPhotoGroup.ProductForCreateDto, listPhoto);
                return RedirectToAction(nameof(Index));


            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName");
            ViewData["SupplierId"] = new SelectList(_context.Suppliers, "SupplierId", "CompanyName");
            return View("Create");
        }


        // GET: ProductsDtoService/Edit/5
        public async Task<IActionResult> Editt(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName", product.CategoryId);
            ViewData["SupplierId"] = new SelectList(_context.Suppliers, "SupplierId", "CompanyName", product.SupplierId);
            return View(product);
        }
      /*  [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var mdl = _serviceContext.ProductService.GetProductById(false);
        }*/
        // POST: ProductsDtoService/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,ProductName,SupplierId,CategoryId,QuantityPerUnit,UnitPrice,UnitsInStock,UnitsOnOrder,ReorderLevel,Discontinued")] Product product)
        {
            if (id != product.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.ProductId))
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
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName", product.CategoryId);
            ViewData["SupplierId"] = new SelectList(_context.Suppliers, "SupplierId", "CompanyName", product.SupplierId);
            return View(product);
        }

        // GET: ProductsDtoService/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Supplier)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: ProductsDtoService/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.ProductId == id);
        }
    }
}
