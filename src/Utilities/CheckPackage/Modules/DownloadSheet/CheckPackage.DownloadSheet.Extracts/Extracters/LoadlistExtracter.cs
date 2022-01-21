using CheckPackage.Core.Extracts;
using CheckPackage.DownloadSheet.Entities;
using CheckPackage.DownloadSheet.Mapping;
using Microsoft.Extensions.Logging;
using Package.Abstraction.Entities;
using Package.Localization;
using System;
using System.Linq;

namespace CheckPackage.DownloadSheet.Extracters
{
    public class LoadlistExtracter : ParameterExtracter<LoadlistExtractDto>
    {                
        private readonly ILoadlistRowMapper _mapper;
        private readonly MappingContextBuilder _mappingContextBuldr;
        private readonly ILogger<LoadlistExtracter> _logger;

        public LoadlistExtracter(ILoadlistRowMapper mapper, MappingContextBuilder mappingContextBuldr,
            ILogger<LoadlistExtracter> logger)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _mappingContextBuldr = mappingContextBuldr ?? throw new ArgumentNullException(nameof(mappingContextBuldr));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        protected override ExtractValue ExtractProtected(LoadlistExtractDto info, ExtractContext context)
        {
            Loadlist loadlist = new Loadlist();
            var mappingContext = _mappingContextBuldr.Build();
            var colMaps = mappingContext.ColumnMapRepository.Get();
            foreach (var colMap in colMaps)
                loadlist.AddColumn(colMap.Name);
            foreach (var entity in context.CurrentChildren)
            {
                PackageEntityStackEnumerable entitiesEnumerable = new PackageEntityStackEnumerable(entity);
                foreach (var entityInner in entitiesEnumerable)
                {
                    var mappingResult = _mapper.MapToRow(entityInner, loadlist);
                    if (!mappingResult.Result.IsSuccess)
                    {
                        _logger.LogError(mappingResult.Result.Details);
                        return new ExtractValue();
                    }                        
                }
            }            
            return new ExtractValue(new ParameterResult(info.ParameterId, loadlist));
        }                
    }
}
