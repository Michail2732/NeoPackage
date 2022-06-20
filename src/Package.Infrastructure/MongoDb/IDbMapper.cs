using MongoDB.Bson;

namespace Package.Infrastructure.MongoDb
{
    public interface IDbMapper<TItem>: IDbCapabilityMapper 
        where TItem: class
    {
        TItem Map(BsonDocument document);
    }
}