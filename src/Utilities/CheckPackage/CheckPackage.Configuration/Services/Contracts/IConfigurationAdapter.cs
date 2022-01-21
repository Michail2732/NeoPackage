using System;
using System.Collections.Generic;
using System.Text;

namespace CheckPackage.Configuration.Services
{
    public interface IConfigurationAdapter<T>
    {
        IList<T> Get();
    }
}
