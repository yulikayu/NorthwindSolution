using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Northwind.Contracts.Dto.Category;
using Northwind.Contracts.Dto.Product;
using Northwind.Domain.Models;
using Northwind.Persistence;
using Northwind.Services.Abstraction;
using X.PagedList;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Northwind.Web.Controllers
{
    public class ProductsPagedServerController : Controller
    {
        //Depedensi Injection
        private readonly NorthwindContext _context;
        private readonly IServiceManager _serviceContext;
        private readonly IUtilityService _utilityService;

        public ProductsPagedServerController(NorthwindContext context, IServiceManager serviceContext = null, IUtilityService utilityService = null)
        {
            _context = context;
            _serviceContext = serviceContext;
            _utilityService = utilityService;
        }
        private List<SelectListItem>GetPageSize(int selectPagesize = 10)
        {
            var pageSize = new List<SelectListItem>();
            if(selectPagesize==5)
                pageSize.Add(new SelectListItem("5","5",true));
            else
                pageSize.Add(new SelectListItem("5", "5"));
            for (int i = 10; i <=100; i += 10)
            {
                if (i == selectPagesize)
                {
                    pageSize.Add(new SelectListItem(i.ToString(),i.ToString(),true));   
                }
                else
                    pageSize.Add(new SelectListItem(i.ToString(),i.ToString()));
            }
            return pageSize;
            
        }


        // GET: ProductsPagedServer
        public async Task<IActionResult> Index(string searchString, string currentFilter,
            int? page, int? fetchSize,string sortOrder)
        {
            /*  var northwindContext = _context.Products.Include(p => p.Category).Include(p => p.Supplier);*/
            var pageIndex = page ?? 1;
            var pageSize = fetchSize ?? 5;
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;

            var productDtos = await _serviceContext.ProductService.GetProductPaged(pageIndex,pageSize, false);
            var totalRows = productDtos.Count();
            switch (sortOrder)
            {
                case "OrderDate":
                    productDtos = productDtos.OrderByDescending(x => x.ProductName);
                    break;
                case "RequiredDate":
                    productDtos = productDtos.OrderByDescending(s => s.UnitPrice);
                    break;
                case "ShipName":
                    productDtos = productDtos.OrderBy(s => s.Category.CategoryName);
                    break;
                default:
                    productDtos = productDtos.OrderBy(s => s.ProductName);
                    break;
            }

            if (!String.IsNullOrEmpty(searchString))
            {
                productDtos = productDtos
                    .Where(p => p.ProductName.ToLower().Contains(searchString)||p.Category.CategoryName.ToLower().Contains(searchString));
            }
            /*var produtDtosPaged = 
                new StaticPagedList<ProductDto>(productDtos, pageIndex + 1, pageSize - (pageSize-1), totalRows);
            ViewBag.PagedList = new SelectList(new List<int> { 8, 15, 20 });*/
            var produtDtosPaged =
                new StaticPagedList<ProductDto>(productDtos, pageIndex + 1, pageSize - (pageSize - 1), totalRows);
            ViewBag.PagedList = GetPageSize(pageSize);

            ViewData["CurrentFilter"] = searchString;
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["PriceSortParm"] = string.IsNullOrEmpty(sortOrder) ? "unitPrice" : "";
            ViewData["DataSortParm"] = sortOrder == "Cate" ? "UnitInOrder" : "Cate";
            return View(produtDtosPaged);
        }
/*
        [HttpPost]
        public async Task<IActionResult> CreateProductPhoto1(ProductPhotoGroupDto productPhotoGroupDto)
        {
            *//*var latestProductId = _serviceContext.ProductService.CreateProductId(productPhotoDto.productForCreatDto);
            if (ModelState.IsValid)
            {
                try
                {
                    var file = productPhotoDto.AllPhoto;
                    var folderName = Path.Combine("Resources", "images");
                    var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                    if (file.Count > 0)
                    {
                        foreach (var item in file)
                        {
                            var fileName = ContentDispositionHeaderValue.Parse(item.ContentDisposition).FileName.Trim('"');
                            var fullPath = Path.Combine(pathToSave, fileName);
                            var dbPath = Path.Combine(folderName, fileName);
                            using (var stream = new FileStream(fullPath, FileMode.Create))
                            {
                                item.CopyTo(stream);
                            }

                            var convertSize = (Int16)item.Length;

                            var Photoo = new ProductPhotoForCreateDto
                            {
                                PhotoFilename = fileName,
                                PhotoFileType = item.ContentType,
                                PhotoFileSize = (byte)convertSize,
                                PhotoProductId = latestProductId.ProductId
                            };
                            _serviceContext.ProductPhotoService.insert(Photoo);

                        }
                        return RedirectToAction(nameof(Index));

                       *//* var productGroup = new ProductPhotoGroupDto
                        {
                            productForCreateDto = productPhotoDto.productForCreateDto,
                            Photo1 = productPhotoDto.Photo1,
                            Photo2 = productPhotoDto.Photo2,
                            Photo3 = productPhotoDto.Photo3
                        };*//*
                    }
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
            *//* string fileName = null;
             var n = productPhotoDto.AllPhoto.Count;
             if (n > 0)
             {
                 string upFolname = Path.Combine("Resources", "images");
                 fileName = Guid.NewGuid().ToString();
                 var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), fileName);
                 using (var fileStream = new FileStream(pathToSave, FileMode.Create))
                 {
                     productPhotoDto.AllPhoto.CopyTo(fileStream);
                 }
             }*//*
            if (ModelState.IsValid)
            {
                return View("Create");
            }
            return View("Create");
        }*/
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
                        PhotoFilename=fileName,
                        PhotoFileSize=(byte)convertSize,
                        PhotoFileType=item.ContentType
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


        // GET: ProductsPagedServer/Details/5
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

        // GET: ProductsPagedServer/Create
        public async Task<IActionResult> Create()
        {
            var allCategory = await _serviceContext.CategoryService.GetAllCategory(false);
            var allSupplier = await _serviceContext.SupplierService.GetAllSupplier(false);
            ViewData["CategoryId"] = new SelectList(allCategory, "CategoryId", "CategoryName");
            ViewData["SupplierId"] = new SelectList(allSupplier, "SupplierId", "CompanyName");
            return View();
        }

        // POST: ProductsPagedServer/Create
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

       /*     var inp = IFormFile.ReferenceEquals('TextBoxFor');
            inp.addEventListener('change', function(e){
                var file = this.files[0];
                var reader = new FileReader();
                reader.onload = function(){
                    document.getElementById('preview').src = this.result;
                };
                reader.readAsDataURL(file);
            },false);*/
            return View(product);
        }

        // GET: ProductsPagedServer/Edit/5
        public async Task<IActionResult> Edit(int? id)
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

        // POST: ProductsPagedServer/Edit/5
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

        // GET: ProductsPagedServer/Delete/5
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

        // POST: ProductsPagedServer/Delete/5
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