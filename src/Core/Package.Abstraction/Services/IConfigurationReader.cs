using Package.Abstraction.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Package.Abstraction.Services
{
    public interface IConfigurationReader
    {        
        /// <summary>
        /// Заполняет элемент конфигурации значениями из конфигурации
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        T Get<T>() where T: class;        
    }
}
