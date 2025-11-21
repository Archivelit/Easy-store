namespace Store.API.Extensions;

public static class WebApplicationExtensions
{
    /// <summary>
    /// Adds Swagger middleware to the request pipeline in development environment
    /// </summary>
    public static WebApplication UseSwaggerInDev(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("swagger/v1/swagger.json", "v1");
                options.RoutePrefix = string.Empty;
            });
        }

        return app;
    }
}