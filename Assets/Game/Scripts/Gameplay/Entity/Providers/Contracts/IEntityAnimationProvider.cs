using TowerDefence.Gameplay.Entity.Domain;

namespace TowerDefence.Gameplay.Entity.Providers.Contracts
{
    public interface IEntityAnimationProvider : IEntityProvider
    {
        void SetState(EAnimationState animationState, float targetTime = 0);

        void Cancel(EAnimationState animationState);
        void ForceCancel();
        void UpdateMultiply(float multiplier);
    }
}