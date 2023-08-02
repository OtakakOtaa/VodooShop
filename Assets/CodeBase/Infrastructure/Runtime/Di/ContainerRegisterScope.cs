using VContainer;

namespace CodeBase.Infrastructure.Runtime.Di
{
    public abstract class ContainerRegisterScope
    {
        public abstract void Configure(IContainerBuilder builder);
    }
}