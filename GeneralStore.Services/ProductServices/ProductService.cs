using GeneralStore.Models.CustomerModels;
using GeneralStore.Models.ProductModels;
using GeneralStore_MVC_NET6.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralStore.Services.ProductServices
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _context;

        public ProductService(ApplicationDbContext context)
        {
            _context = context;
            _context = context;
        }
        public Task<bool> CreateProduct(ProductCreateModel product)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteProduct(int productId)
        {
            throw new NotImplementedException();
        }

        public Task<CustomerDetail> GetProduct(int productId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ProductIndexModel>> GetProducts()
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateProduct(int productId, ProductEditModel product)
        {
            throw new NotImplementedException();
        }
    }
}
