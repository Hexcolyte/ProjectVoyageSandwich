using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VoyageSandwich.Shell.Audio;

namespace VoyageSandwich.Core.Main
{
    public class BeatInputListener : MonoBehaviour
    {
        [SerializeField] private Conductor _conductor;
        [SerializeField] private float _beatThreshold;

        private int _previousBeatTime;

        public event Action OnBeat;

        public void ProcessInput()
        {
            float songPositionInMilliseconds = AudioClock.GetSongPositionInMilliseconds(_conductor.MillisecondsPerBeat, _conductor.AudioStartTime, _conductor.InitialTimeOffsetInMilliseconds);
            int roundedBeatTime = _conductor.GetRoundedBeatTime(songPositionInMilliseconds);

            if (AudioClock.IsOnBeat(songPositionInMilliseconds, _conductor.MillisecondsPerBeat, _beatThreshold))
            {
                _previousBeatTime = roundedBeatTime;
                Debug.Log("Pressed: On Beat");
                OnBeat?.Invoke();
            }
        }
    }

}