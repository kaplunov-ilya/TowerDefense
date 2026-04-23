using TowerDefence.Core.Services.MessageBus;
using TowerDefence.Gameplay.Utils.BehaviourTree;
using TowerDefence.Core.Utils.Storage;
using TowerDefence.Gameplay.Behaviour.Contract;
using TowerDefence.Gameplay.Entity.Configs;
using TowerDefence.Gameplay.Entity.Domain;
using TowerDefence.Gameplay.Entity.Providers.Contracts;
using TowerDefence.Gameplay.Entity.Stats.Contracts;

namespace TowerDefence.Gameplay.Entity
{
    public sealed class Actor
    {
        public Actor(GroupStorage<IEntityProvider> providers,
                     GroupStorage<IResource> resources, 
                     GroupStorage<IAttribute> attributes, 
                     GroupStorage<IBehaviour> behaviours, 
                     BehaviourController behaviourController, 
                     ActorView view, 
                     ActorConfig actorConfig,
                     ActorServiceLocator locator)
        {
            ProvidersGroup = providers;
            AttributesGroup = attributes;
            BehavioursGroup = behaviours;
            ResourcesGroup = resources;
            BehaviourController = behaviourController;
            View = view;
            ActorConfig = actorConfig;
            Locator = locator;
        }
        
        public ActorServiceLocator Locator { get; } 

        public ActorView View { get; set; }
        public ActorContext ActorContext { get; } = new();
        
        public BehaviourController BehaviourController { get; }
        
        public GroupStorage<IEntityProvider> ProvidersGroup { get; }
        public GroupStorage<IResource> ResourcesGroup { get; }
        public GroupStorage<IAttribute> AttributesGroup { get; }
        public GroupStorage<IBehaviour> BehavioursGroup { get; }
        
        public ActorConfig ActorConfig { get; } 

        public void SetEnableBehaviours(bool enable)
        {
            foreach (var behaviour in BehavioursGroup.Dictionary.Values)
            {
                if(enable)
                    behaviour.SetEnable();
                else
                    behaviour.SetDisable();
            }
        }
    }
}