using TowerDefence.Gameplay.Behaviour.Attack.Contracts;
using TowerDefence.Gameplay.Behaviour.Attack.Domain;
using TowerDefence.Gameplay.Behaviour.Contract;

namespace TowerDefence.Gameplay.Behaviour.Attack.Activity.Initiator
{
    public sealed class AttackInitiator : BaseActivity, IAttackInitiator
    {
        public AttackSource GetData()
        {
            throw new System.NotImplementedException();
        }

        public override IActivity CloneActivity() => new AttackInitiator();
    }
}