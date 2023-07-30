using System.Collections.Generic;
using UnityEngine;
using VoyageSandwich.World.Enemy;
using VoyageSandwich.Shell.Audio;
using VoyageSandwich.Shell.Enemy;
using VoyageSandwich.Shell.Enum;
using System;
using Random = UnityEngine.Random;
using MoreMountains.Feedbacks;

namespace VoyageSandwich.World.Game
{
    public class EnemyManager: ScrollerComponentBase<EnemyObject, EnemyObjectRuntimeData>
    {
        [Header("Properties")]
        [SerializeField] private float ChanceToSpawnPerStep;
        [SerializeField] private EnemyLibrary EnemyLibrary;


        protected override float FinalYPos => _anchorPosition.y - _positionOffset;

        [SerializeField] private MMF_Player _deathFeedBackPlayer;

        protected override float CurrentSongTime => _conductor.GetSongPosition();

        private CameraController _cameraController;
        private Conductor _conductor;

        Queue<EnemyObject> LeftLandQueue = new Queue<EnemyObject>();
        Queue<EnemyObject> CenterLandQueue = new Queue<EnemyObject>();
        Queue<EnemyObject> RightLandQueue = new Queue<EnemyObject>();

        private bool _spawnToggle = false;

        public event Action<EnemyObjectRuntimeData> OnEnemyHitFinishLine;

        public void Initialize(CameraController cameraController, Conductor conductor)
        {
            base.Initialize();

            _cameraController = cameraController;
            _conductor = conductor;

            _conductor.OnBeat += OnBeat;
        }

        private void OnBeat(float songPositionInMilliseconds)
        {
            _spawnToggle = !_spawnToggle;
            
            if (_spawnToggle)
            {
                if (Random.Range(0,4) == 0)
                {
                    SpawnEnemy(songPositionInMilliseconds, EnemyTypeEnum.Obstacle);
                }
                else
                {
                    if (Random.Range(0f, 100f) <= ChanceToSpawnPerStep)
                        SpawnEnemy(songPositionInMilliseconds);
                }
            }

            MoveOneStep();
        }

        protected override void MoveOneStep()
        {
            base.MoveOneStep();

            if (!IsInitialized)
                return;

            Vector3 cameraPosition = _cameraController.GetCamera().transform.position;

            foreach (EnemyObject enemyObject in _existingObjectQueue)
            {
                // Quaternion rotation = Quaternion.LookRotation(enemyObject.transform.position - cameraPosition, Vector3.up);
                Quaternion rotation = Quaternion.Euler(-40f, 0f, 0f);
                enemyObject.Rotate(rotation);
            }
        }

        public void SpawnEnemy(float songPositionInMilliseconds, EnemyTypeEnum enemyType = EnemyTypeEnum.Normal)
        {
            EnemyObject enemyObject = _objectPool.Get();

            _existingObjectQueue.Enqueue(enemyObject);

            int randomLane = Random.Range(0, 3);

            float xStartPos = 0f;
            PathPositionEnum pathPosition = (PathPositionEnum)randomLane;

            switch (pathPosition)
            {
                case PathPositionEnum.Left:
                    LeftLandQueue.Enqueue(enemyObject);
                    xStartPos = -_positionOffset;
                    break;

                case PathPositionEnum.Center:
                    CenterLandQueue.Enqueue(enemyObject);
                    xStartPos = 0f;
                    break;

                case PathPositionEnum.Right:
                    RightLandQueue.Enqueue(enemyObject);
                    xStartPos = _positionOffset;
                    break;
            }

            //Add one unit because it will go to the next step right after this
            float maxDistanceUnit = _anchorPosition.y + 8f + 1f;
            float yStartPos = maxDistanceUnit - _positionOffset;

            enemyObject.Move(new Vector3(xStartPos, yStartPos, 0f));

            float songPositionInSeconds = songPositionInMilliseconds / 1000f;
            float targetSongPosition = (float)songPositionInSeconds + (_conductor.SecondsPerBeat * 8f);

            EnemyObjectRuntimeData newRuntimeData = new EnemyObjectRuntimeData(
                targetSongPosition,
                targetSongPosition + (_conductor.BeatInfo.AcceptableBeatThreshold / 1000f / 2f),
                pathPosition,
                enemyType
            );

            enemyObject.Initialize(newRuntimeData, _conductor.SecondsPerBeat);

            if (EnemyLibrary.TryGetPossibleSprites(enemyType, out Sprite[] enemySprites))
            {
                enemyObject.SetSprite(enemySprites[Random.Range(0, enemySprites.Length)]);
            }
        }

        protected override void OnLastObjectScrollEnded(EnemyObject lastRemovedObject)
        {
            RemoveEnemyFromLand(lastRemovedObject);
            OnEnemyHitFinishLine?.Invoke(lastRemovedObject.RuntimeData);
        }

        private void RemoveEnemyFromLand(EnemyObject lastRemovedObject)
        {
            switch (lastRemovedObject.RuntimeData.PathPosition)
            {
                case PathPositionEnum.Left:
                    LeftLandQueue.Dequeue();
                    break;

                case PathPositionEnum.Center:
                    CenterLandQueue.Dequeue();
                    break;

                case PathPositionEnum.Right:
                    RightLandQueue.Dequeue();
                    break;
            }
        }

        public bool TryGetClosestEnemyOnLand(PathPositionEnum PathPosition, out EnemyObject enemyObject)
        {
            enemyObject = null;

            switch (PathPosition)
            {
                case PathPositionEnum.Left:
                    if (LeftLandQueue.Count == 0)
                        return false;

                    enemyObject = LeftLandQueue.Peek();
                    return true;

                case PathPositionEnum.Center:
                    if (CenterLandQueue.Count == 0)
                        return false;

                    enemyObject = CenterLandQueue.Peek();
                    return true;

                case PathPositionEnum.Right:
                    if (RightLandQueue.Count == 0)
                        return false;

                    enemyObject = RightLandQueue.Peek();
                    return true;
            }

            return false;
        }

        public void RemoveEnemy()
        {
            bool removed = RemoveObject(out EnemyObject enemyObject);

            
            _deathFeedBackPlayer.PlayFeedbacks();   

            if (removed)
                RemoveEnemyFromLand(enemyObject);
        }
    }
}