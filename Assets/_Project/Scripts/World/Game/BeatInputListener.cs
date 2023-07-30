using System;
using UnityEngine;
using VoyageSandwich.Shell.Base;
using VoyageSandwich.Shell.Audio;

namespace VoyageSandwich.World.Game
{
    public class BeatInputListener : BaseComponent
    {
        [SerializeField] private Conductor _conductor;
        [SerializeField] private float _perfectBeatThreshold;
        [SerializeField] private float _acceptableBeatThreshold;

        private int _previousBeatTime;

        public event Action<float> OnPerfectTap;
        public event Action<float> OnAcceptableTap;
        public event Action<float> OnFailTap;

        public void ProcessInput()
        {
            TryToAttack();
        }

        private void TryToAttack()
        {
            float songPositionInMilliseconds = AudioClock.GetSongPositionInMilliseconds(_conductor.MillisecondsPerBeat, _conductor.AudioStartTime, _conductor.InitialTimeOffsetInMilliseconds);
            int roundedBeatTime = _conductor.GetRoundedBeatTime(songPositionInMilliseconds);

            if (AudioClock.IsOnBeat(songPositionInMilliseconds, _conductor.MillisecondsPerBeat, _perfectBeatThreshold))
            {
                _previousBeatTime = roundedBeatTime;
                OnPerfectTap?.Invoke(songPositionInMilliseconds);
            }
            else if (AudioClock.IsOnBeat(songPositionInMilliseconds, _conductor.MillisecondsPerBeat, _acceptableBeatThreshold))
            {
                OnAcceptableTap?.Invoke(songPositionInMilliseconds);
            }
            else
            {
                OnFailTap?.Invoke(songPositionInMilliseconds);
            }
        }
    }

}