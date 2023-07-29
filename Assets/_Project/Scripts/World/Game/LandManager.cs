using UnityEngine;
using UnityEngine.Pool;
using VoyageSandwich.Shell.Base;

namespace VoyageSandwich.World.Game
{
    public class LandManager: BaseComponent
    {
        [SerializeField] private SpriteRenderer[] _laneSpriteRenderers;

        private ObjectPool<SpriteRenderer> _landSpriteRendererPool;

        public void Initialize(CameraController cameraController)
        {
            base.Initialize();


        }
    }
}