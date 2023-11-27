namespace WebApi.Extensions;

using Microsoft.EntityFrameworkCore;
using WebApi.Dal;
using WebApi.Dal.Database;
using WebApi.Services;
using WebApi.Services.Contracts;

public static class ServiceCollectionExtensions
{
    #region Methods

    public static IServiceCollection AddPetShelterServices(this IServiceCollection services)
    {
        services.AddDbContext<PetShelterDbContext>(
            (sp, dbOptions) =>
            {
                var config = sp.GetRequiredService<IConfiguration>();
                dbOptions.UseSqlServer(config.GetConnectionString("PetShelterDbContext"));
            },
            ServiceLifetime.Scoped,
            ServiceLifetime.Singleton);
        services.AddHttpClient<IPetService, PetService>(
            (sp, client) =>
            {
                var config = sp.GetRequiredService<IConfiguration>();
                client.BaseAddress = new Uri(config["SERVICE_CONFIG:MY_PET_REGISTER_URL_BASE"]!);
            });
        return services.AddScoped<IOwnerService, OwnerService>().AddScoped<IRepository, Repository>();
    }

    #endregion
}