using Cysharp.Threading.Tasks;

namespace CodeBase.Infrastructure.Runtime.Contracts
{
    public interface IApplicationGates
    {
        void Exit();
        UniTask LoadMainMenu();
        UniTask LoadShopHall();
    }
}