using TowerDefence.Gameplay.Utils.BehaviourTree;
using TowerDefence.Core.Utils.Storage;
using TowerDefence.Gameplay.Behaviour.Contract;
using TowerDefence.Gameplay.Entity.Configs;
using TowerDefence.Gameplay.Entity.Domain;
using TowerDefence.Gameplay.Entity.Providers.Contracts;
using TowerDefence.Gameplay.Entity.Stats.Contracts;
using VContainer;
using Object = UnityEngine.Object;

namespace TowerDefence.Gameplay.Entity.Factory
{
    public sealed class FactoryActor
    {
        [Inject] private readonly IObjectResolver _resolver;
        
        public Actor CreateActor(ActorConfig config)
        {
            var view = CreateView(config);
            
            var attributes = config.AttributesConfigs.Create();
            var resources = config.ResourceConfigs.Create();

            var behavioursGroup = new GroupStorage<IBehaviour>(config.BehavioursConfigs.Count);
            var entityProviderGroup = new GroupStorage<IEntityProvider>(view.EntityProviders.Count);
            var attributesGroup = new GroupStorage<IAttribute>(attributes.Length);
            var resourcesGroup = new GroupStorage<IResource>(resources.Length);

            foreach (var behaviour in config.BehavioursConfigs)
            {
                var value = behaviour.Create();
                behavioursGroup.Add(value.Type, value);
            }

            foreach (var provider in view.EntityProviders)
            {
                entityProviderGroup.Add(provider.EntityProviderType, provider);
            }

            foreach (var attribute in attributes)
            {
                attributesGroup.Add(attribute.GetType(), attribute);
            }

            foreach (var resource in resources)
            {
                resourcesGroup.Add(resource.GetType(), resource);
            }
            
            var node = config.ActorBehaviourTreeConfig.Create();
            var controller = _resolver.Resolve<BehaviourController>();
            var actor = new Actor(entityProviderGroup, resourcesGroup, attributesGroup, behavioursGroup, controller, view, config);
            
            var context = _resolver.Resolve<BehaviourActorContext>();
            context.Actor = actor;
            context.Controller = controller;
            context.AttackContext = actor.ActorContext.AttackContext;

            foreach (var behaviour in actor.BehavioursGroup.Dictionary.Values)
            {
               behaviour.Init(actor);
            }
            
            controller.Init(node, context);
            
            return actor;
        }

        private ActorView CreateView(ActorConfig config)
        {
            // TODO: создавать через ресурсы
            var view = Object.Instantiate(config.ActorViewPrefab);
            return view;
        }
    }
}