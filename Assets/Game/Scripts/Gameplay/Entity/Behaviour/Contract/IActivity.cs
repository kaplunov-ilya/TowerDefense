using TowerDefence.Gameplay.Entity;

namespace TowerDefence.Gameplay.Behaviour.Contract
{
    /// <summary> Внутренние возможности Behaviour </summary>
    public interface IActivity
    {
        Actor Owner  { get; }
    }
}