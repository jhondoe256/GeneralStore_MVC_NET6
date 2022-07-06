using GeneralStore.Models.CustomerModels;
using GeneralStore.Models.ProductModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralStore.Services.ProductServices
{
    public interface IProductService
    {
        Task<bool> CreateProduct(ProductCreateModel product);
        Task<IEnumerable<ProductIndexModel>> GetProducts();
        Task<CustomerDetail> GetProduct(int productId);
        Task<bool> UpdateProduct(int productId, ProductEditModel product);
        Task<bool> DeleteProduct(int productId);
    }
}
