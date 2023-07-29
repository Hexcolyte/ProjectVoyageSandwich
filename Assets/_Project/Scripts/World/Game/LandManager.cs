using UnityEngine;
using VoyageSandwich.World.Environment;
using VoyageSandwich.Shell.Environment;
using VoyageSandwich.Shell.Audio;

namespace VoyageSandwich.World.Game
{
    public class LandManager: ScrollerComponentBase<LandObject>
    {
        [SerializeField] private WorldSpriteInfo _worldSpriteInfo;

        [Header("Properties")]
        [SerializeField] private int _initialLandObjectCount = 4;

        private CameraController _cameraController;

        protected override float FinalYPos => _anchorPosition.y;

        public void Initialize(CameraController cameraController, Conductor conductor)
        {
            base.Initialize();

            _cameraController = cameraController;

            InitialSetupWorld();

            conductor.OnBeat += OnBeat;
        }

        private void InitialSetupWorld()
        {
            for (int i = 0; i < _initialLandObjectCount; i++)
            {
                SpawnLand();
            }
        }

        private void OnBeat()
        {
            MoveOneStep();

            if (!IsInitialized)
                return;

            Vector3 cameraPosition = _cameraController.GetCamera().transform.position;

            foreach (LandObject landObject in _existingObjectQueue)
            {
                // Quaternion rotation = Quaternion.LookRotation(landObject.transform.position - cameraPosition, Vector3.up);
                Quaternion rotation = Quaternion.Euler(-40f, 0f, 0f);
                landObject.Rotate(rotation);
            }
        }

        private void SpawnLand()
        {
            int i = _existingObjectQueue.Count;
            LandObject landObject = _objectPool.Get();
            float yOffset = i * _positionOffset;
            landObject.Move(_anchorPosition + new Vector3(0f, yOffset, 0f));
            _existingObjectQueue.Enqueue(landObject);

            landObject.SetSprite(_worldSpriteInfo.LandSprite, _worldSpriteInfo.PropSpriteChance);
        }

        protected override void OnLastObjectScrollEnded()
        {
            SpawnLand();
        }
    }
}