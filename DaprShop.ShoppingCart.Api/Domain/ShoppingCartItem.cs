namespace DaprShop.ShoppingCart.Api.Domain;

public record ShoppingCartItem(string ProductId, string ProductName, decimal Price) {
    public int Quantity { get; set; }
}
