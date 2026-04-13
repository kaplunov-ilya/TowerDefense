using System;
using TowerDefence.Gameplay.Behaviour.Contract;
using TowerDefence.Gameplay.Utils.BehaviourTree;
using TowerDefence.Gameplay.Utils.BehaviourTree.Node.Contract;

namespace TowerDefence.Gameplay.Behaviour.Move
{
    [Serializable]
    public sealed class MoveBehaviourNode : BehaviourNode
    {
        public override Type Type => typeof(MoveBehaviourNode);
        
        public override NodeStatus Tick(float deltaTime, float multiplier)
        {
            return NodeStatus.Running;
        }

        public override IBehaviour CloneBehaviour() => CloneSelf();
        public override INode CloneNode() => CloneSelf();

        private MoveBehaviourNode CloneSelf()
        {
            var node = new MoveBehaviourNode();
            return node;
        }
    }
}