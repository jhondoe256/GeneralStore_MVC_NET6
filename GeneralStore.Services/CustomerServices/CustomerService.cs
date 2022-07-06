using GeneralStore.Models.CustomerModels;
using GeneralStore_MVC_NET6.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralStore.Services.CustomerServices
{
    public class CustomerService : ICustomerService
    {
        private readonly ApplicationDbContext _context;

        public CustomerService(ApplicationDbContext context)
        {
            _context = context;
        }
        public Task<bool> CreateCustomer(CustomerCreate customer)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteCustomer(int customerId)
        {
            throw new NotImplementedException();
        }

        public Task<CustomerDetail> GetCustomer(int customerId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<CustomerIndexModel>> ListCustmers()
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateCustomer(int customerId, CustomerEditModel customer)
        {
            throw new NotImplementedException();
        }
    }
}
