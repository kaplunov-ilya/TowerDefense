using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using TowerDefence.Gameplay.Entity.Domain;
using TowerDefence.Gameplay.Entity.Providers.Contracts;
using UnityEngine;

namespace TowerDefence.Gameplay.Entity.Providers
{
    public abstract class AnimatorProvider : EntityProvider, IEntityAnimationProvider
    {
        [SerializeField] protected Animator _animator;
        
        private readonly Dictionary<EAnimationState, float> _animationsLength = new();
        
        public abstract void SetState(EAnimationState animationState, float targetTime);

        public abstract void Cancel(EAnimationState animationState);

        public virtual void ForceCancel()
        {
            _animator.SetTrigger(AnimationKeys.CancelAllTrigger);
        }

        public void UpdateMultiply(float multiplier)
        {
            _animator.speed = multiplier;
        }
        
        protected async UniTaskVoid SetAnimation(float targetTime, string floatFlag, string stringTrigger, EAnimationState state, int layer)
        {
            _animator.SetTrigger(stringTrigger);

            if (_animationsLength.TryGetValue(state, out var length))
            {
                var multiply = CalculateMultiply(targetTime, length);
                _animator.SetFloat(floatFlag, multiply);
            }
            else
            {
                var value = await GetAndSaveAnimationLength(layer, state);
                var multiply = CalculateMultiply(targetTime, value);
                _animator.SetFloat(floatFlag, multiply);
            }
        }
        
        private float CalculateMultiply(float targetTime, float lengthClip) => lengthClip / targetTime;
        
        private async UniTask<float> GetAndSaveAnimationLength(int layer, EAnimationState state)
        {
            await UniTask.Yield(PlayerLoopTiming.Update);
            await UniTask.Yield(PlayerLoopTiming.Update);
            
            var info = _animator.GetCurrentAnimatorClipInfo(layer);

            if (info.Length == 0)
            {
                return 1f;
            }
            
            var length = info[0].clip.length;
            
            _animationsLength.TryAdd(state, length);
            return length;
        }
    }
}