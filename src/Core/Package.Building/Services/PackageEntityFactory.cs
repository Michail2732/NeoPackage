using Package.Abstraction.Entities;
using Package.Building.Builders;
using Package.Building.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Package.Building.Services
{
    public class PackageEntityFactory
    {
        public static Entity_ Create(EntityBuildingResult buildRessult, List<Entity_> children)
        {
            return new Entity_(buildRessult.Id, buildRessult.Name, children,
                buildRessult.Parameters.Copy(), buildRessult.UserParameters.Copy());
        }

        public static Package_ Create(PackageBuildingResult buildResult, List<Entity_> items)
        {
            return new Package_(buildResult.Id, buildResult.Name, items);
        }
    }
}
