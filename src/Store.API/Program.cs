using API.Requests;
using API.Middleware.Logging;
using Store.Core.Contracts.Customers;
using Store.Infrastructure.Data.Sqlite.CustomersData;

namespace API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        builder.Services.AddServices();

        var app = builder.Build();

        app.UseLogging();
        
        new CustomerAuthentication().InitializeDatabaseAsync();

        app.MapPost("/RegisterCustomer", async (ICustomerManager customerManager, 
                                                        RegisterCustomerRequest request) 
                                                        =>
                                                        {
                                                            await customerManager.RegisterAsync(
                                                                request.Name,
                                                                request.Email,
                                                                request.Password);
                                                            return Results.Ok();
                                                        });

        app.MapPost("/AuthenticateCustomer", async (ICustomerManager customerManager,
                                                            AuthenticateCustomerRequest request)
                                                            =>
                                                            {
                                                                var token = await customerManager.AuthenticateAsync(
                                                                    request.Email,
                                                                    request.Password);
                                                                return Results.Ok( new { token } );
                                                            });
        
        app.Run();
    }
}