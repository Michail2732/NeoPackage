using MongoDB.Bson;

namespace Package.Infrastructure.MongoDb
{
    public interface IDbCapabilityMapper
    {
        bool CanMap(BsonDocument document);
    }
}