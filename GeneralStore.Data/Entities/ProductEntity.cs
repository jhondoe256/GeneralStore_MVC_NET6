using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralStore.Data.Entities
{
    public class ProductEntity
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int QuantityInStock { get; set; }
        public double Price { get; set; }

        public virtual List<TransactionEntity> Transactions { get; set; }
    }
}
