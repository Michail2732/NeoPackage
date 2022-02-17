using CheckPackage.Core.Checks;
using CheckPackage.DownloadSheet.Entities;
using CheckPackage.DownloadSheet.Extensions;
using CheckPackage.DownloadSheet.Service;
using Package.Abstraction.Entities;
using Package.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CheckPackage.DownloadSheet.Commands
{

    public class LoadlistStructureCheck : PackageCheckCommand
    {
        public IReadOnlyList<string> IdentificationColumns { get; }

        public LoadlistStructureCheck(IReadOnlyList<string> identificationColumns, 
            string message) : base(message)
        {
            IdentificationColumns = identificationColumns ?? throw new ArgumentNullException(nameof(identificationColumns));
        }
        
        //todo: messages
        protected override Result InnerCheck(Package_ package, PackageContext context)
        {
            if (IdentificationColumns.Count == 0)
                return Result.Error(context.Messages[MessageKeys.IdentificationColumnsCountMustBeLarge0]);
            StringBuilder errorSb = new StringBuilder();
            var loadlist = package.FindLoadlist();
            if (loadlist == null)
                return Result.Error(context.Messages[MessageKeys.NotFoundLoadlistFile, "todo"]);

            var loadlistFromPackage = MapToLoadlist(loadlist, package, context, errorSb);            
            List<LoadlistRow> matchedRows = new List<LoadlistRow>(loadlist.Rows.Count);
            foreach (var lRowFromPackage in loadlistFromPackage.Rows)
            {
                var matchRow = FindMatchRow(lRowFromPackage, loadlist.Rows);
                if (matchRow == null)
                {
                    errorSb.Append(context.Messages[MessageKeys.CouldNotMapLoadlistRow, "to do messages"] + "\n");
                    continue;
                }
                foreach (var col in loadlist.Columns)
                    if (lRowFromPackage[col.ColumnName] != matchRow[col.ColumnName])
                        errorSb.Append(context.Messages[MessageKeys.IncorrectValueInLoadlistColumn, matchRow.Index, col.ColumnName, lRowFromPackage[col.ColumnName]] + "\n");
                matchedRows.Add(matchRow);
            }

            var excessRows = loadlist.Rows.Except(matchedRows);
            foreach (var excessRow in excessRows)
                errorSb.Append(context.Messages[MessageKeys.ExcessLoadlistRow, excessRow.Index]);
            return new Result(errorSb.Length == 0, errorSb.ToString());
        }        

        private LoadlistRow? FindMatchRow(LoadlistRow rowFromPackage, IEnumerable<LoadlistRow> rows)
        {
            LoadlistRow? matchRow = null;
            foreach (var row in rows)
            {
                bool isMatch = true;
                foreach (var identityColumn in IdentificationColumns)                
                    if (!(rowFromPackage.HasColumn(identityColumn) && row.HasColumn(identityColumn)
                        && rowFromPackage[identityColumn] == row[identityColumn]))
                        isMatch = false;
                if (isMatch) matchRow = row;
            }
            return matchRow;
        }


        private Loadlist MapToLoadlist(Loadlist loadList, Package_ package, PackageContext context, 
            StringBuilder errorSb)
        {            
            Loadlist loadlistFromPackage = new Loadlist();
            foreach (var column in loadList.Columns)            
                loadList.AddColumn(column.ColumnName);            
            EntityStackEnumerable entityEnumerable = new EntityStackEnumerable(package.Entities);
            var mapper = context.GetService<ILoadlistRowMapper>();
            foreach (var packageEntity in entityEnumerable)
            {
                var mapEntity = mapper.MapToRow(packageEntity, loadlistFromPackage, context);
                if (!mapEntity.Result.IsSuccess)
                    errorSb.Append(context.Messages[MessageKeys.CouldNotMatchLoadlistRowWithEntity, packageEntity.Name]+"\n");                
            }                        
            return loadlistFromPackage;
        }
        
    }
}
