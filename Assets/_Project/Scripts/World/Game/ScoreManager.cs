using UnityEngine;
using VoyageSandwich.Shell.Base;
using VoyageSandwich.Shell.Player;
using VoyageSandwich.Shell.Enemy;
using VoyageSandwich.Shell.Enum;
using VoyageSandwich.World.Enemy;
using VoyageSandwich.Shell.Audio;
using MoreMountains.Feedbacks;

namespace VoyageSandwich.World.Game
{
    public class ScoreManager: BaseComponent
    {
        private EnemyManager _enemyManager;
        private PlayerController _playerController;
        private Conductor _conductor;
        [SerializeField] MMF_Player _playerHit;

        public void Initialize(
            Conductor conductor,
            PlayerController playerController,
            BeatInputListener beatInputListener,
            EnemyManager enemyManager)
        {
            base.Initialize();

            _enemyManager = enemyManager;
            _playerController = playerController;
            _conductor = conductor;

            beatInputListener.OnPerfectTap += OnPerfectTap;
            beatInputListener.OnAcceptableTap += OnAcceptableTap;
            beatInputListener.OnFailTap += OnFailTap;

            enemyManager.OnEnemyHitFinishLine += OnEnemyHitFinishLine;
        }

        private void OnPerfectTap(float songPositionInMilliseconds)
        {
            if (!TryIsEnemyInTapRange(songPositionInMilliseconds, out bool isInRange))
                return;

            if (isInRange)
            {
                Debug.Log("<color=green>Perfect</color>");
                _enemyManager.RemoveEnemy();
            }
            else
            {
                Debug.Log("<color=white>Tapped empty</color>");
            }
        }

        private void OnAcceptableTap(float songPositionInMilliseconds)
        {
            if (!TryIsEnemyInTapRange(songPositionInMilliseconds, out bool isInRange))
                return;

            if (isInRange)
            {
                _enemyManager.RemoveEnemy();
                Debug.Log("<color=orange>Acceptable</color>");
            }
            else
                Debug.Log("<color=white>Tapped empty</color>");
        }

        private void OnFailTap(float songPositionInMilliseconds)
        {
            Debug.Log("<color=red>Fail tap</color>");
            TakeDamage();
        }

        private bool TryIsEnemyInTapRange(float songPositionInMilliseconds, out bool isInRange)
        {
            isInRange = false;

            bool successful = _enemyManager.TryGetClosestEnemyOnLand(_playerController.PathPosition, out EnemyObject enemyObject);

            if (!successful)
            {
                Debug.Log($"No enemy found in {_playerController.PathPosition}");
                return false;
            }

            isInRange = enemyObject.RuntimeData.TargetSongPosition - (songPositionInMilliseconds / 1000f) < _conductor.SecondsPerBeat;
            return true;
        }

        private void OnEnemyHitFinishLine(EnemyObjectRuntimeData runtimeData)
        {
            switch(runtimeData.EnemyType)
            {
                case EnemyTypeEnum.Obstacle:
                    if (runtimeData.PathPosition != _playerController.PathPosition)
                        return;
                    break;
            }
            
            TakeDamage();
        }

        private void TakeDamage(int damageCount = 1)
        {
            if (_playerHit) _playerHit.PlayFeedbacks();
        }
    }
}