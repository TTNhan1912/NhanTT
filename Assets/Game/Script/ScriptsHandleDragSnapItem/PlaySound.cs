using UnityEngine;

public class PlaySound : MonoBehaviour
{
    public AudioClip Sound;
    public bool PlayOnAwake;
    public SoundPlayType PlayType;

    private void OnEnable()
    {
        if (PlayOnAwake)
        {
            Play();
        }
    }

    public void Play()
    {
        switch (PlayType)
        {
            case SoundPlayType.REWIND:
                //   SoundManager.Get.PlaySfxRewind(Sound);
                break;

            case SoundPlayType.DONT_REWIND:
                //  SoundManager.Get.PlaySfxNoRewind(Sound);
                break;

            case SoundPlayType.OVERRIDE:
                // SoundManager.Get.PlaySfxOverride(Sound);
                break;

            case SoundPlayType.LOOP:
                //   SoundManager.Get.PlaySfxLoop(Sound, 0);
                break;
        }
    }

    public void Stop()
    {
        if (PlayType == SoundPlayType.LOOP)
        {
            // SoundManager.Get.StopLoopSound(Sound, 0);
        }
        else
        {
            //  SoundManager.Get.StopSfxSound(Sound);
        }
    }

    public bool IsPlaying()
    {
        //  return SoundManager.Get.IsPlayingSound(Sound);
        return true;
    }

    private void OnDisable()
    {
        Stop();
    }

    private void OnDestroy()
    {
        Stop();
    }
}


public enum SoundPlayType
{
    /// <summary>
    /// restart the sound
    /// </summary>
    REWIND,
    /// <summary>
    /// if the same sound is playing, dont play this one
    /// </summary>
    DONT_REWIND,
    /// <summary>
    /// always play the sound
    /// </summary>
    OVERRIDE,
    LOOP,
    NONE
}