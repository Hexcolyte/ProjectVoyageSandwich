using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.Tilemaps;
using VoyageSandwich.World.Environment;
using VoyageSandwich.Shell.Base;
using VoyageSandwich.Shell.Environment;

namespace VoyageSandwich.World.Game
{
    public class LandManager: BaseComponent
    {
        [SerializeField] private LandObject _landObjectPrefab;
        [SerializeField] private WorldSpriteInfo _worldSpriteInfo;

        [Header("Properties")]
        [SerializeField] private int _initialLandObjectCount = 4;

        [Header("Positions")]
        [SerializeField] private Vector2 _landPositionRange = new Vector2();
        [SerializeField] private Transform _anchorLandTransform;
        [SerializeField] private float _landStackedOffset = 3f;

        //Private
        private ObjectPool<LandObject> _landObjectPool;
        private Vector3 _anchorLandPosition => _anchorLandTransform.position;
        private Queue<LandObject> _existingLandObjectQueue = new Queue<LandObject>();

        public void Initialize(CameraController cameraController)
        {
            base.Initialize();

            _landObjectPool = new ObjectPool<LandObject>
            (
                () => Instantiate<LandObject>(_landObjectPrefab), 
                (landObject) => landObject.Show(),
                (landObject) => landObject.Hide(),
                (landObject) => Destroy(landObject.gameObject),
                false,
                10,
                100
            );

            InitialSetupWorld();
        }

        private void InitialSetupWorld()
        {
            for (int i = 0; i < _initialLandObjectCount; i++)
            {
                SpawnLand();
            }
        }

        public override void Tick(float deltaTime)
        {
            LandObject firstLand = _existingLandObjectQueue.Peek();

            if (firstLand.CurrentYPos <= _anchorLandPosition.y - _landStackedOffset)
            {
                LandObject landToBeRemoved = _existingLandObjectQueue.Dequeue();
                _landObjectPool.Release(landToBeRemoved);

                SpawnLand();
            }

            foreach (LandObject landObject in _existingLandObjectQueue)
            {
                float currentYPos = landObject.CurrentYPos;
                currentYPos -= deltaTime;
                
                landObject.Move(currentYPos);
            }
        }

        private void SpawnLand()
        {
            int i = _existingLandObjectQueue.Count;
            LandObject landObject = _landObjectPool.Get();
            float yOffset = i * _landStackedOffset;
            landObject.transform.position = _anchorLandPosition + new Vector3(0f, yOffset, 0f);
            _existingLandObjectQueue.Enqueue(landObject);
        }
    }
}