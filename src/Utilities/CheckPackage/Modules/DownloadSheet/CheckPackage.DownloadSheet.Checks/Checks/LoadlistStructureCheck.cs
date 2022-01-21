using CheckPackage.Core.Checks;
using CheckPackage.DownloadSheet.Checks.Extensions;
using CheckPackage.DownloadSheet.Entities;
using CheckPackage.DownloadSheet.Extensions;
using CheckPackage.DownloadSheet.Mapping;
using Package.Abstraction.Entities;
using Package.Localization;
using Package.Validation.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CheckPackage.DownloadSheet.Checks
{

    public class LoadlistStructureCheck : PackageEntityCheckCommand<LoadlistStructureCheckDto>
    {
        private readonly ILoadlistRowMapper _mapper;        

        public LoadlistStructureCheck(ILoadlistRowMapper mapper) 
        {            
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        protected override Result CheckProtected(LoadlistStructureCheckDto checkInfo, ValidationContext context)
        {            
            StringBuilder errorSb = new StringBuilder();
            var loadlist = context.CurrentEntity.UserParameters.
                First(a => a.Value.GetType() == typeof(Loadlist)).Value.As<Loadlist>();            
            var loadlistFromPackage = MapToLoadlist(loadlist, context, errorSb);
            // фильтруем столбцы и строки
            IEnumerable<LoadlistRow> lrows = checkInfo.RowFilters.FilterOut(loadlist.Rows);
            IEnumerable<LoadlistColumn> lcols = checkInfo.ColumnFilter.FilterOut(loadlist.Columns);                

            List<LoadlistRow> matchedRows = new List<LoadlistRow>(loadlist.Rows.Count);
            foreach (var lRowFromPackage in loadlistFromPackage.Rows)
            {                
                var matchRow = FindMatchRow(lRowFromPackage, lrows, checkInfo);
                if (matchRow == null)
                {
                    errorSb.Append(context.MessageBuilder.Get(MessageKeys.CouldNotMapLoadlistRow, checkInfo.GetIdentity(lRowFromPackage)) + "\n");
                    continue;
                }                
                foreach (var col in lcols)                
                    if (lRowFromPackage[col.ColumnName] != matchRow[col.ColumnName])
                        errorSb.Append(context.MessageBuilder.Get(MessageKeys.IncorrectValueInLoadlistColumn, matchRow.Index, col.ColumnName, lRowFromPackage[col.ColumnName]) + "\n");                
                matchedRows.Add(matchRow);
            }

            var excessRows = lrows.Except(matchedRows);
            foreach (var excessRow in excessRows)            
                errorSb.Append(context.MessageBuilder.Get(MessageKeys.ExcessLoadlistRow, excessRow.Index));            

            return new Result(errorSb.Length == 0, errorSb.ToString());
        }

        private LoadlistRow? FindMatchRow(LoadlistRow rowFromPackage, IEnumerable<LoadlistRow> rows, 
            LoadlistStructureCheckDto checkInfo)
        {
            LoadlistRow? matchRow = null;
            foreach (var row in rows)
            {
                bool isMatch = true;
                foreach (var identityColumn in checkInfo.IdentificationColumns)                
                    if (!(rowFromPackage.HasColumn(identityColumn) && row.HasColumn(identityColumn)
                        && rowFromPackage[identityColumn] == row[identityColumn]))
                        isMatch = false;
                if (isMatch) matchRow = row;
            }
            return matchRow;
        }


        private Loadlist MapToLoadlist(Loadlist loadList, ValidationContext context, StringBuilder errorSb)
        {
            var rootEntity = context.CurrentEntity;
            Loadlist loadlistFromPackage = new Loadlist();
            foreach (var column in loadList.Columns)            
                loadList.AddColumn(column.ColumnName);            
            PackageEntityStackEnumerable entityEnumerable = new PackageEntityStackEnumerable(rootEntity);            
            foreach (var packageEntity in entityEnumerable)
            {
                var mapEntity = _mapper.MapToRow(packageEntity, loadlistFromPackage);
                if (!mapEntity.Result.IsSuccess)
                    errorSb.Append(context.MessageBuilder.Get(MessageKeys.CouldNotMatchLoadlistRowWithEntity, packageEntity.Name)+"\n");                
            }                        
            return loadlistFromPackage;
        }

       
    }
}
