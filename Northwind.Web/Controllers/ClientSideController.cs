using Microsoft.AspNetCore.Mvc;

namespace Northwind.Web.Controllers
{
    public class ClientSideController : Controller
    {
        public IActionResult IndexJs()
        {
            return View();
        }
        public IActionResult IndexJQuery()
        {
            return View();
        }
    }
}
