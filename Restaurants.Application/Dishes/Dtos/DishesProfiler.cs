using AutoMapper;
using Restaurants.Domain.Entities;

namespace Restaurants.Application.Dishes.Dtos
{
    public class DishesProfiler : Profile
    {
        public DishesProfiler()
        {
            CreateMap<Dish, DishDto>();
        }
    }
}
