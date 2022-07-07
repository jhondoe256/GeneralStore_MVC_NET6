using GeneralStore.Models.ProductModels;
using GeneralStore.Services.ProductServices;
using Microsoft.AspNetCore.Mvc;

namespace GeneralStore_MVC_NET6.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetProducts();
            return View(products);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if(id == null) return NotFound();

            var product = await _productService.GetProduct(id);
            if(product == null) return NotFound();

            return View(product);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductCreateModel product)
        {
            if (product == null || !ModelState.IsValid) return BadRequest(ModelState);

            if(await _productService.CreateProduct(product))
                return RedirectToAction(nameof(Index));
            else
                return View(product);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var product = await _productService.GetProduct(id);
            if (product == null) return NotFound();

            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProductEditModel productEditModel)
        {
            if (id != productEditModel.Id || !ModelState.IsValid)
                return BadRequest(ModelState);
            else
                if (await _productService.UpdateProduct(id, productEditModel))
                return RedirectToAction(nameof(Index));
            else
                return UnprocessableEntity();
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var product = await _productService.GetProduct(id);
            if (product == null) return NotFound();

            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            if(await _productService.DeleteProduct(id))
                return RedirectToAction(nameof(Index));
            return UnprocessableEntity(); // or BadRequest...
        }

    }
}
