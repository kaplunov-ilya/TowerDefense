using System;
using TowerDefence.Gameplay.Entity.Domain;
using TowerDefence.Gameplay.Entity.Providers.Contracts;

namespace TowerDefence.Gameplay.Entity.Providers
{
    public sealed class HumanoidAnimationProvider : AnimatorProvider
    {
        public override Type EntityProviderType => typeof(IEntityAnimationProvider);

        public override void SetState(EAnimationState animationState, float targetTime)
        {
            switch (animationState)
            {
                case EAnimationState.Idle:
                   // _animator.SetLayerWeight(AnimationKeys.FullBodyLayer, 1f);
                    _animator.SetTrigger(AnimationKeys.IdleTrigger);
                    break;
                case EAnimationState.Moving:
                    _animator.SetBool(AnimationKeys.IsMoveFlag, true);
                    _animator.SetTrigger(AnimationKeys.MoveTrigger);
                    break;
                case EAnimationState.WindUp:
                    _animator.SetLayerWeight(AnimationKeys.FullBodyLayer, 0f);
                    _animator.SetBool(AnimationKeys.IsAttackFlag, true);
                    SetAnimation(targetTime, AnimationKeys.AttackSpeedFloat, AnimationKeys.WindUpTrigger,
                                 animationState, AnimationKeys.TopLayer).Forget();
                    break;
                case EAnimationState.Attacking:
                    _animator.SetLayerWeight(AnimationKeys.FullBodyLayer, 0f);
                    _animator.SetBool(AnimationKeys.IsAttackFlag, true);
                    SetAnimation(targetTime, AnimationKeys.AttackSpeedFloat, AnimationKeys.AttackTrigger,
                                 animationState, AnimationKeys.TopLayer).Forget();
                    break;
                case EAnimationState.Stunning:
                    _animator.SetLayerWeight(AnimationKeys.FullBodyLayer, 1f);
                    _animator.SetTrigger(AnimationKeys.StunTrigger);
                    break;
                case EAnimationState.Death:
                    _animator.SetLayerWeight(AnimationKeys.FullBodyLayer, 1f);
                    _animator.SetTrigger(AnimationKeys.DieTrigger);
                    break;
            }
        }

        public override void Cancel(EAnimationState animationState)
        {
            switch (animationState)
            {
                case EAnimationState.Idle:
                case EAnimationState.Stunning:
                case EAnimationState.Death:
                    _animator.SetLayerWeight(AnimationKeys.FullBodyLayer, 0f);
                    break;
                case EAnimationState.Moving:
                    _animator.SetBool(AnimationKeys.IsMoveFlag, false);
                    break;
                case EAnimationState.WindUp:
                case EAnimationState.Attacking:
                    _animator.SetBool(AnimationKeys.IsAttackFlag, false);
                    break;
            }

            _animator.SetTrigger(AnimationKeys.CancelTrigger);
        }

        public override void ForceCancel()
        {
            _animator.SetBool(AnimationKeys.IsMoveFlag, false);
            _animator.SetBool(AnimationKeys.IsAttackFlag, false);

            _animator.SetLayerWeight(AnimationKeys.FullBodyLayer, 0f);

            base.ForceCancel();
        }
    }
}