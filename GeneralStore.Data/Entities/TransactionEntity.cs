using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralStore.Data.Entities
{
    public class TransactionEntity
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int CustomerId { get; set; }
        public int Quantity { get; set; }
        public DateTime DateOfTransaction { get; set; }
        // Ignore these warnings - these shouldn't ever be null
        public virtual CustomerEntity Customer { get; set; }
        public virtual ProductEntity Product { get; set; }
    }
}
