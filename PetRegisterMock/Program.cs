public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var app = builder.Build();

        app.MapPost("/api/register/{petType}", async context =>
        {
            await context.Response.WriteAsync($"{context.Request.RouteValues["petType"]?.ToString()?.ToUpper() ?? "PET"}{DateTime.Now.Year}{Guid.NewGuid().ToString().ToUpper()[..5]}");
        });

        app.Run();
    }
}