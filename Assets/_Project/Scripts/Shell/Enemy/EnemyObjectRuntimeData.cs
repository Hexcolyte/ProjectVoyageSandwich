using VoyageSandwich.Shell.Base;
using VoyageSandwich.Shell.Enum;

namespace VoyageSandwich.Shell.Enemy
{
    public class EnemyObjectRuntimeData: MovableObjectRuntimeData
    {
        private readonly float _targetSongPosition;
        private readonly PathPositionEnum _pathPosition;
        private readonly EnemyTypeEnum _enemyType;

        public float TargetSongPosition => _targetSongPosition;
        public PathPositionEnum PathPosition => _pathPosition;
        public EnemyTypeEnum EnemyType => _enemyType;

        public EnemyObjectRuntimeData(
            float targetBeatPosition,
            float toReachBeatPosition,
            PathPositionEnum pathPosition,
            EnemyTypeEnum enemyType) : base (toReachBeatPosition)
        {
            _targetSongPosition = targetBeatPosition;
            _pathPosition = pathPosition;
            _enemyType = enemyType;
        }
    }
}