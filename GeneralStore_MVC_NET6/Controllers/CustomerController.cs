using GeneralStore.Models.CustomerModels;
using GeneralStore.Services.CustomerServices;
using Microsoft.AspNetCore.Mvc;

namespace GeneralStore_MVC_NET6.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }
        
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var customers = await _customerService.ListCustmers();
            return View(customers);
        }
        
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CustomerCreate customerCreate)
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMsg"] = "Model State is Invalid";
                return View(customerCreate);
            }
            else
            {
                if (await _customerService.CreateCustomer(customerCreate))
                    return RedirectToAction(nameof(Index));
            }

            TempData["ErrorMsg"] = "Unable to save to the database. Please Try Again later.";
            return View(customerCreate);
        }
        
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            var customer = await _customerService.GetCustomer(id);
            if (customer == null) return NotFound();
            return View(customer);
        }

        [HttpGet]
        public async Task<IActionResult> EditCustomer(int? id)
        {
            var customer = await _customerService.GetCustomer(id);
            if (customer == null) return NotFound();
            return View(customer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditCustomer(int id, CustomerEditModel customerEditModel)
        {
            if (id != customerEditModel.Id || !ModelState.IsValid)
                return BadRequest(ModelState);
            else
            {
                if (await _customerService.UpdateCustomer(id, customerEditModel))
                    return RedirectToAction(nameof(Index));
            }
            return View(customerEditModel);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteCustomer(int? id)
        {
            var customer = await _customerService.GetCustomer(id);
            if (customer == null) return NotFound();
            return View(customer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            if (await _customerService.DeleteCustomer(id))
                return RedirectToAction(nameof(Index));
            else
            {
                return BadRequest();
            }
        }
    }
}
