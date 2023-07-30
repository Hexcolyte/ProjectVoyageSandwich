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
            return (60f / bpm) * 1000;
        }

        public static float GetSongPositionInMilliseconds(float secondsPerBeat, float startTime, float initialTimeOffsetInMilliseconds)
        {
            return (float) (AudioSettings.dspTime - startTime - initialTimeOffsetInMilliseconds) * 1000f;
        }

        public static bool IsOnBeat(float songPositionInMilliseconds, float timeIntervalPerBeatInMilliseconds, float timeOffsetThreshold)
        {
            float beatsModulusInMilliseconds = songPositionInMilliseconds % timeIntervalPerBeatInMilliseconds;

            float timeOffsetFromBeatInMilliseconds;

            if (beatsModulusInMilliseconds > timeIntervalPerBeatInMilliseconds / 2)
            {
                timeOffsetFromBeatInMilliseconds = timeIntervalPerBeatInMilliseconds - beatsModulusInMilliseconds;
            }

            else
            {
                timeOffsetFromBeatInMilliseconds = beatsModulusInMilliseconds;
            }

            return timeOffsetFromBeatInMilliseconds <= timeOffsetThreshold;
        }

        public static bool IsAfterBeat(float songPositionInMilliseconds, float timeIntervalPerBeatInMilliseconds, float timeOffsetThreshold)
        {
            return songPositionInMilliseconds % timeIntervalPerBeatInMilliseconds < timeOffsetThreshold;
        }

        public static int lastInterval;

        public static bool CheckForNewInterval(float currentInterval)
        {
            if (Mathf.FloorToInt(currentInterval) != lastInterval)
            {
                lastInterval = Mathf.FloorToInt(currentInterval);
                return true;
            }

            return false;
        }
    }
}