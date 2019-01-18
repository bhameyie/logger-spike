using Autofac;
using Heimdall.DataAccess.MongoDb.Entities;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Heimdall.DataAccess.MongoDb
{
    public class MongoModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(container =>
            {
                var config = container.Resolve<IConfiguration>();
                return new MongoClient(config["Heimdall:Mongo:Host"]);
            }).As<IMongoClient>().SingleInstance();

            builder.RegisterType<MongoSightingRepository>().As<ISightingRepository<ReportedSighting>>();
        }
    }
}