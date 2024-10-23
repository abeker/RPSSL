namespace RPSSL.Api.Extensions;

public static class WebApplicationExtensions
{
    public static void Configure(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1.0/swagger.json", "RPSSL API");
                options.RoutePrefix = "swagger";
            });
        }
        
        app.UseCors("CorsPolicy");
        app.UseHttpsRedirection();
        app.UseRouting();
        
        app.MapControllers();
    }
}