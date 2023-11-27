using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using WebApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson(
    setup =>
    {
        setup.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
        setup.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
    });

builder.Services.AddPetShelterServices();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
        options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo { Title = "Pet Shelter", Version = "1.0.0", Description = "DotNet Core Dojo" });
        })
    .AddSwaggerGenNewtonsoftSupport();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(setup => { setup.RouteTemplate = "/swagger/{documentName}/openapi.json"; });
    app.UseSwaggerUI(options => options.SwaggerEndpoint("v1/openapi.json", "Pet Shelter v1"));
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
