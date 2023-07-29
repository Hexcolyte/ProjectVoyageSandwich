using UnityEngine;
using VoyageSandwich.Shell.Base;

namespace VoyageSandwich.World.Base
{
    public abstract class MovableObjectBase : BaseComponent
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;

        public float CurrentYPos => transform.localPosition.y;

        public virtual void MoveY(float newYPos)
        {
            Vector3 currentPos = transform.localPosition;
            transform.localPosition = new Vector3(currentPos.x, newYPos, currentPos.z);
        }

        public virtual void Move(Vector3 newPosition)
        {
            transform.localPosition = newPosition;
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