using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VoyageSandwich.Shell.Base;

namespace VoyageSandwich.Shell.Audio
{
    public class Conductor : BaseComponent
    {
        public float audioBPM;

        public float initialTimeOffsetInMilliseconds;

        //How many seconds have passed since the song started
        public float audioStartTime;

        //an AudioSource attached to this GameObject that will play the music.
        public AudioSource musicSource;

        public float beatThreshold;

        private int _previousBeatTime = 0;
        private float _millisecondsPerBeat;

        public event Action OnBeat;

        public override void Initialize()
        {
            double initTime = AudioSettings.dspTime;

            //Calculate the number of seconds in each beat
            _millisecondsPerBeat = AudioClock.GetTimeIntervalsPerBeat(audioBPM);

            //Record the time when the music starts
            audioStartTime = (float)initTime;

            //Start the music
            musicSource.PlayScheduled(initTime);
        }

        public override void Tick(float deltaTime)
        {
            float songPositionInMilliseconds = AudioClock.GetSongPositionInMilliseconds(_millisecondsPerBeat, audioStartTime, initialTimeOffsetInMilliseconds);
            int roundedBeatTime = Mathf.FloorToInt(songPositionInMilliseconds / _millisecondsPerBeat);

            if (_previousBeatTime < roundedBeatTime && AudioClock.IsAfterBeat(songPositionInMilliseconds, _millisecondsPerBeat, beatThreshold))
            {
                _previousBeatTime = roundedBeatTime;
                OnBeat?.Invoke();
            }
        }
    }
}