using GeneralStore.Models.CustomerModels;
using GeneralStore.Models.TransactionModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralStore.Services.TransactionServices
{
    public interface ITransactionService
    {
        Task<bool> CreateTransaction(TransactionCreateModel transaction);
        Task<IEnumerable<TransactionListItem>> ListTransactions();
        Task<TransactionDetailModel> GetTransaction(int transactionId);
        Task<bool> UpdateTransaction(int transactionId, TransactionEditModel transaction);
        Task<bool> DeleteTransaction(int transactionId);
    }
}
