using GeneralStore.Models.CustomerModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralStore.Services.CustomerServices
{
    public interface ICustomerService
    {
        Task<bool> CreateCustomer(CustomerCreate customer);
        Task<IEnumerable<CustomerIndexModel>>ListCustmers();
        Task<CustomerDetail>GetCustomer(int customerId);
        Task<bool>UpdateCustomer(int customerId, CustomerEditModel customer);
        Task<bool>DeleteCustomer(int customerId);
    }
}
