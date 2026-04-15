using System;
using TowerDefence.Gameplay.Entity;

namespace TowerDefence.Gameplay.Behaviour.Contract
{
    /// <summary> Внутренние возможности Behaviour </summary>
    public interface IActivity
    {
        Type Type { get; }
        
        Actor Owner  { get; }
        
        void Init(Actor owner);
        
        IActivity CloneActivity();
    }
}