using UnityEngine;
using VoyageSandwich.World.Environment;
using VoyageSandwich.Shell.Environment;

namespace VoyageSandwich.World.Game
{
    public class LandManager: ScrollerComponentBase<LandObject>
    {
        [SerializeField] private WorldSpriteInfo _worldSpriteInfo;

        [Header("Properties")]
        [SerializeField] private int _initialLandObjectCount = 4;

        protected override float FinalYPos => _anchorPosition.y - _positionOffset;

        public void Initialize(CameraController cameraController)
        {
            base.Initialize();

            InitialSetupWorld();
        }

        private void InitialSetupWorld()
        {
            for (int i = 0; i < _initialLandObjectCount; i++)
            {
                SpawnLand();
            }
        }

        private void SpawnLand()
        {
            int i = _existingObjectQueue.Count;
            LandObject landObject = _objectPool.Get();
            float yOffset = i * _positionOffset;
            landObject.Move(_anchorPosition + new Vector3(0f, yOffset, 0f));
            _existingObjectQueue.Enqueue(landObject);
        }

        protected override void OnLastObjectScrollEnded() => SpawnLand();
    }
}