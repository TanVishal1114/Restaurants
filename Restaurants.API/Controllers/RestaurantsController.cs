using MediatR;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Restaurants;
using Restaurants.Application.Restaurants.Commands.CreateRestaurant;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Application.Restaurants.Queries.GetAllRestaurants;
using Restaurants.Application.Restaurants.Queries.GetRestaurantById;

namespace Restaurants.API.Controllers;

[ApiController]
[Route("api/restaurants")]
public class RestaurantsController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var restaurants = await mediator.Send(new GetAllRestaurantsQuery());
        return Ok(restaurants);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetRestauranById([FromRoute] int id)
    {
        var restaurants = await mediator.Send(new GetRestaurantByIdQuery(id));
        if (restaurants is null)
            return NotFound();
        return Ok(restaurants);
    }

    [HttpPost]
    public async Task<IActionResult> CreateRestaurant([FromBody] CreateRestaurantCommand createRestaurantCommand)
    {
        //if(!ModelState.IsValid)
        //    return BadRequest(ModelState);
        int id = await mediator.Send(createRestaurantCommand);
        return CreatedAtAction(nameof(GetRestauranById), new { id }, null);
    }
}
