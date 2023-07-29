using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VoyageSandwich.Shell.Audio;

namespace VoyageSandwich.Core.Main
{
    public class Conductor : MonoBehaviour
    {
        public float audioBPM;

        public float secondsPerBeat;

        public float initialBeatOffset;

        //Current song position, in beats
        public float songPositionInBeats;

        //How many seconds have passed since the song started
        public float audioStatTime;

        //an AudioSource attached to this GameObject that will play the music.
        public AudioSource musicSource;

        // Start is called before the first frame update
        void Start()
        {
            //Load the AudioSource attached to the Conductor GameObject
            musicSource = GetComponent<AudioSource>();

            //Calculate the number of seconds in each beat
            secondsPerBeat = AudioClock.GetTimeIntervalsPerBeat(audioBPM);

            //Record the time when the music starts
            audioStatTime = (float)AudioSettings.dspTime;

            //Start the music
            musicSource.Play();
        }

        // Update is called once per frame
        void Update()
        {
            songPositionInBeats = AudioClock.GetSongPositionInBeats(secondsPerBeat, audioStatTime, initialBeatOffset);

            if (songPositionInBeats % 1 == 0)
                Debug.Log($"Beats: {songPositionInBeats}");
        }
    }
}