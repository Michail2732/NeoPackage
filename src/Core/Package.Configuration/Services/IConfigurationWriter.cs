using System;
using System.Collections.Generic;
using System.Text;

namespace Package.Configuration.Services
{
    public interface IConfigurationWriter
    {
        ///// <summary>
        ///// сохраняет элемень в конфигурацию
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="item"></param>
        void Set<T>(T item) where T: class;
    }
}
