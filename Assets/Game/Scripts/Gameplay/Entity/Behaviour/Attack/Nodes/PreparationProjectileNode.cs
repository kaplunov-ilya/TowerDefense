using System;
using TowerDefence.Gameplay.Utils.BehaviourTree;
using TowerDefence.Gameplay.Utils.BehaviourTree.Domain;
using TowerDefence.Gameplay.Utils.BehaviourTree.Node;
using TowerDefence.Gameplay.Utils.BehaviourTree.Node.Contract;
using TowerDefence.Gameplay.Behaviour.Attack.Domain;
using TowerDefence.Gameplay.Entity.Configs.Meta;
using TowerDefence.Gameplay.Entity.Domain;
using TowerDefence.Gameplay.Entity.Providers.Contracts;

namespace TowerDefence.Gameplay.Behaviour.Attack.Nodes
{
    [Serializable]
    /// <summary> Получаем из пулла проджектайл </summary>
    public sealed class PreparationProjectileNode : Node<BehaviourActorContext>
    {
        private IProjectileSlotProvider _projectileSlotProvider;
        private ProjectileMetaConfig _projectileMetaConfig;
        
        private ProjectileMeta _projectileMeta = new();
        
        private AttackContext AttackContext => TypedContext.Actor.ActorContext.AttackContext;
        
        public override void Init(BehaviourContext context)
        {
            base.Init(context);
            
            TypedContext.Actor.ProvidersGroup.TryGet(typeof(IProjectileSlotProvider), out _projectileSlotProvider);
            TypedContext.Actor.ActorConfig.Meta.TryGet(typeof(ProjectileMetaConfig), out _projectileMetaConfig);
            
            AttackContext.Meta.Add(typeof(ProjectileMeta), _projectileMeta);
        }

        public override NodeStatus Tick(float deltaTime, float multiplier)
        {
            if(TypedContext.Actor == null)
                return ReturnStatus(NodeStatus.Failure);

            var result = TrySetProjectile();
            return ReturnStatus(result);
        }

        private NodeStatus TrySetProjectile()
        {
            return NodeStatus.Success;

            /*if(AttackContext.Projectile != null)
                return ReturnStatus(NodeStatus.Success);

            var projectile = TypedContext.PoolProjectiles.Get(TypedContext.Actor.ActorConfig.ProjectileConfig);
            AttackContext.Projectile = projectile;

            _projectileSlotProvider.SetBeginSlotProjectile(projectile);

            return ReturnStatus(AttackContext.Projectile == null ? NodeStatus.Failure : NodeStatus.Success);*/
        }
        
        public override INode CloneNode() => new PreparationProjectileNode();
    }
}