using TowerDefence.Gameplay.Entity;

namespace TowerDefence.Gameplay.Behaviour.Effects.Contracts
{
    public interface IEffect
    {
        void Apply(Actor owner, Actor target);
        void Discard();
        void Tick(float deltaTime);
    }
}