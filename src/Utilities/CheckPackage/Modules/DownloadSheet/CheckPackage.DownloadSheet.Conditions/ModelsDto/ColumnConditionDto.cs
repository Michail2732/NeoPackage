using CheckPackage.Core.Condition;
using CheckPackage.DownloadSheet.Entities;
using Package.Abstraction.Entities;
using Package.Localization;
using System;
using System.Linq;

namespace CheckPackage.DownloadSheet.Conditions
{
    public class ColumnConditionDto : ConditionInfo
    {
        public string Column { get; }
        public string? Value { get; set; }

        public ColumnConditionDto(string parameterId, string column) : base(parameterId)
        {
            if (string.IsNullOrEmpty(column))            
                throw new ArgumentException(nameof(column));            
            Column = column;
        }



        public override Result Validate(ConditionContext context)
        {
            if (context is null)            
                throw new ArgumentNullException(nameof(context));            
            if (string.IsNullOrEmpty(Column))
                return Result.Error(context.Messages.Get(MessageKeys.NotSetProperty, nameof(Column)));            
            if (!context.CurrentEntity.UserParameters.Any( a => a.Key == ParameterId && a.Value.GetType() == typeof(LoadlistRow)))
                return Result.Error(context.Messages.Get(MessageKeys.NotFoundCustomParameterInEntity, nameof(LoadlistRow)));
            if (!context.CurrentEntity.UserParameters.Any(a => a.Key == ParameterId && a.Value.As<LoadlistRow>().HasColumn(Column)))
                return Result.Error(context.Messages.Get(MessageKeys.NotFoundLoadlistColumn, Column));
            return Result.Success();
        }
    }
}
