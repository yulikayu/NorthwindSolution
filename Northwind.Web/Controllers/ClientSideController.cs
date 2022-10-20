﻿using Microsoft.AspNetCore.Mvc;
using Northwind.Contracts.Dto.Category;
using Northwind.Domain.Base;

namespace Northwind.Web.Controllers
{
    public class ClientSideController : Controller
    {
  

        public IRepositoryManager _repositoryManager { get; set; }
        public ClientSideController(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }
        public JsonResult GetTotalProductByCategory()
        {
            var result = _repositoryManager.ProductRepository.GetTotalProductByCategory();
            return Json(result);
        }
        public IActionResult IndexJs()
        {
            return View();
        }
        public IActionResult IndexJQuery()
        {
            return View();
        }
        public IActionResult IndexChart()
        {
            return View();
        }

        [HttpPost]
        public IActionResult PostCategory ([FromBody] CategoryForCreateDto categoryForCreateDto)
        {
            var categoryDto = categoryForCreateDto;

            var result = new JsonResult(null)
            {
                Value = "Succes"
            };
            return Ok(result);

        }
    }
}
