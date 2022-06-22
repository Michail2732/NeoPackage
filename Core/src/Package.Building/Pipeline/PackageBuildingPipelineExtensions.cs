

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
        
        public static PackageBuildingPipeline CustomBuildItems(
            this PackageBuildingPipeline pipeline,
            CustomBuildItems customBuildItems)
        {
            pipeline.AddPipelineItem(customBuildItems);
            return pipeline;
        }
        
        public static PackageBuildingPipeline GroupItems(
            this PackageBuildingPipeline pipeline,
            IGroupingRule[] rules)
        {
            pipeline.AddPipelineItem(new GroupPackageItems(rules));
            return pipeline;
        }
        
        internal static bool HasLoop(this IBuildPipelineItem item, IBuildPipelineItem newNext)
        {
            var localCurrent = item;
            while (localCurrent != null)
            {
                if (ReferenceEquals(localCurrent, newNext))
                    return true;
                localCurrent = localCurrent.Next;
            }
            return false;
        }
        
    }
}