using TowerDefence.Core.Services.Pool.Contract;
using TowerDefence.Gameplay.Utils.BehaviourTree.Domain;
using TowerDefence.Gameplay.Behaviour.Attack.Domain;
using VContainer;

namespace TowerDefence.Gameplay.Entity.Domain
{
    public sealed class BehaviourActorContext : BehaviourContext
    {
        [Inject] public IPoolProjectiles PoolProjectiles { get; private set; }
        
        public Actor Actor { get; set; }
        
        public AttackContext AttackContext { get; set; } = new();
    }
}