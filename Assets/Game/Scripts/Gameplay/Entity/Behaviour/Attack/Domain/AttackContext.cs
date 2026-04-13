using System.Collections.Generic;
using TowerDefence.Gameplay.Behaviour.Defence.Contracts;
using TowerDefence.Gameplay.Behaviour.Effects.Contracts;

namespace TowerDefence.Gameplay.Behaviour.Attack.Domain
{
    public sealed class AttackContext
    {
        public ITargetableBehaviour Target { get; set; } 
       // public IProjectile Projectile { get; set; }
        public bool IsWindUp { get; set; }
        
        public List<IEffect> AttackEffects = new(8);
    }
}