using ApiMongo.Models;
using MongoDB.Bson.Serialization;
using MongoDB.Generators;
using MongoDB.Infrastructure;

namespace ApiMongo.Infraestructure.Data.Mappings;

public class CustomerMap : IMongoDbFluentConfiguration
{
    public void Configure()
    {
        if (BsonClassMap.IsClassMapRegistered(typeof(Customer)))
        {
            return;
        }

        BsonClassMap.RegisterClassMap<Customer>(builder =>
        {
            builder.AutoMap();
            builder.MapIdMember(x => x.Id).SetIdGenerator(Int64IdGenerator<Customer>.Instance);
        });
    }
}