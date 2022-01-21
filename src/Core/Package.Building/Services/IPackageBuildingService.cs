using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Package.Abstraction.Entities;

namespace Package.Building.Services
{
    public interface IPackageBuildingService
    {
        /// <summary>
        /// Совершает сборку пакета
        /// </summary>
        /// <returns></returns>
        /// <exception cref="PackageBuildingException">Если возникла ошибка в результате сборки пакета</exception>
        Package_ Create();
        /// <summary>
        /// Совершает сборку пакета асинхронно
        /// </summary>
        /// <param name="ct"></param>
        /// <returns></returns>
        /// <exception cref="PackageBuildingException">Если возникла ошибка в результате сборки пакета</exception>
        Task<Package_> CreateAsync(CancellationToken ct);
        /// <summary>
        /// Срабатывает после построения одного уровня элементов
        /// </summary>
        event EventHandler<LevelBuildEventArgs> LevelBuilded;
    }
}
