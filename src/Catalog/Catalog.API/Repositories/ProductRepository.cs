using Catalog.API.Data.Interfaces;
using Catalog.API.Entities;
using Catalog.API.Repositories.Interfaces;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.API.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ICatalogContexts _contexts;

        public ProductRepository(ICatalogContexts contexts)
        {
            _contexts = contexts;
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _contexts.Products.Find(p => true).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductByCategory(string categoryName)
        {
            FilterDefinition <Product> filter = Builders<Product>.Filter.ElemMatch(p => p.Category, categoryName);

            return await _contexts
                .Products
                .Find(filter)
                .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductByName(string name)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.ElemMatch(p => p.Name, name);

            return await _contexts
                .Products
                .Find(filter)
                .ToListAsync();
        }

        public async Task<Product> GetProduct(string id)
        {
            return await _contexts.Products.Find(p => p.Id == id).FirstOrDefaultAsync();
        }

        public async Task Create(Product product)
        {
            await _contexts.Products.InsertOneAsync(product);
        }

        public async Task<bool> Delete(string id)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Id, id);

            var deleteResult = await _contexts.Products.DeleteOneAsync(filter);

            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
        }

        public async Task<bool> Update(Product product)
        {
            var updateResult = await _contexts.Products.ReplaceOneAsync(filter: g => g.Id == product.Id, replacement: product);

            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
        }
    }
}
