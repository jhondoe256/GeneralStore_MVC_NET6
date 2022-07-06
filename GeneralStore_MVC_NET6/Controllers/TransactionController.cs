using Microsoft.AspNetCore.Mvc;

namespace GeneralStore_MVC_NET6.Controllers
{
    public class TransactionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
