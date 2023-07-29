using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VoyageSandwich.Shell.Audio;

namespace VoyageSandwich.Core.Main
{
    public class Conductor : MonoBehaviour
    {
        public float audioBPM;

        public float millisecondsPerBeat;

        public float initialTimeOffsetInMilliseconds;

        //Current song position, in beats
        public float songPositionInBeats;

        //How many seconds have passed since the song started
        public float audioStartTime;

        //an AudioSource attached to this GameObject that will play the music.
        public AudioSource musicSource;

        public float beatThreshold;

        private int _previousBeatTime = 0;

        // Start is called before the first frame update
        void Start()
        {
            //Load the AudioSource attached to the Conductor GameObject
            musicSource = GetComponent<AudioSource>();

            //Calculate the number of seconds in each beat
            millisecondsPerBeat = AudioClock.GetTimeIntervalsPerBeat(audioBPM);

            //Record the time when the music starts
            audioStartTime = (float)AudioSettings.dspTime;

            //Start the music
            musicSource.Play();
        }

        private void Update()
        {
            float songPositionInMilliseconds = AudioClock.GetSongPositionInMilliseconds(millisecondsPerBeat, audioStartTime, initialTimeOffsetInMilliseconds);
            int roundedBeatTime = Mathf.FloorToInt(songPositionInMilliseconds / millisecondsPerBeat);

            if (_previousBeatTime < roundedBeatTime && AudioClock.IsAfterBeat(songPositionInMilliseconds, millisecondsPerBeat, beatThreshold))
            {
                _previousBeatTime = roundedBeatTime;
                Debug.Log("Update: On Beat");
            }
        }

        public void Tap()
        {
            float songPositionInMilliseconds = AudioClock.GetSongPositionInMilliseconds(millisecondsPerBeat, audioStartTime, initialTimeOffsetInMilliseconds);
            
            Debug.Log("On Beat");
        }
    }
}