using TowerDefence.Gameplay.Utils.BehaviourTree.Domain;

namespace TowerDefence.Gameplay.Utils.BehaviourTree.Node.Contract
{
    public interface INode
    {
        NodeStatus Status { get; }
        
        public EAbortMode AbortMode { get; }

        void Init(BehaviourContext context);
        NodeStatus Tick(float deltaTime, float multiplier);
        void Abort();

        INode CloneNode();
    }
}