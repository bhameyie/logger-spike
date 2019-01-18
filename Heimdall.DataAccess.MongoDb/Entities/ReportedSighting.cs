using Heimdall.DataAccess.Entities;
using MongoDB.Bson;

namespace Heimdall.DataAccess.MongoDb.Entities
{
    public class ReportedSighting:IReportedSighting
    {
        public ObjectId Id { get; set; }

    }
}