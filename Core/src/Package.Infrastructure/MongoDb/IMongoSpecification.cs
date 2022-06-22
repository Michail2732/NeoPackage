using MongoDB.Bson;

namespace Package.Infrastructure.MongoDb
{
    public interface IMongoSpecification
    {
        BsonDocument GetSpecification();
    }
}