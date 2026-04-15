using TowerDefence.Gameplay.Entity;

namespace TowerDefence.Gameplay.Behaviour.Attack.Contracts
{
    /// <summary>
    /// висит на проджектайле или внутри определенных Delivery 
    /// просчитывает кому и как нанести урон, а затем отправляет в message buss 
    /// </summary>
    public interface IDamageApplicator
    {
        void SetOwner(Actor owner);
    }
}