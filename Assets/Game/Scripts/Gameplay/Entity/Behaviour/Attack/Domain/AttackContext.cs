using System.Collections.Generic;
using TowerDefence.Core.Utils.Storage;
using TowerDefence.Gameplay.Behaviour.Effects.Contracts;
using TowerDefence.Gameplay.Entity;
using TowerDefence.Gameplay.Entity.Configs.Meta.Contract;

namespace TowerDefence.Gameplay.Behaviour.Attack.Domain
{
    public sealed class AttackContext
    {
        public Actor Target { get; set; } 
        public bool IsWindUp { get; set; }
        
        public List<IEffect> AttackEffects = new(8);

        public GroupStorage<IMeta> Meta { get; private set; } = new(4);
    }
}