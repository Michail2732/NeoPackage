using Package.Abstraction.Entities;
using Package.Localization;
using System;

namespace CheckPackage.DownloadSheet.Mapping
{
    public class MappingContextBuilder
    {
        private readonly IResourceStoragesProvider _resources;
        private readonly MessagesService _messages;

        public MappingContextBuilder(IResourceStoragesProvider resources, MessagesService messages)
        {
            _resources = resources ?? throw new ArgumentNullException(nameof(resources));
            _messages = messages ?? throw new ArgumentNullException(nameof(messages));
        }

        public  MappingContext Build()
        {
            return new MappingContext(_messages,
                _resources.GetStorage<ColumnMappingResource, string>(),
                _resources.GetStorage<RowMappingResource, string>());
        }
    }
}
