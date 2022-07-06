using GeneralStore.Data.Entities;
using GeneralStore.Models.CustomerModels;
using GeneralStore_MVC_NET6.Data;
using Microsoft.EntityFrameworkCore;
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
        public async Task<bool> CreateCustomer(CustomerCreate customer)
        {
            if (customer != null)
            {
                _context.Customers.Add(new Customer
                {
                    Name = customer.Name,
                    Email = customer.Email
                });

                if (await _context.SaveChangesAsync() == 1)
                    return true;
            }
            return false;

        }

        public async Task<bool> DeleteCustomer(int customerId)
        {
            var customer = _context.Customers.Find(customerId);
            if (customer is null)
                return false;
            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<CustomerDetail> GetCustomer(int? customerId)
        {
            if (customerId == null)
                return null;

            var customer = await _context.Customers.FindAsync(customerId);
            
            if (customer is null)
                return null;
            
            return new CustomerDetail
            {
                Id = customer.Id,
                Name = customer.Name,
                Email = customer.Email
            };
        }

        public async Task<IEnumerable<CustomerIndexModel>> ListCustmers()
        {
            var customers =await _context.Customers.Select(customer => new CustomerIndexModel
            {
                Id = customer.Id,
                Name = customer.Name,
                Email = customer.Email
            }).ToListAsync();
            return customers;
        }

        public async Task<bool> UpdateCustomer(int customerId, CustomerEditModel customer)
        {
            var customerInDb = await _context.Customers.FindAsync(customerId);
            if (customerInDb is null)
                return false;

            if (customerInDb != null)
            {
                customerInDb.Name = customer.Name;
                customerInDb.Email = customer.Email;
                try
                {
                    _context.Update(customerInDb);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExist(customerInDb.Id))
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

        private bool ProductExist(int id)
        {
            return _context.Products.Any(p => p.Id == id);
        }
    }
}
