using Calculator.Application.Common.Extensions;
using Calculator.Infrastructure.Common.InfrastructureServices;
using Calculator.Web.Common.Extensions;
using Calculator.Web.Common.Middleware.ErrorHandling;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
    .AddWebServices(builder.Configuration)
    .AddApplicationServices()
    .AddInfrastructureServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseMiddleware<ErrorHandlerMiddleware>();

app.MapControllers();

app.Run();