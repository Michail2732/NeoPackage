using CheckPackage.Core.Entities;
using Package.Abstraction.Entities;

namespace CheckPackage.Core.Conditions
{
    public abstract class PackageEntityConditionCommand: IConditionCommand<PackageEntity>
    {
        public LogicalOperator Logic { get; set; }
        public bool Inverse { get; set; }
        public bool Recurse { get; set; }

        public bool Resolve(PackageEntity entity, PackageContext context)
        {                        
            if (Recurse)
            {
                PackageEntityStackEnumerable entityEnumerable = new PackageEntityStackEnumerable(entity);
                foreach (var entity_ in entityEnumerable)
                    if (Inverse ? !InnerResolve(entity, context): InnerResolve(entity, context))
                        return true;
                return false;
            }
            return Inverse ? !InnerResolve(entity, context) : InnerResolve(entity, context);            
        }        


        protected abstract bool InnerResolve(PackageEntity entity, PackageContext context);        
    }
}
