using System;
using TowerDefence.Gameplay.Utils.BehaviourTree;
using TowerDefence.Gameplay.Utils.BehaviourTree.Node;
using TowerDefence.Gameplay.Utils.BehaviourTree.Node.Contract;

namespace TowerDefence.Gameplay.Behaviour.Nodes
{
    [Serializable]
    public sealed class MoveNode : Node
    {
        public override NodeStatus Tick(float deltaTime, float multiplier)
        {
            return ReturnStatus(NodeStatus.Success);
        }

        public override INode CloneNode() => new MoveNode();
    }
}