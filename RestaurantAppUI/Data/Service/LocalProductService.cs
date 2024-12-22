using RestaurantAppUI.Data.Repository;
using RestaurantAppUI.Domain;
using RestaurantAppUI.Domain.Model;
using RestaurantAppUI.Domain.Repository;
using RestaurantAppUI.Domain.Service;

namespace RestaurantAppUI.Data.Service;

public class LocalProductService(
    IProductRepository productRepository,
    IProductRequestRepository productRequestRepository,
    IProductDeductionRepository productDeductionRepository,
    IRestaurantMenuItemRepository restaurantMenuItemRepository,
    IMenuItemRepository menuItemRepository
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

    public IEnumerable<ProductEditing> GetProductEditing(IEnumerable<ProductRequestItem> items)
    {
        return productRepository.FindAll()
            .Select(p => ApplyProductEditing(p, items));
    }

    public bool IsProductRequestItemQuantityAvailable(ProductRequestItem item, IEnumerable<ProductRequestItem> allItems)
    {
        var quantity = CalculateProductRequestItemQuantityAvailable(item.ProductId, allItems);
        return item.Quantity <= quantity;
    }

    public decimal CalculateProductRequestItemQuantityAvailable(int productId,
        IEnumerable<ProductRequestItem> requestItems)
    {
        var product = productRepository.Find(productId);
        if (product == null)
        {
            throw new EntityNotFoundException(productId);
        }

        return product.Data.Quantity - requestItems.Where(i => i.ProductId == product.Id)
            .Sum(i => i.Quantity);
    }

    public IEnumerable<SavedModel<Product>> FindRequiredInMenuProducts(int restaurantId)
    {
        var menuItemIds = restaurantMenuItemRepository.FindAllByRestaurantId(restaurantId)
            .Select(item => item.Data.MenuItemId);
        var menuItems = menuItemRepository.FindAllByIds(menuItemIds);
        var requiredProductIds = menuItems.SelectMany(item => item.Data.Ingredients)
            .Select(item => item.ProductId)
            .Distinct().ToList();
        return productRepository.FindAllByIds(requiredProductIds);
    }

    private ProductEditing ApplyProductEditing(SavedModel<Product> product, IEnumerable<ProductRequestItem> items)
    {
        var requestQuantity = items.Where(i => i.ProductId == product.Id).Sum(i => i.Quantity);

        var quantity = product.Data.Quantity - requestQuantity;
        return new ProductEditing(product, quantity);
    }
}