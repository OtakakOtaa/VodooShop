using Cysharp.Threading.Tasks;

namespace CodeBase.Infrastructure
{
    public interface IApplicationGates
    {
        void Exit();
        UniTask LoadMainMenu();
        UniTask LoadShopHall();
    }
}