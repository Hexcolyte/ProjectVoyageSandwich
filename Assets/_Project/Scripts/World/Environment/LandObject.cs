using UnityEngine;
using VoyageSandwich.Shell.Base;

namespace VoyageSandwich.World.Environment
{
    public class LandObject : BaseComponent
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;

        public float CurrentYPos => transform.localPosition.y;

        public void Move(float newYPos)
        {
            transform.localPosition = new Vector3(0f, newYPos, 0f);
        }

        public void Show()
        {
            _spriteRenderer.enabled = true;
        }

        public void Hide()
        {
            _spriteRenderer.enabled = false;
        }
    }
}