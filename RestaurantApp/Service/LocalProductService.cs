using RestaurantApp.Model;
using RestaurantApp.Repository;

namespace RestaurantApp.Service;

public class LocalProductService(
    IProductRepository productRepository,
    IProductRequestRepository productRequestRepository,
    IProductDeductionRepository productDeductionRepository
) : IProductsService
{
    public decimal CalculateProductsQuantityInRestaurant(int restaurantId, int productId)
    {
        var productRequests = productRequestRepository
            .FindAllByRestaurantIdAndContainsItemWithProductId(restaurantId, productId)
            .SelectMany(item => item.Data.ProductRequestItems)
            .Where(item => item.ProductId == productId);
        var productDeductions = productDeductionRepository.FindAllByRestaurantIdAndProductId(restaurantId, productId);
        return productRequests.Sum(request => request.Quantity) - productDeductions.Sum(item => item.Data.RestaurantId);
    }

    public void AddProductRequest(ProductRequest request)
    {
        var items = SimplifyProductRequestItems(request.ProductRequestItems);
        var newRequest = new ProductRequest.Builder()
            .SetRestaurantId(request.RestaurantId)
            .SetRequestDate(request.RequestDate)
            .AddProductRequestItems(items)
            .Build();
        ;
        productRequestRepository.Add(newRequest);
        foreach (var item in newRequest.ProductRequestItems)
        {
            var savedModel = productRepository.Find(item.ProductId);
            if (savedModel == null)
            {
                throw new EntityNotFoundException(item.ProductId, "Product");
            }

            productRepository.Update(savedModel.Id, new Product.Builder(savedModel.Data)
                .SetQuantity(savedModel.Data.Quantity - item.Quantity)
                .Build());
        }
    }

    private IEnumerable<ProductRequestItem> SimplifyProductRequestItems(IEnumerable<ProductRequestItem> items)
    {
        return items.GroupBy(item => item.ProductId)
            .Select(group => new ProductRequestItem.Builder()
                .SetProductId(group.Key)
                .SetQuantity(group.Sum(i => i.Quantity))
                .Build()).ToList();
    }

    public List<ProductEditing> GetProductEditing(List<ProductRequestItem> items)
    {
        return productRepository.FindAll()
            .Select(p => ApplyProductEditing(p, items))
            .ToList();
    }

    public bool IsProductRequestItemQuantityAvailable(ProductRequestItem item, List<ProductRequestItem> allItems)
    {
        var quantity = CalculateAvailableQuantity(item.ProductId, allItems);
        return item.Quantity <= quantity;
    }

    private decimal CalculateAvailableQuantity(int productId, List<ProductRequestItem> requestItems)
    {
        var product = productRepository.Find(productId);
        if (product == null)
        {
            throw new EntityNotFoundException(productId);
        }

        return product.Data.Quantity - requestItems.Where(i => i.ProductId == product.Id)
            .Sum(i => i.Quantity);
    }

    private ProductEditing ApplyProductEditing(SavedModel<Product> product, List<ProductRequestItem> items)
    {
        var requestQuantity = items.Where(i => i.ProductId == product.Id).Sum(i => i.Quantity);

        var quantity = product.Data.Quantity - requestQuantity;
        return new ProductEditing(product, quantity);
    }
}