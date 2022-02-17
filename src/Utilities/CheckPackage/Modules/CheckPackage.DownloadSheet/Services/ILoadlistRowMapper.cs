using CheckPackage.DownloadSheet.Entities;
using Package.Abstraction.Entities;

namespace CheckPackage.DownloadSheet.Service
{
    public interface ILoadlistRowMapper
    {        
        EntityToRowMapResult MapToRow(Entity_ entity, Loadlist loadlist, PackageContext context);        
    }
}
