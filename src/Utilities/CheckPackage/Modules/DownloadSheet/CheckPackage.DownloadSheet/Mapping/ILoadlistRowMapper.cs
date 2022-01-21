using CheckPackage.DownloadSheet.Entities;
using Package.Abstraction.Entities;

namespace CheckPackage.DownloadSheet.Mapping
{
    public interface ILoadlistRowMapper
    {        

        EntityToRowMapResult MapToRow(PackageEntity entity, Loadlist loadlist);        
    }
}
