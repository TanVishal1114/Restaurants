using System;
using FluentValidation;

namespace Restaurants.Application.Restaurants.Queries.GetAllRestaurants;

public class GetAllRestaurantQueryValidator : AbstractValidator<GetAllRestaurantsQuery>
{
    private int[] allowedPageSizes = new[] { 5, 10, 20, 50 };
    public GetAllRestaurantQueryValidator()
    {
        RuleFor(x => x.PageNumber).GreaterThanOrEqualTo(1);
        RuleFor(x => x.PageSize).Must(value => allowedPageSizes.Contains(value))
                                   .WithMessage($"Page size must be in [{string.Join(",", allowedPageSizes)}]");
    }
}
