using AutoMapper;
using Restaurants.Application.Dishes.Commands;
using Restaurants.Domain.Entities;

namespace Restaurants.Application.Dishes.Dtos
{
    public class DishesProfiler : Profile
    {
        public DishesProfiler()
        {
            CreateMap<CreateDishCommand, Dish>();
            CreateMap<Dish, DishDto>();
        }
    }
}
