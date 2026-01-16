using MediatR;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Dishes.Commands.CreateDish;
using Restaurants.Application.Dishes.Commands.DeleteDish;
using Restaurants.Application.Dishes.Dtos;
using Restaurants.Application.Dishes.Queries.GetByIdForRestaurant;
using Restaurants.Application.Dishes.Queries.GetDishesForRestaurant;
using Restaurants.Application.Restaurants.Commands.DeleteRestaurant;

namespace Restaurants.API.Controllers
{
    [Route("api/restaurants/{restaurantsId}/dishes")]
    [ApiController]
    public class DishesController(IMediator mediator) : ControllerBase
    {

        [HttpPost]
        public async Task<IActionResult> CreateDish([FromRoute] int restaurantsId, CreateDishCommand command)
        {
            command.RestaurantId = restaurantsId;
            var dishId = await mediator.Send(command);
            //return Ok(dish);
            return CreatedAtAction(nameof(GetByIdForRestaurant), new { restaurantsId, dishId }, null);

        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DishDto>>> GetAllForRestaurant([FromRoute] int restaurantsId)
        {
            var dish = await mediator.Send(new GetDishesForRestaurantQuery(restaurantsId));
            return Ok(dish);
        }

        [HttpGet("{dishId}")]
        public async Task<ActionResult<DishDto>> GetByIdForRestaurant([FromRoute] int restaurantsId, [FromRoute] int dishId)
        {
            var dish = await mediator.Send(new GetByIdForRestaurantQuery(restaurantsId, dishId));
            return Ok(dish);
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteDish([FromRoute] int restaurantsId)
        {
            await mediator.Send(new DeleteDishCommand(restaurantsId));

            return NoContent();
        }

    }
}
