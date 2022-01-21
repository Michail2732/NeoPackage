using CheckPackage.Core.Checks;
using CheckPackage.DownloadSheet.Entities;
using Package.Abstraction.Entities;
using Package.Localization;
using Package.Validation.Context;
using System.Collections.Generic;
using System.Linq;

namespace CheckPackage.DownloadSheet.Checks
{
    public abstract class LoadlistBaseCheckDto : CheckInfo
    {        
        public List<RowFilter>? RowFilters { get; set; }
        public ColumnFilter? ColumnFilter { get; set; }

        public LoadlistBaseCheckDto(string parameterId, string? errorMessage): base(parameterId, errorMessage)
        {
            RowFilters = new List<RowFilter>();
        }               

        protected Result ValidateProtected(ValidationContext context)
        {            
            if (RowFilters == null)
                return Result.Error(context.MessageBuilder.Get(MessageKeys.NotSetProperty, nameof(RowFilters)));
            var loadlist = context.CurrentEntity.UserParameters.FirstOrDefault(a => a.Value.GetType() == 
                            typeof(Loadlist) && a.Key == ParameterId).Value?.As<Loadlist>();
            if (loadlist == null)
                return Result.Error(context.MessageBuilder.Get(MessageKeys.NotFoundParameterInEntity, ParameterId));            
            if (loadlist.GetType() != typeof(Loadlist))
                return Result.Error(context.MessageBuilder.Get(MessageKeys.IncorrectTypeCustomParameter, 
                    ParameterId, typeof(Loadlist)));
            foreach (var filter in RowFilters!)            
                if (!loadlist.Columns.Any(a => a.ColumnName == filter.FilterColumnName))
                    return Result.Error(context.MessageBuilder.Get(MessageKeys.IncorrectLoadlistRowFilter, 
                        filter.FilterColumnName));            
            if (ColumnFilter!= null)
                foreach (var columnName in ColumnFilter.ColumnsName)                
                    if (!loadlist.Columns.Any(a => a.ColumnName == columnName))
                        return Result.Error(context.MessageBuilder.Get(MessageKeys.IncorrectLoadlistColumnsFilter,
                            columnName));                
            return Result.Success();
        }
        
    }
}
