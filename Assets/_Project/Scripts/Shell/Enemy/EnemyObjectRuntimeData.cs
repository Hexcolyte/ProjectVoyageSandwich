using VoyageSandwich.Shell.Base;

namespace VoyageSandwich.Shell.Enemy
{
    public class EnemyObjectRuntimeData: MovableObjectRuntimeData
    {
        private readonly float _targetSongPosition;

        public float TargetSongPosition => _targetSongPosition;

        public EnemyObjectRuntimeData(float targetBeatPosition, float toReachBeatPosition) : base (toReachBeatPosition)
        {
            _targetSongPosition = targetBeatPosition;
        }
    }
}