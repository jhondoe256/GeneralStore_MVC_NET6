using Microsoft.AspNetCore.Mvc;

namespace GeneralStore_MVC_NET6.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
