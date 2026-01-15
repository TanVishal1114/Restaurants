using Restaurants.API.Middlewares;
using Restaurants.Application.Extention;
using Restaurants.Infrastructure.Extention;
using Restaurants.Infrastructure.Seeders;
using Serilog;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddScoped<ErrorHanlingMiddleware>();
builder.Services.AddScoped<RequestTimeLoggingMiddleware>();
builder.Services.AddSwaggerGen();
builder.Services.AddApplication();
builder.Services.AddInfrastructre(builder.Configuration);
builder.Host.UseSerilog((context, configuration) =>
configuration.ReadFrom.Configuration(context.Configuration)
//.MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
//.MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Information)
//.WriteTo.File("Logs/Restaurant-API-.log", rollingInterval: RollingInterval.Day, rollOnFileSizeLimit: true)
//.WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] |{SourceContext}| {NewLine} {Message:lj}{NewLine}{Exception}")

);

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

app.UseAuthorization();

app.MapControllers();

app.Run();
