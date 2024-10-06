using UnityEngine;

namespace Framework
{
    public static class AudioHelper
    {
        public static void PlayRandomSound(ESound[] sounds) => AudioManager.Instance.PlaySound(sounds[Random.Range(0, sounds.Length)]);

        public static void PlaySoundIfNotPlaying(ESound soundKey)
        {
            if (AudioManager.Instance.IsSoundPlaying(soundKey)) return;
            AudioManager.Instance.PlaySound(soundKey);
        }
    }
}