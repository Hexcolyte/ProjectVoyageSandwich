using UnityEngine;

namespace VoyageSandwich.Shell.Game
{
    [CreateAssetMenu(menuName = "Game/BeatInfo")]
    public class BeatInfo : ScriptableObject
    {
        [SerializeField] private float _perfectBeatThreshold;
        [SerializeField] private float _acceptableBeatThreshold;

        public float PerfectBeatThreshold => _perfectBeatThreshold;
        public float AcceptableBeatThreshold => _acceptableBeatThreshold;
    }
}