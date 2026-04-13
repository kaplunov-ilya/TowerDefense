using TowerDefence.Gameplay.Behaviour.Contract;
using UniRx;
using UnityEngine;

namespace TowerDefence.Gameplay.Behaviour.Defence.Contracts
{
    public interface IDamageBehaviour : IBehaviour
    {
        Transform Transform { get; }
        IReadOnlyReactiveProperty<bool> IsAlive { get; }

        void TakeAttack(float damage);
    }
}