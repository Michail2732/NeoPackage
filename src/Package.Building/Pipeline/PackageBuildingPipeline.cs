using System;
using System.Collections.Generic;

namespace Package.Building.Pipeline
{
    public class PackageBuildingPipeline
    {
        internal IBuildPipelineItem? Current { get; private set; }

        internal readonly Dictionary<string, object> Parammeters = new Dictionary<string, object>();

        public int PipelineLength { get; private set; }
        
        public void AddParameter<T>(string key, T value)
        {
            if (value is null)
                throw new ArgumentException(nameof(value));
            Parammeters[key] = value;
        }

        internal void AddPipelineItem(IBuildPipelineItem item)
        {
            if (Current == null)
                Current = item;
            else
            {
                Current.Next = item;
                Current = Current.Next;
            }

            PipelineLength++;
        }
    }
}