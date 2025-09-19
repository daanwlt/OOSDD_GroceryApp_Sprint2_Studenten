using Grocery.Core.Interfaces.Repositories;
using Grocery.Core.Interfaces.Services;
using Grocery.Core.Models;

namespace Grocery.Core.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IGroceryListItemsRepository _groceryListItemsRepository;

        public ProductService(IProductRepository productRepository, IGroceryListItemsRepository groceryListItemsRepository)
        {
            _productRepository = productRepository;
            _groceryListItemsRepository = groceryListItemsRepository;
        }

        public List<Product> GetAll()
        {
            return _productRepository.GetAll();
        }

        public List<Product> GetAvailableProducts(int groceryListId)
        {
            // Haal alle producten op
            List<Product> allProducts = _productRepository.GetAll();
            
            // Haal alle producten op die al op de boodschappenlijst staan
            List<GroceryListItem> existingItems = _groceryListItemsRepository.GetAll()
                .Where(item => item.GroceryListId == groceryListId)
                .ToList();
            
            // Filter producten die nog voorraad hebben en niet al op de lijst staan
            List<Product> availableProducts = allProducts
                .Where(product => product.Stock > 0 && 
                       !existingItems.Any(item => item.ProductId == product.Id))
                .ToList();
            
            return availableProducts;
        }

        public Product Add(Product item)
        {
            throw new NotImplementedException();
        }

        public Product? Delete(Product item)
        {
            throw new NotImplementedException();
        }

        public Product? Get(int id)
        {
            throw new NotImplementedException();
        }

        public Product? Update(Product item)
        {
            return _productRepository.Update(item);
        }
    }
}
