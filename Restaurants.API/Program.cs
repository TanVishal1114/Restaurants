using Microsoft.OpenApi.Models;
using Restaurants.API.Extensions;
using Restaurants.API.Middlewares;
using Restaurants.Application.Extention;
using Restaurants.Domain.Entities;
using Restaurants.Infrastructure.Extention;
using Restaurants.Infrastructure.Seeders;
using Serilog;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.


builder.AddPresentation();
builder.Services.AddApplication();
builder.Services.AddInfrastructre(builder.Configuration);

var app = builder.Build();
var scope = app.Services.CreateScope(); 
var seeder = scope.ServiceProvider.GetRequiredService<IRestaurantSeeder>();
await seeder.Seed();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<ErrorHanlingMiddleware>();
app.UseMiddleware<RequestTimeLoggingMiddleware>();
app.UseSerilogRequestLogging();
app.UseHttpsRedirection();
app.MapGroup("api/identity")
    .WithTags("Identity")
    .MapIdentityApi<UserEntity>();
app.UseAuthorization();

app.MapControllers();

app.Run();
