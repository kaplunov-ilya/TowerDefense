using System;
using TowerDefence.Gameplay.Behaviour.Contract;
using TowerDefence.Gameplay.Entity;

namespace TowerDefence.Gameplay.Behaviour
{
    public abstract class BaseActivity : IActivity
    {
        public virtual Type Type => typeof(BaseActivity);
        public Actor Owner { get; private set; }
        public void Init(Actor owner) => Owner = owner;
        public virtual IActivity CloneActivity() => this;
    }
}