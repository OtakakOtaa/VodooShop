using CodeBase.Configuration.Data.MainConfig;

namespace CodeBase.Level
{
    public sealed class DayCustomersPool
    {
        private readonly IMainConfigProvider _config;
        
        public DayCustomersPool(IMainConfigProvider config)
        {
            _config = config;
        }
    }
}