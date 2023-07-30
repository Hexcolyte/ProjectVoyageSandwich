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

        public float InitialTimeOffsetInMilliseconds;

        //How many seconds have passed since the song started
        private float _audioStartTime;
        public float AudioStartTime {  get { return _audioStartTime; } }

        //an AudioSource attached to this GameObject that will play the music.
        public AudioSource musicSource;

        public float beatThreshold;

        private int _previousBeatTime = 0;
        public int PrevBeatTime => _previousBeatTime;

        private float _millisecondsPerBeat;
        public float MillisecondsPerBeat { get { return _millisecondsPerBeat; } }

        public event Action OnBeat;

        public override void Initialize()
        {
            double initTime = AudioSettings.dspTime;

            //Calculate the number of seconds in each beat
            _millisecondsPerBeat = AudioClock.GetTimeIntervalsPerBeat(audioBPM);

            //Record the time when the music starts
            _audioStartTime = (float)initTime;

            //Start the music
            musicSource.PlayScheduled(initTime);
        }

        public override void Tick(float deltaTime)
        {
            float songPositionInMilliseconds = AudioClock.GetSongPositionInMilliseconds(_millisecondsPerBeat, _audioStartTime, InitialTimeOffsetInMilliseconds);
            int roundedBeatTime = GetRoundedBeatTime(songPositionInMilliseconds);

            if (_previousBeatTime < roundedBeatTime && AudioClock.IsAfterBeat(songPositionInMilliseconds, _millisecondsPerBeat, beatThreshold))
            {
                _previousBeatTime = roundedBeatTime;
                OnBeat?.Invoke();
            }
        }

        public int GetRoundedBeatTime(float songPositionInMilliseconds) => Mathf.FloorToInt(songPositionInMilliseconds / _millisecondsPerBeat);
    }
}