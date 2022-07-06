using GeneralStore.Data.Entities;
using GeneralStore.Models.TransactionModels;
using GeneralStore_MVC_NET6.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralStore.Services.TransactionServices
{
    public class TransactionService : ITransactionService
    {

        private readonly ApplicationDbContext _context;

        public TransactionService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<bool> CreateTransaction(TransactionCreateModel transaction)
        {
            var customerInDb = await _context.Customers.FindAsync(transaction.CustomerId);
            if (customerInDb == null)
                return false;
           
            var productInDb = await _context.Products.FindAsync(transaction.ProductId);
            if (productInDb == null)
                return false;

            var entity = new Transaction
            {
                CustomerId=customerInDb.Id,
                Customer=customerInDb,
                ProductId=productInDb.Id,
                Product=productInDb,
                Quantity=transaction.Quantity,
                DateOfTransaction=DateTime.Now
            };

            _context.Transactions.Add(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteTransaction(int customerId)
        {
            var transaction = await _context.Transactions.FindAsync(customerId);
            if (transaction is null)
                return false;
           
            _context.Transactions.Remove(transaction);
            
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<TransactionDetailModel> GetTransaction(int? customerId)
        {
            if (customerId == null)
                return null;
            
            var transaction = await _context.Transactions.Include(t => t.Customer).Include(t => t.Product).FirstOrDefaultAsync(m => m.Id == customerId);
           
            if (transaction is null)
                return null;

            return new TransactionDetailModel
            {
                Id=transaction.Id,
                CustomerId=transaction.CustomerId,
                ProductId=transaction.ProductId,
                Product=transaction.Product,
                Customer=transaction.Customer,
                DateOfTransaction=transaction.DateOfTransaction,
                Quantity=transaction.Quantity,
            };
        }

        public async Task<IEnumerable<TransactionListItem>> ListTransactions()
        {
            var transactions = await _context.Transactions.Include(t => t.Customer).Include(t => t.Product)
                .Select(t => new TransactionListItem
                {
                    Id = t.Id,
                    CustomerId = t.CustomerId,
                    ProductId = t.ProductId,
                }).ToListAsync();
            return transactions;
        }

        public async Task<bool> UpdateTransaction(int customerId, TransactionEditModel transaction)
        {
            var transactionInDb = await _context.Transactions.FindAsync(transaction);
            if (transactionInDb is null)
                return false;

            if (transactionInDb != null)
            {
                //transactionInDb.CustomerId = transaction.CustomerId;
                //transactionInDb.ProductId = transaction.ProductId;

                try
                {
                    _context.Update(transactionInDb);
                    transactionInDb.DateOfTransaction = DateTime.UtcNow;
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TransactionExist(transactionInDb.Id))
                    {
                        return false;
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return false;
        }

        private bool TransactionExist(int id)
        {
            return _context.Products.Any(p => p.Id == id);
        }
    }
}
