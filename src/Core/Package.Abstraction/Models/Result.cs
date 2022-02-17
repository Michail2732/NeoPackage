using System;
using System.Collections.Generic;
using System.Text;

namespace Package.Abstraction.Entities
{
    public readonly struct  Result
    {
        public readonly bool IsSuccess;
        public readonly string? Details;

        public Result(bool success, string? details)
        {
            IsSuccess = success;
            Details = details;
        }

        public static Result Error(string? details = null) => new Result(false, details);
        public static Result Success(string? details = null) => new Result(true, details);
    }

    public readonly struct Result<T> where T : class
    {
        public readonly bool IsSuccess;
        public readonly string? Details;
        public readonly T Object;

        public Result(bool isSuccess, string? details, T @object)
        {
            IsSuccess = isSuccess;
            Details = details;
            Object = @object ?? throw new ArgumentNullException(nameof(@object));
        }
    }
}
