using System.Collections;
using UnityEngine;

namespace Framework
{
    public class SoundChannel : MonoBehaviour
    {
        public bool IsPlay;
        public AudioSource Source;
        public ESound SoundKey;

        public bool IsMute
        {
            get => Source.mute;
            set => Source.mute = value;
        }

        public void Play(ESound soundKey, AudioClip clip, float volume, float delay)
        {
            IsPlay = true;
            SoundKey = soundKey;
            Source.clip = clip;
            Source.volume = volume;
            Source.loop = false;
            Source.PlayDelayed(delay);
            StartCoroutine(AutoStopCoroutine());
        }

        public void PlayLoop(ESound soundKey, AudioClip clip, float volume, float delay, bool isSetTimeStop, float timeStop)
        {
            IsPlay = true;
            SoundKey = soundKey;
            Source.clip = clip;
            Source.volume = volume;
            Source.loop = true;
            Source.PlayDelayed(delay);

            if (isSetTimeStop)
            {
                StartCoroutine(AutoStopCoroutine(delay + timeStop));
            }
        }

        public void Stop()
        {
            IsPlay = false;
            Source.Stop();
            StopAllCoroutines();
        }

        private IEnumerator AutoStopCoroutine(float time)
        {
            yield return new WaitForSeconds(time);
            Stop();
        }

        private IEnumerator AutoStopCoroutine()
        {
            yield return new WaitUntil(() => !Source.isPlaying);
            Stop();
        }
    }
}