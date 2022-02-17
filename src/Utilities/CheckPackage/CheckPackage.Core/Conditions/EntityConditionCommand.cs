using CheckPackage.Core.Entities;
using Package.Abstraction.Entities;

namespace CheckPackage.Core.Conditions
{
    public abstract class EntityConditionCommand: IConditionCommand<Entity_>
    {
        public Logical Logic { get; set; }
        public bool Inverse { get; set; }
        public bool Recurse { get; set; }

        public bool Resolve(Entity_ entity, PackageContext context)
        {                        
            if (Recurse)
            {
                EntityStackEnumerable entityEnumerable = new EntityStackEnumerable(entity);
                foreach (var entity_ in entityEnumerable)
                    if (Inverse ? !InnerResolve(entity, context): InnerResolve(entity, context))
                        return true;
                return false;
            }
            return Inverse ? !InnerResolve(entity, context) : InnerResolve(entity, context);            
        }        


        protected abstract bool InnerResolve(Entity_ entity, PackageContext context);        
    }
}
