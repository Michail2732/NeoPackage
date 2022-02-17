using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Package.Configuration.Services
{
    /// <summary>
    /// связыватель объекта конфигурации из объекта корня конфигурации
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IJsonConfigurationBinder<T>
    {
        T Get(JObject configurationRoot);
        void Set(T item, JObject configuration);
    }    
}
