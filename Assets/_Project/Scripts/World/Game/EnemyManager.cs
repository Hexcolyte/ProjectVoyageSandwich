using System.Collections;
using UnityEngine;
using VoyageSandwich.World.Enemy;
using VoyageSandwich.Shell.Audio;

namespace VoyageSandwich.World.Game
{
    public class EnemyManager: ScrollerComponentBase<EnemyObject>
    {
        protected override float FinalYPos => _anchorPosition.y;

        private CameraController _cameraController;

        public void Initialize(CameraController cameraController, Conductor conductor)
        {
            base.Initialize();

            _cameraController = cameraController;

            conductor.OnBeat += OnBeat;
        }

        private void OnBeat()
        {
            if (Random.Range(0,5) == 0)
                SpawnEnemy();

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
                Quaternion rotation = Quaternion.LookRotation(enemyObject.transform.position - cameraPosition, Vector3.up);
                enemyObject.Rotate(rotation);
            }
        }

        public void SpawnEnemy()
        {
            EnemyObject enemyObject = _objectPool.Get();

            float maxDistanceUnit = 8f;
            enemyObject.Move(new Vector3(0f, maxDistanceUnit - _positionOffset, 0f));

            _existingObjectQueue.Enqueue(enemyObject);
        }

        protected override void OnLastObjectScrollEnded()
        {
            Debug.Log("Enemy attacked");
        }
    }
}