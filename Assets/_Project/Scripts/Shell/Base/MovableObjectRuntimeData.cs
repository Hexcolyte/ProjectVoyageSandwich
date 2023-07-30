namespace VoyageSandwich.Shell.Base
{
    public class MovableObjectRuntimeData
    {
        private readonly float _toReachSongPosition;
        public float ToReachSongPosition => _toReachSongPosition;

        public MovableObjectRuntimeData(float toReachBeatPosition)
        {
            _toReachSongPosition = toReachBeatPosition;
        }
    }
}