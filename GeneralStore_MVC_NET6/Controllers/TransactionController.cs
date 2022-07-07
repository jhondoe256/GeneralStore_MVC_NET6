using GeneralStore.Models.TransactionModels;
using GeneralStore.Services.CustomerServices;
using GeneralStore.Services.ProductServices;
using GeneralStore.Services.TransactionServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace GeneralStore_MVC_NET6.Controllers
{
    public class TransactionController : Controller
    {
        private readonly ITransactionService _transactionService;
        private readonly ICustomerService _customerService;
        private readonly IProductService _productService;

        public TransactionController(
            ITransactionService transactionService
            ,ICustomerService customerService
            ,IProductService productService)
        {
            _transactionService = transactionService;
            _customerService = customerService;
            _productService = productService;
        }
        public async Task<IActionResult> Index()
        {
            var transactions = await _transactionService.ListTransactions();
            return View(transactions);
        }

        public async Task<IActionResult> Detail(int id)
        {
            var transaction = await _transactionService.GetTransaction(id);
            if (transaction is null) return NotFound();

            return View(transaction);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewData["CustomerId"] = new SelectList(await _customerService.ListCustmers(), "Id", "Email");
            ViewData["ProductId"] = new SelectList(await _productService.GetProducts(), "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TransactionCreateModel transactionCreate)
        {
            if (transactionCreate == null || !ModelState.IsValid)
                return BadRequest(ModelState);

            if (await _transactionService.CreateTransaction(transactionCreate))
                return RedirectToAction(nameof(Index));

            return View(transactionCreate);
        }


        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var transaction = await _transactionService.GetTransaction(id);
            if (transaction is null) return NotFound();

            return View(transaction);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, TransactionEditModel transactionEdit)
        {
            if (id != transactionEdit.Id || !ModelState.IsValid) return BadRequest(ModelState);

            if (await _transactionService.UpdateTransaction(id, transactionEdit))
                return RedirectToAction(nameof(Index));

            return UnprocessableEntity();
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var transaction = await _transactionService.GetTransaction(id);
            if (transaction is null) return NotFound();

            return View(transaction);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(TransactionDetailModel model)
        {
            if (await _transactionService.DeleteTransaction(model.Id))
                return RedirectToAction(nameof(Index));

            return UnprocessableEntity();
        }
    }
}
