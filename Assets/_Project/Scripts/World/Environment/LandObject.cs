using System.Linq;
using UnityEngine;
using VoyageSandwich.World.Base;
using VoyageSandwich.Shell.Environment;

namespace VoyageSandwich.World.Environment
{
    public class LandObject : MovableObjectBase<LandObjectRuntimeData>
    {
        [SerializeField] private SpriteRenderer[] _landSpriteRenderers;
        [SerializeField] private SpriteRenderer[] _propSpriteRenderers;
        [SerializeField] private SpriteRenderer _leftWalkablePathSpriteRenderer;
        [SerializeField] private SpriteRenderer _rightWalkablePathSpriteRenderer;

        public void SetSprite(WorldSpriteInfo worldSpriteInfo)
        {
            SpriteSpawnChance[] propSpriteChances = worldSpriteInfo.PropSpriteChance;
            Sprite landSprite = worldSpriteInfo.LandSprite;

            _leftWalkablePathSpriteRenderer.sprite = worldSpriteInfo.LeftWalkablePathSprite;
            _rightWalkablePathSpriteRenderer.sprite = worldSpriteInfo.RightWalkablePathSprite;

            _spriteRenderer.sprite = worldSpriteInfo.CenterWalkablePathSprite;

            foreach (SpriteRenderer sr in _landSpriteRenderers)
            {
                sr.sprite = landSprite;
            }

            foreach (SpriteRenderer sr in _propSpriteRenderers)
            {
                int randomInt = Random.Range(0, 10);
                if (randomInt <= 3)
                {
                    bool spriteSet = false;
                    foreach(SpriteSpawnChance spriteChance in propSpriteChances)
                    {
                        if (Random.Range(0, spriteChance.Chance) == 0)
                        {
                            sr.sprite = spriteChance.Sprite;
                            spriteSet = true;
                            break;
                        }
                    }

                    if (!spriteSet) sr.sprite = null;
                }
                else
                {
                    sr.sprite = null;
                }

                sr.flipX = Random.Range(0, 2) == 0;
                sr.transform.localScale *= Random.Range(0.8f, 1.2f);
            }
        }

        public override void Show()
        {
            base.Show();

            foreach (SpriteRenderer sr in _landSpriteRenderers)
            {
                sr.enabled = true;
                sr.flipX = false;
                sr.transform.localScale = Vector3.one;
            }

            foreach (SpriteRenderer sr in _propSpriteRenderers)
            {
                sr.enabled = true;
                sr.flipX = false;
                sr.transform.localScale = Vector3.one;
            }
        }

        public override void Hide()
        {
            base.Hide();

            _landSpriteRenderers.ToList().ForEach(sr => sr.enabled = false);
            _propSpriteRenderers.ToList().ForEach(sr => sr.enabled = false);
        }

        public override void Rotate(Quaternion newRotation)
        {
            //Not calling base because we don't want it

            foreach (SpriteRenderer sr in _propSpriteRenderers)
            {
                if (!sr.enabled)
                    continue;

                sr.transform.rotation = newRotation;
            }
        }
    }
}