using System;
using UnityEngine;
using VoyageSandwich.Shell.Base;
using VoyageSandwich.Shell.Audio;

namespace VoyageSandwich.World.Game
{
    public class BeatInputListener : BaseComponent
    {
        [SerializeField] private Conductor _conductor;

        private int _previousBeatTime;

        public event Action<float> OnPerfectTap;
        public event Action<float> OnAcceptableTap;
        public event Action<float> OnFailTap;

        public VoidEvent OnPerfectEvent;
        public VoidEvent OnAcceptableEvent;
        public VoidEvent OnFailEvent;

        public void ProcessInput()
        {
            TryToAttack();
        }

        private void TryToAttack()
        {
            float songPositionInMilliseconds = AudioClock.GetSongPositionInMilliseconds(_conductor.MillisecondsPerBeat, _conductor.AudioStartTime, _conductor.InitialTimeOffsetInMilliseconds);
            int roundedBeatTime = _conductor.GetRoundedBeatTime(songPositionInMilliseconds);

            if (AudioClock.IsOnBeat(songPositionInMilliseconds, _conductor.MillisecondsPerBeat, _conductor.BeatInfo.PerfectBeatThreshold))
            {
                _previousBeatTime = roundedBeatTime;
                OnPerfectTap?.Invoke(songPositionInMilliseconds);
                OnPerfectEvent?.Raise();
            }
            else if (AudioClock.IsOnBeat(songPositionInMilliseconds, _conductor.MillisecondsPerBeat, _conductor.BeatInfo.AcceptableBeatThreshold))
            {
                OnAcceptableTap?.Invoke(songPositionInMilliseconds);
                OnAcceptableEvent?.Raise();
            }
            else
            {
                OnFailTap?.Invoke(songPositionInMilliseconds);
                OnFailEvent?.Raise();
            }
        }
    }

}