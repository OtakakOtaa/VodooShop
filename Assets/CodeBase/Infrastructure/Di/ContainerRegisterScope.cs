using VContainer;

namespace CodeBase.Infrastructure.Di
{
    public abstract class ContainerRegisterScope
    {
        public abstract void Configure(IContainerBuilder builder);
    }
}