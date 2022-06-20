using System.Threading;
using System.Threading.Tasks;
using Package.Building.Context;

namespace Package.Building.Pipeline
{
    internal interface IBuildPipelineItem
    {
        IBuildPipelineItem? Next { get; set; }
        void Invoke(PackageBuildingContext context);
    }
}