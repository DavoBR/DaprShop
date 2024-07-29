using Dapr;
using Microsoft.AspNetCore.Mvc;

namespace DaprShop.Recommendation.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RecommendationController : ControllerBase
{
    private const string PubsubName = "pubsub";
    private const string TopicNameOfShoppingCartItems = "daprshop.shoppingcart.items";

    [Topic(PubsubName, TopicNameOfShoppingCartItems)]
    [Route("products")]
    [HttpPost]
    public ActionResult AddProduct(Contracts.ProductItemAddedToShoppingCartEvent @event)
    {
        Console.WriteLine($"New product has been added into shopping cart. Product Id: {@event.ProductId} User Id: {@event.UserId}");

        return Ok();
    }
}
