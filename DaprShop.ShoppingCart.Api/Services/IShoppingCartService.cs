using DaprShop.ShoppingCart.Api.Domain;

namespace DaprShop.ShoppingCart.Api.Services;

public interface IShoppingCartService
{
    Task<Domain.ShoppingCart> GetShoppingCartAsync(string userId);
    Task AddItemToShoppingCartAsync(string userId, ShoppingCartItem shoppingCart);
}
