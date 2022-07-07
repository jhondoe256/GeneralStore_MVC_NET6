﻿using GeneralStore.Data.Entities;
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
            var entity = new TransactionEntity
            {
                CustomerId=transaction.CustomerId,
                ProductId=transaction.ProductId,
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

        public async Task<TransactionDetailModel> GetTransaction(int transactionId)
        {
            var transaction = await _context.Transactions
                .Include(t => t.Customer)
                .Include(t => t.Product)
                .FirstOrDefaultAsync(t => t.Id == transactionId);
           
            if (transaction is null)
                return null;

            return new TransactionDetailModel
            {
                Id = transaction.Id,
                CustomerName = transaction.Customer.Name,
                ProductName = transaction.Product.Name,
                DateOfTransaction = transaction.DateOfTransaction,
                TotalPrice = transaction.Product.Price * transaction.Quantity,
                Quantity = transaction.Quantity,
            };
        }

        public async Task<IEnumerable<TransactionListItem>> ListTransactions()
        {
            var transactions = await _context.Transactions
                .Include(t => t.Customer)
                .Include(t => t.Product)
                .Select(t => new TransactionListItem
                {
                    Id = t.Id,
                    ProductName = t.Product.Name,
                    Quantity = t.Quantity,
                }).ToListAsync();
            return transactions;
        }

        public async Task<bool> UpdateTransaction(int customerId, TransactionEditModel transaction)
        {
            var transactionInDb = await _context.Transactions.FindAsync(transaction.Id);
            if (transactionInDb is null)
                return false;

            if (transactionInDb != null)
            {
                transactionInDb.CustomerId = transaction.CustomerId;
                transactionInDb.ProductId = transaction.ProductId;
                transactionInDb.Quantity= transaction.Quantity;

                try
                {
                    _context.Update(transactionInDb);
                    transactionInDb.DateOfTransaction = DateTime.UtcNow;
                    await _context.SaveChangesAsync();
                    return true;
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
