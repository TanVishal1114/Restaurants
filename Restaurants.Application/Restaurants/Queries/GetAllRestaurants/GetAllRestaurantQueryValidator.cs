using System;
using FluentValidation;
using Restaurants.Application.Restaurants.Dtos;

namespace Restaurants.Application.Restaurants.Queries.GetAllRestaurants;

public class GetAllRestaurantQueryValidator : AbstractValidator<GetAllRestaurantsQuery>
{
    private int[] allowedPageSizes = new[] { 5, 10, 20, 50 };
    private string[] allowedSortByValues = [nameof(RestaurantDto.Name), nameof(RestaurantDto.Description), nameof(RestaurantDto.Category)];
    public GetAllRestaurantQueryValidator()
    {
        RuleFor(x => x.PageNumber).GreaterThanOrEqualTo(1);
        RuleFor(x => x.PageSize).Must(value => allowedPageSizes.Contains(value))
                                   .WithMessage($"Page size must be in [{string.Join(",", allowedPageSizes)}]");
        RuleFor(x => x.SortBy).Must(value => allowedSortByValues.Contains(value))
                               .When(x => x.SortBy != null)
                               .WithMessage($"SortBy is optional, or must bring [{string.Join(",", allowedSortByValues)}]");
    }
}
