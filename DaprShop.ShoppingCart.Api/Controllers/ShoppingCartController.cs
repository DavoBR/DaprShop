using DaprShop.ShoppingCart.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace DaprShop.ShoppingCart.Api.Controllers;

[ApiController]
[Route("api/shopping-cart")]
public class ShoppingCartController(IShoppingCartService shoppingCartService) : ControllerBase
{
    [HttpGet("{userId}")]
    public async Task<ActionResult<Domain.ShoppingCart>> Get(string userId)
    {
        var shoppingCart = await shoppingCartService.GetShoppingCartAsync(userId);

        return Ok(shoppingCart);
    }

    [HttpPost("{userId}/items")]
    public async Task<ActionResult> Post(string userId, [FromBody] Domain.ShoppingCartItem item)
    {
        try
        {
            await shoppingCartService.AddItemToShoppingCartAsync(userId, item);

            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
