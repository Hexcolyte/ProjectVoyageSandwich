using UnityEngine;
using VoyageSandwich.Shell.Base;

namespace VoyageSandwich.World.Base
{
    public abstract class MovableObjectBase<T1> : BaseComponent
        where T1: MovableObjectRuntimeData
    {
        [SerializeField] protected SpriteRenderer _spriteRenderer;

        public float CurrentYPos => transform.localPosition.y;
        public bool IsCheckSongPosition => IsInitialized && _runtimeData.ToReachSongPosition > 0f;
        public virtual bool IsOverReach(float currentAudioTime) => currentAudioTime >= _runtimeData.ToReachSongPosition;
        
        private T1 _runtimeData;
        public T1 RuntimeData => _runtimeData;

        public void Initialize(T1 runtimeData)
        {
            base.Initialize();

            _runtimeData = runtimeData;
        }

        public virtual void MoveY(float newYPos)
        {
            Vector3 currentPos = transform.localPosition;
            transform.localPosition = new Vector3(currentPos.x, newYPos, currentPos.z);
        }

        public virtual void Move(Vector3 newPosition)
        {
            transform.localPosition = newPosition;
        }

        public virtual void Rotate(Quaternion newRotation)
        {
            _spriteRenderer.transform.rotation = newRotation;
        }

        public virtual void Show()
        {
            _spriteRenderer.enabled = true;
        }

        public virtual void Hide()
        {
            _spriteRenderer.enabled = false;
        }

        public virtual void SelfDestroy()
        {
            Destroy(gameObject);
        }
    }
}