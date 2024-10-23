namespace RPSSL.Api.Extensions;

public static class WebApplicationExtensions
{
    public static void Configure(this WebApplication app)
    {
        if (app.Environment.IsDevelopment()) {
            app.UseDeveloperExceptionPage();
        }
        
        app.UseCors("CorsPolicy");
        app.UseRouting();

        app.UseSwagger();
        app.UseSwaggerUI(c => {
            c.SwaggerEndpoint("/swagger/v1.0/swagger.json", "RPSSL API v1");
            c.RoutePrefix = string.Empty;
        });
    }
}