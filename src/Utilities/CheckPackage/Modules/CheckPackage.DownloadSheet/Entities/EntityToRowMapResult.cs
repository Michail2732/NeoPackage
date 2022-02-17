using CheckPackage.DownloadSheet.Entities;
using Package.Abstraction.Entities;
using Package.Building.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckPackage.DownloadSheet.Entities
{
    public readonly struct EntityToRowMapResult
    {
        public readonly LoadlistRow? Row;
        public readonly Result Result;

        public EntityToRowMapResult(LoadlistRow? row, Result result)
        {
            Row = row;
            Result = result;
        }

        public static EntityToRowMapResult Error(string error) => new EntityToRowMapResult(null, new Result(false, error));
        public static EntityToRowMapResult Success(LoadlistRow row) => new EntityToRowMapResult(row, new Result(true, null));
    }
}
