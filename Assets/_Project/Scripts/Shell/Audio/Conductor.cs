using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VoyageSandwich.Shell.Base;
using VoyageSandwich.Shell.Game;

namespace VoyageSandwich.Shell.Audio
{
    public class Conductor : BaseComponent
    {
        [SerializeField] private BeatInfo _beatInfo;
        public BeatInfo BeatInfo => _beatInfo;

        public float audioBPM;

        public float InitialTimeOffsetInMilliseconds;

        //How many seconds have passed since the song started
        private float _audioStartTime;
        public float AudioStartTime => _audioStartTime;

        //an AudioSource attached to this GameObject that will play the music.
        public AudioSource musicSource;

        [SerializeField] private float _beatTickCheckThreshold;

        private int _previousBeatTime = 0;
        public int PrevBeatTime => _previousBeatTime;

        private float _millisecondsPerBeat;
        public float MillisecondsPerBeat => _millisecondsPerBeat;
        public float SecondsPerBeat => _millisecondsPerBeat / 1000f;

        public event Action<float> OnBeat;

        public override void Initialize()
        {
            double initTime = AudioSettings.dspTime + (InitialTimeOffsetInMilliseconds / 1000f);

            //Calculate the number of seconds in each beat
            _millisecondsPerBeat = AudioClock.GetTimeIntervalsPerBeat(audioBPM);

            //Record the time when the music starts
            _audioStartTime = (float)initTime;

            //Start the music
            musicSource.PlayScheduled(initTime);
        }

        public override void Tick(float deltaTime)
        {
            float songPositionInMilliseconds = GetSongPosition();
            int roundedBeatTime = GetRoundedBeatTime(songPositionInMilliseconds);

            if (_previousBeatTime < roundedBeatTime && AudioClock.IsAfterBeat(songPositionInMilliseconds, _millisecondsPerBeat, _beatTickCheckThreshold))
            {
                _previousBeatTime = roundedBeatTime;
                OnBeat?.Invoke(songPositionInMilliseconds);
            }
        }

        public int GetRoundedBeatTime(float songPositionInMilliseconds) => Mathf.FloorToInt(songPositionInMilliseconds / _millisecondsPerBeat);

        public float GetSongPosition()
        {
            return AudioClock.GetSongPositionInMilliseconds(_millisecondsPerBeat, _audioStartTime, InitialTimeOffsetInMilliseconds);
        }
    }
}