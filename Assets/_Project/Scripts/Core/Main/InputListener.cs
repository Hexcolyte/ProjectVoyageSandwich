using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VoyageSandwich.Shell.Audio;

namespace VoyageSandwich.Core.Main
{
    public class InputListener : MonoBehaviour
    {
        [SerializeField] private Conductor _conductor;
        [SerializeField] private float _beatThreshold;

        public event Action OnBeat;

        public void ProcessInput()
        {
            float songPositionInMilliseconds = AudioClock.GetSongPositionInMilliseconds(_conductor.MillisecondsPerBeat, _conductor.AudioStartTime, _conductor.InitialTimeOffsetInMilliseconds);

            if (AudioClock.IsOnBeat(songPositionInMilliseconds, _conductor.MillisecondsPerBeat, _beatThreshold))
            {
                Debug.Log("Pressed: On Beat");
                OnBeat?.Invoke();
            }
        }
    }

}