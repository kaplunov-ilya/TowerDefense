using TowerDefence.Gameplay.Behaviour.Contract;

namespace TowerDefence.Gameplay.Behaviour.Configs.Contracts
{
    public interface IBehaviourConfig
    {
        IBehaviour Create();
    }
}