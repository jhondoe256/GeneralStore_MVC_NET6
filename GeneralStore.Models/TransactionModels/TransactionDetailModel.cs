using GeneralStore.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralStore.Models.TransactionModels
{
    public class TransactionDetailModel
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string CustomerName { get; set; }
        public int Quantity { get; set; }
        public double TotalPrice { get; set; }
        public DateTime DateOfTransaction { get; set; }
    }
}
