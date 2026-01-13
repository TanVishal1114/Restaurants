using FluentValidation;
using Restaurants.Application.Restaurants.Dtos;

namespace Restaurants.Application.Restaurants.Commands.CreateRestaurant;

public class CreateRestaurantCommandValidator:AbstractValidator<CreateRestaurantCommand>
{
    private readonly List<string> validCategories=["Italian", "maxican", "Chinese", "Indian", "French", "Japanese"];
    public CreateRestaurantCommandValidator()
    {
        RuleFor(x => x.Name)
            .Length(3, 100);
        //.NotEmpty().WithMessage("Name is required")
        // .MaximumLength(100).WithMessage("Name must not exceed 100 characters");

        // Example of how to validate required properties in DTO using FluentValidation.
        //no need to check if property have not null value because in dto it is required(non nullable)

        //RuleFor(x => x.Description)
        //    .NotEmpty().WithMessage("Description is required");

        //RuleFor(x => x.Category)
        //    .NotEmpty().WithMessage("Insert a valid category");

        RuleFor(x => x.Category)
            .Must(validCategories.Contains)
            .WithMessage("Invalid Category. Please choose from the valid categories");
            //.Custom((value, context) =>
            //{
            //    if (!validCategories.Contains(value))
            //    {
            //        context.AddFailure("Category", $"'{value}' is not a valid category. Valid categories are: {string.Join(", ", validCategories)}");
            //    }
            //})
            //.NotEmpty().WithMessage("Insert a valid category");


        RuleFor(x => x.ContactEmail)    
            .EmailAddress().WithMessage("Please provide a valid email address");    

        RuleFor(x => x.PostalCode)   
            .Matches(@"^\d{2}-\d{3}$")
            .WithMessage("Please provide a valid postal code (XX-XXX).");
    }
}

