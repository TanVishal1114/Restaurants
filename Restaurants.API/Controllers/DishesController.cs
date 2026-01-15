using MediatR;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Dishes.Commands;
using Restaurants.Application.Dishes.Dtos;
using Restaurants.Application.Dishes.Queries.GetByIdForRestaurant;
using Restaurants.Application.Dishes.Queries.GetDishesForRestaurant;

namespace Restaurants.API.Controllers
{
    [Route("api/restaurants/{restaurantsId}/dishes")]
    [ApiController]
    public class DishesController(IMediator mediator) : ControllerBase
    {
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

        [HttpPost]
        public async Task<IActionResult> CreateDish([FromRoute] int restaurantsId, CreateDishCommand command)
        {
            command.RestaurantId = restaurantsId;
            var dish = await mediator.Send(command);
            return Ok(dish);
        }
    }
}
