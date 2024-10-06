using UnityEngine;

namespace Framework
{
    public class MusicChannel : MonoBehaviour
    {
        public bool IsPlay;
        public AudioSource Source;
        public EMusic MusicKey;

        public bool IsMute
        {
            get => Source.mute;
            set => Source.mute = value;
        }

        public void Play(EMusic musicKey, AudioClip clip,float volume, float delay = 0)
        {
            IsPlay = true;
            MusicKey = musicKey;
            Source.clip = clip;
            Source.loop = true;
            Source.volume = volume;
            Source.PlayDelayed(delay);
        }

        public void Stop()
        {
            IsPlay = false;
            StopAllCoroutines();
        }
    }
}
