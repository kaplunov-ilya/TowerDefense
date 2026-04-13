namespace TowerDefence.Gameplay.Utils.BehaviourTree
{
    public abstract class NodeContext
    {
        public bool IsCancelled { get; set; } = false;
    }
}