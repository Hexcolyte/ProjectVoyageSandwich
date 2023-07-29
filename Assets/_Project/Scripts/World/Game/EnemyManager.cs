using System.Collections;
using UnityEngine;
using UnityEngine.Pool;
using VoyageSandwich.World.Enemy;
using VoyageSandwich.Shell.Base;
using VoyageSandwich.Shell.Environment;

namespace VoyageSandwich.World.Game
{
    public class EnemyManager: ScrollerComponentBase<EnemyObject>
    {
        protected override float FinalYPos => _anchorPosition.y;

        public override void Initialize()
        {
            base.Initialize();

            StartCoroutine(TestSpawn());
        }

        private IEnumerator TestSpawn()
        {
            SpawnEnemy();
            yield return new WaitForSeconds(1f);
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