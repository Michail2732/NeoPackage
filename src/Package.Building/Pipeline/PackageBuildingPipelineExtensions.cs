using System;
using System.Threading;
using System.Threading.Tasks;
using Package.Domain;
using Package.Building.Context;

namespace Package.Building.Pipeline
{
    public static class PackageBuildingPipelineExtensions
    {
        public static PackageBuildingPipeline BuildItems(
            this PackageBuildingPipeline pipeline,
            IFillingRule[] rules)
        {
            pipeline.AddPipelineItem(new BuildPackageItems(rules));
            return pipeline;
        }
        
        public static PackageBuildingPipeline GroupItems(
            this PackageBuildingPipeline pipeline,
            IGroupingRule[] rules)
        {
            pipeline.AddPipelineItem(new GroupPackageItems(rules));
            return pipeline;
        }
        
    }
}