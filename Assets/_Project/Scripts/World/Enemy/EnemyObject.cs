using UnityEngine;
using VoyageSandwich.Shell.Base;
using VoyageSandwich.Shell.Enemy;
using VoyageSandwich.World.Base;

namespace VoyageSandwich.World.Enemy
{
    public class EnemyObject : MovableObjectBase<EnemyObjectRuntimeData>
    {
        private static readonly int Idle = Animator.StringToHash("Idle");
        [SerializeField] protected Animator _animator;

        public void SetSpriteAnimation(AnimatorOverrideController newAnimator)
        {
            _animator.runtimeAnimatorController = newAnimator;
            _animator.CrossFade(Idle, 0f);
        }
    }
}