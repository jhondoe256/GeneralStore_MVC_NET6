using GeneralStore.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralStore.Models.TransactionModels
{
    public class TransactionListItem
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int CustomerId { get; set; }

    }
}
