using System;
using System.Collections.Generic;
using System.Text;

namespace Package.Abstraction.Extensions
{
    public static class StringExtensions
    {

        public static string EmptyIfNull(this string? str)
        {
            return str == null ? string.Empty : str;
        }
    }
}
