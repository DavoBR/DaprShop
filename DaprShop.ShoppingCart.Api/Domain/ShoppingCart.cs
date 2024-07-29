namespace DaprShop.ShoppingCart.Api.Domain;

public record ShoppingCart(string UserId, List<ShoppingCartItem> Items);