using Catalog.API.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.API.Data
{
    public class CatalogContextSeed
    {
        public static void SeedData(IMongoCollection<Product> productCollection) {
            bool existProduct = productCollection.Find(p => true).Any();

            if (existProduct)
                return;

            productCollection.InsertManyAsync(GetPreconfiguredProduct());
        }

        private static IEnumerable<Product> GetPreconfiguredProduct()
        {
            return new List<Product>()
            {
                new Product()
                {
                    Name = "Iphone X",
                    Summary = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Pellentesque cursus consectetur dolor nec luctus. Sed aliquam, libero id aliquet suscipit, orci diam semper tortor, quis convallis neque metus in tellus",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit",
                    ImageFie = "product-1.png",
                    Price = 950,
                    Category = "Smart Phone"
                },
                 new Product()
                {
                    Name = "Samsung X",
                    Summary = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Pellentesque cursus consectetur dolor nec luctus. Sed aliquam, libero id aliquet suscipit, orci diam semper tortor, quis convallis neque metus in tellus",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit",
                    ImageFie = "product-2.png",
                    Price = 1000,
                    Category = "Smart Phone"
                },
                  new Product()
                {
                    Name = "Xiaomi X",
                    Summary = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Pellentesque cursus consectetur dolor nec luctus. Sed aliquam, libero id aliquet suscipit, orci diam semper tortor, quis convallis neque metus in tellus",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit",
                    ImageFie = "product-1.png",
                    Price = 500,
                    Category = "Dumb Phone"
                }

            };
        }
    }
}
