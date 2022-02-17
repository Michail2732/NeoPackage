using CheckPackage.Configuration.Json.Entities;
using Package.Abstraction.Entities;
using Package.Building.Builders;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;

namespace CheckPackage.PackageBuilding.Builders
{
    public class EntityBasisBuilder : IEntityBasisBuilder
    {               

        public IEnumerable<EntityBuildingResult> Build(PackageContext context)
        {
            return BuildPrivate(context, CancellationToken.None);
        }

        public async Task<IEnumerable<EntityBuildingResult>> BuildAsync(PackageContext context, CancellationToken ct)
        {
            return await Task.Run(() => BuildPrivate(context, ct), ct);
        }



        private IEnumerable<EntityBuildingResult> BuildPrivate(PackageContext context, CancellationToken ct)
        {
            ct.ThrowIfCancellationRequested();
            List<EntityBuildingResult> entities = new List<EntityBuildingResult>();
            var buildConfig = context.Configuration.Get<PackageConfigurationJson>();            
            var files = Directory.GetFiles(buildConfig.PackageDirectory, "*", SearchOption.AllDirectories);
            foreach (var file in files)
            {
                ct.ThrowIfCancellationRequested();
                Dictionary<string, string> parameters = new Dictionary<string, string>();
                FileInfo fileInfo = new FileInfo(file);
                parameters[nameof(fileInfo.FullName).ToLower()] = fileInfo.FullName;                
                parameters[nameof(fileInfo.Name).ToLower()] = fileInfo.Name;
                parameters[nameof(fileInfo.Extension).ToLower()] = fileInfo.Extension;
                parameters[nameof(fileInfo.Length).ToLower()] = fileInfo.Length.ToString();
                parameters[nameof(fileInfo.CreationTime).ToLower()] = fileInfo.CreationTime.ToString();
                parameters[nameof(fileInfo.CreationTimeUtc).ToLower()] = fileInfo.CreationTimeUtc.ToString();
                parameters[nameof(fileInfo.DirectoryName).ToLower()] = fileInfo.DirectoryName;
                parameters[nameof(fileInfo.Exists).ToLower()] = fileInfo.Exists.ToString();
                parameters[nameof(fileInfo.IsReadOnly).ToLower()] = fileInfo.IsReadOnly.ToString();
                parameters[nameof(fileInfo.LastWriteTime).ToLower()] = fileInfo.LastWriteTime.ToString();
                parameters[nameof(fileInfo.LastAccessTimeUtc).ToLower()] = fileInfo.LastAccessTimeUtc.ToString();
                parameters[nameof(fileInfo.LastAccessTime).ToLower()] = fileInfo.LastAccessTime.ToString();
                parameters[nameof(fileInfo.LastWriteTimeUtc).ToLower()] = fileInfo.LastWriteTimeUtc.ToString();
                if (buildConfig.IsCheckHash)
                    using (SHA256 sha = SHA256.Create())
                    using (FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read))
                        parameters["hash"] = BitConverter.ToString(sha.ComputeHash(fs)).Replace("-", "");
                entities.Add(new EntityBuildingResult(parameters, fileInfo.Name, fileInfo.FullName));
            }
            return entities;
        }

    }
}
