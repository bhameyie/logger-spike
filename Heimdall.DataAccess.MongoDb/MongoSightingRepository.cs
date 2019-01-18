using System;
using System.Collections.Generic;
using Heimdall.DataAccess.MongoDb.Entities;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Heimdall.DataAccess.MongoDb
{
    public class MongoSightingRepository : ISightingRepository<ReportedSighting>
    {
        private IMongoCollection<ReportedSighting> _reportedSightingsCollection;

        public MongoSightingRepository(IMongoClient mongoClient, IConfiguration configuration)
        {
            var dbName = configuration["Heimdall:Mongo:DatabaseName"];
            _reportedSightingsCollection = mongoClient.GetDatabase(dbName).GetCollection<ReportedSighting>("ReportedSightings");
        }

        public ReportedSighting Add(ReportedSighting sighting)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ReportedSighting> FindByReportedCorrelationId(string reportedCorrelationId)
        {
            throw new NotImplementedException();
        }
    }
}