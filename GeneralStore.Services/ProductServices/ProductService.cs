using GeneralStore.Data.Entities;
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
        }
        public async Task<bool> CreateProduct(ProductCreateModel product)
        {
            if (product == null) return false;

            _context.Products.Add(new Product
            {
                Name = product.Name,
                Price = product.Price,
                QuantityInStock = product.QuantityInStock
            });

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteProduct(int productId)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product is null)
                return false;
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<ProductDetailModel> GetProduct(int? productId)
        {
            return (productId !=null) ? await _context.Products.Select(p => new ProductDetailModel
            {
                Id = p.Id,
                Name = p.Name,
                QuantityInStock = p.QuantityInStock,
                Price = p.Price
            }).FirstOrDefaultAsync(m => m.Id == productId) : null;
            
        }

        public async Task<IEnumerable<ProductIndexModel>> GetProducts()
        {
            var products = await _context.Products.Select(p => new ProductIndexModel
            {
                Id = p.Id,
                Name = p.Name,
                QuantityInStock = p.QuantityInStock,
                Price = p.Price
            }).ToListAsync();

            return products;
        }

        public async Task<bool> UpdateProduct(int productId, ProductEditModel product)
        {
            var productInDb = await _context.Products.FindAsync(productId);
            if (productInDb is null)
                return false;

            if (productInDb!=null)
            {
                productInDb.Name = product.Name;
                productInDb.Price = product.Price;
                productInDb.QuantityInStock = product.QuantityInStock;
                try
                {
                    _context.Update(productInDb);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExist(productInDb.Id))
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
