using System.Numerics;
using TowerDefence.Gameplay.Entity;

namespace TowerDefence.Gameplay.Behaviour.Attack.Domain
{
    public readonly struct AttackSource
    {
        public Actor Target { get; } 
        public Vector3 TargetPosition { get; }
    }
}