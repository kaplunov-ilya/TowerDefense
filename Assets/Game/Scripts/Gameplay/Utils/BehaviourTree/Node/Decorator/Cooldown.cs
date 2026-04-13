using System;
using TowerDefence.Gameplay.Utils.BehaviourTree.Node.Contract;
using UnityEngine;

namespace TowerDefence.Gameplay.Utils.BehaviourTree.Node.Decorator
{
    [Serializable]
    public class Cooldown : Node
    {
        [SerializeField] private float _cooldown;
        
        private float _nowValue;
        
        public Cooldown(){}
        
        public Cooldown(float cooldown) => _cooldown = cooldown;
        

        public override NodeStatus Tick(float deltaTime, float multiplier)
        {
            _nowValue += deltaTime;

            if (_nowValue >= _cooldown)
            {
                _nowValue = 0;
                return ReturnStatus(NodeStatus.Success);
            }
            
            return ReturnStatus(NodeStatus.Running);
        }

        public void Reset() => _nowValue = 0;

        public void SetCooldown(float cooldown) => _cooldown = cooldown;

        public override void Abort()
        {
            base.Abort();

            Reset();
        }

        public override INode CloneNode() => new Cooldown(_cooldown);
    }
}