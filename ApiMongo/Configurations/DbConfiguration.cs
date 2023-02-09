using ApiMongo.Infraestructure.Data.Context;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Infrastructure;
using MongoDB.Infrastructure.Extensions;
using MongoDB.UnitOfWork.Abstractions.Extensions;

namespace PaymentApi.Configurations;

public static class DbConfiguration
{
    public static void AddDbConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMongoDbContext<IMongoDbContext, ApplicationDbContext>(provider =>
        {
            var connectionString = configuration.GetValue<string>("MongoSettings:ConnectionString");
            var databaseName = configuration.GetValue<string>("MongoSettings:DatabaseName");

            var context = new ApplicationDbContext(connectionString, databaseName);

            return context;
        });

        services.AddMongoDbUnitOfWork();
        services.AddMongoDbUnitOfWork<ApplicationDbContext>();
    }
}