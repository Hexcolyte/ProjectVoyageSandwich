using System.Collections;
using UnityEngine;
using VoyageSandwich.World.Enemy;
using VoyageSandwich.Shell.Audio;
using VoyageSandwich.Shell.Enemy;

namespace VoyageSandwich.World.Game
{
    public class EnemyManager: ScrollerComponentBase<EnemyObject, EnemyObjectRuntimeData>
    {
        protected override float FinalYPos => _anchorPosition.y;
        protected override float CurrentSongTime => _conductor.GetSongPosition();

        private CameraController _cameraController;
        private Conductor _conductor;

        public void Initialize(CameraController cameraController, Conductor conductor)
        {
            base.Initialize();

            _cameraController = cameraController;
            _conductor = conductor;

            _conductor.OnBeat += OnBeat;
        }

        private void OnBeat(float songPositionInMilliseconds)
        {
            if (Random.Range(0,5) == 0)
                SpawnEnemy(songPositionInMilliseconds);

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

        public void SpawnEnemy(float songPositionInMilliseconds)
        {
            EnemyObject enemyObject = _objectPool.Get();

            //Add one unit because it will go to the next step right after this
            float maxDistanceUnit = 8f + 1f;
            enemyObject.Move(new Vector3(0f, maxDistanceUnit - _positionOffset, 0f));

            _existingObjectQueue.Enqueue(enemyObject);

            float songPositionInSeconds = songPositionInMilliseconds / 1000f;
            float targetSongPosition = (float)songPositionInSeconds + (_conductor.SecondsPerBeat * 8f);

            EnemyObjectRuntimeData newRuntimeData = new EnemyObjectRuntimeData(targetSongPosition, targetSongPosition + (_conductor.beatThreshold / 1000f / 2f));
            enemyObject.Initialize(newRuntimeData);
        }

        protected override void OnLastObjectScrollEnded()
        {
            Debug.Log("Enemy attacked");
        }
    }
}