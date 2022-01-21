using Package.Abstraction.Entities;
using Package.Localization;
using Package.Resourcing.Resources;
using System;

namespace CheckPackage.DownloadSheet.Mapping
{
    public sealed class MappingContext
    {        
        public MessagesService Messages { get; }
        public IResourceStorage<ColumnMappingResource, string> ColumnMapRepository { get; }
        public IResourceStorage<RowMappingResource, string> RowMapRepository { get; }

        public MappingContext(MessagesService messages, IResourceStorage<ColumnMappingResource, string> columnMapRepository, 
            IResourceStorage<RowMappingResource, string> rowMapRepository)
        {
            Messages = messages ?? throw new ArgumentNullException(nameof(messages));
            ColumnMapRepository = columnMapRepository ?? throw new ArgumentNullException(nameof(columnMapRepository));
            RowMapRepository = rowMapRepository ?? throw new ArgumentNullException(nameof(rowMapRepository));
        }
    }
}
