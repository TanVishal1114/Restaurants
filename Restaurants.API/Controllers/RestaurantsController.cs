using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Restaurants;
using Restaurants.Application.Restaurants.Dtos;

namespace Restaurants.API.Controllers;

[ApiController]
[Route("api/restaurants")]
public class RestaurantsController(IRestaurantsService restaurantsService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var restaurants = await restaurantsService.GetAllRestaurants();
        return Ok(restaurants);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetRestauranById([FromRoute] int id)
    {
        var restaurants = await restaurantsService.GetRestaurantById(id);
        if (restaurants is null)
            return NotFound();
        return Ok(restaurants);
    }

    [HttpPost]
    public async Task<IActionResult> CreateRestaurant([FromBody] CreateRestaurantDto createRestaurantDto)
    {
        //if(!ModelState.IsValid)
        //    return BadRequest(ModelState);
        int id = await restaurantsService.Create(createRestaurantDto);
        return CreatedAtAction(nameof(GetRestauranById), new { id }, null);
    }
}
