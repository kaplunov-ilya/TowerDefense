using TowerDefence.Gameplay.Behaviour.Attack.Domain;
using TowerDefence.Gameplay.Entity.Domain;
using UniRx;

namespace TowerDefence.Gameplay.Entity
{
    public sealed class ActorContext
    {
        public ReactiveProperty<bool> IsAlive { get; } = new(true);

        public AttackContext AttackContext { get; } = new();
        
        public EEntitySide Side { get; set; }
    }
}