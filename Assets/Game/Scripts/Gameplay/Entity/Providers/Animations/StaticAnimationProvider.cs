using System;
using TowerDefence.Gameplay.Entity.Domain;
using TowerDefence.Gameplay.Entity.Providers.Contracts;

namespace TowerDefence.Gameplay.Entity.Providers
{
    public sealed class StaticAnimationProvider : AnimatorProvider
    {
        public override Type EntityProviderType => typeof(IEntityAnimationProvider);
        
        public override void SetState(EAnimationState animationState, float targetTime)
        {
            switch (animationState)
            {
                case EAnimationState.Idle:
                    _animator.SetTrigger(AnimationKeys.IdleTrigger);
                    break;
                case EAnimationState.WindUp:
                    _animator.SetBool(AnimationKeys.IsAttackFlag, true);
                    SetAnimation(targetTime, AnimationKeys.AttackSpeedFloat, AnimationKeys.WindUpTrigger, animationState, 0).Forget();
                    break;
                case EAnimationState.Attacking:
                    _animator.SetBool(AnimationKeys.IsAttackFlag, true);
                    SetAnimation(targetTime, AnimationKeys.AttackSpeedFloat, AnimationKeys.AttackTrigger, animationState, 0).Forget();
                    break;
                case EAnimationState.Stunning:
                    _animator.SetTrigger(AnimationKeys.StunTrigger);
                    break;
                case EAnimationState.Death:
                    _animator.SetTrigger(AnimationKeys.DieTrigger);
                    break;
            }
        }
        
        public override void Cancel(EAnimationState animationState)
        {
            switch (animationState)
            {
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
            
            base.ForceCancel();
        }
    }
}