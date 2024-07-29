using System.Runtime.CompilerServices;
using Dapr.Client;
using DaprShop.Contracts;
using DaprShop.ShoppingCart.Api.Domain;

namespace DaprShop.ShoppingCart.Api.Services;

public class ShoppingCartService(DaprClient daprClient) : IShoppingCartService
{
    private const string StoreName = "statestore";

    public async Task AddItemToShoppingCartAsync(string userId, ShoppingCartItem item)
    {
        var shoppingCart = await GetShoppingCartAsync(userId);

        var existingItem = shoppingCart.Items.FirstOrDefault(x => x.ProductId == item.ProductId);

        if (existingItem is not null) {
            existingItem.Quantity += item.Quantity;
        } else {
            shoppingCart.Items.Add(item);
        }

        await daprClient.SaveStateAsync(StoreName, userId, shoppingCart);

        var productItemAddedToShoppingCartEvent = new ProductItemAddedToShoppingCartEvent(userId, item.ProductId);

        const string pubsubName = "pubsub";
        const string topicNameOfShoppingCartItems = "daprshop.shoppingcart.items";

        await daprClient.PublishEventAsync(pubsubName, topicNameOfShoppingCartItems, productItemAddedToShoppingCartEvent);
    }

    public async Task<Domain.ShoppingCart> GetShoppingCartAsync(string userId)
    {
        var shoppingCart = await daprClient.GetStateAsync<Domain.ShoppingCart>(StoreName, userId);

        shoppingCart ??= new Domain.ShoppingCart(userId, []);

        return shoppingCart;
    }
}
