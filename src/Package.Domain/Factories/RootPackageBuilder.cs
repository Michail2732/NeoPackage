

namespace Package.Domain.Factories
{

    public class RootPackageBuilder : PackageEntityBuilder
    {
        public RootPackage Build() => new RootPackage(
            Id ?? "",
            Name ?? "",
            _children);

    }
}