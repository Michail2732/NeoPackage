using System.Threading.Tasks;
using Package.Building.Context;
using Package.Domain.Factories;

namespace Package.Building.Pipeline   
{
    public interface IFillingRule
    {
        void Fill(
            PackageItemBuilder itemBuilder,
            PackageBuildingContext context);
        bool IsMatch(
            PackageItemBuilder itemBuilder,
            PackageBuildingContext context);
    }
}