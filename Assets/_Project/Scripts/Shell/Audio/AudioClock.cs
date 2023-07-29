using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoyageSandwich.Shell.Audio
{
    public static class AudioClock
    {
        /// <summary>
        /// Returns time in seconds in between each beat
        /// </summary>
        /// <param name="bpm">Beats Per Minute</param>
        /// <returns></returns>
        public static float GetTimeIntervalsPerBeat(float bpm)
        {
            return 60f / bpm;
        }

        public static float GetSongPositionInBeats(float secondsPerBeat, float startTime, float initialBeatOffset)
        {
            float songPosition = (float)AudioSettings.dspTime - startTime - initialBeatOffset;

            return songPosition / secondsPerBeat;
        }
    }
}