using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Pool;

namespace Framework
{
    public class AudioManager : MonoSingleton<AudioManager>
    {
        [Header("AUDIO CLIPS")]
        [SerializeField] private List<AudioClip> _musicClipList;
        [SerializeField] private List<AudioClip> _soundClipList;

        [Header("CHANNEL PREFABS")]
        [SerializeField] private MusicChannel _musicChannelPrefab;
        [SerializeField] private SoundChannel _soundChannelPrefab;

        private List<MusicChannel> _musicChannels = new();
        private List<SoundChannel> _soundChannels = new();

        private Dictionary<EMusic, AudioClip> _bgmClipDic;
        private Dictionary<ESound, AudioClip> _soundClipDic;

        private bool _isMuteMusic;
        private bool _isMuteSound;

        private ObjectPool<MusicChannel> _musicChannelPool;
        private ObjectPool<SoundChannel> _soundChannelPool;

        public bool IsMuteMusic
        {
            get => _isMuteMusic;
            set
            {
                if (value == _isMuteMusic) return;
                _isMuteMusic = value;
                foreach (var channel in _musicChannels)
                {
                    channel.IsMute = _isMuteMusic;
                }
            }
        }

        public bool IsMuteSound
        {
            get => _isMuteSound;
            set
            {
                if (value == _isMuteSound) return;
                _isMuteSound = value;
                foreach (var channel in _soundChannels)
                {
                    channel.IsMute = _isMuteSound;
                }
            }
        }

        #region UNITY

        public void Start()
        {
            _musicChannelPool = new(
                () => Instantiate(_musicChannelPrefab, this.transform),
                channel =>
                {
                    channel.gameObject.SetActive(true);
                    _musicChannels.Add(channel);
                },
                channel =>
                {
                    channel.gameObject.SetActive(false);
                    _musicChannels.Remove(channel);
                },
                channel =>
                {
                    _musicChannels.Remove(channel);
                    Destroy(channel.gameObject);
                },
                true, 10, 20);

            _soundChannelPool = new(
                () => Instantiate(_soundChannelPrefab, this.transform),
                channel =>
                {
                    channel.gameObject.SetActive(true);
                    _soundChannels.Add(channel);
                },
                channel =>
                {
                    channel.gameObject.SetActive(false);
                    _soundChannels.Remove(channel);
                },
                channel =>
                {
                    _soundChannels.Remove(channel);
                    Destroy(channel.gameObject);
                },
                true, 10, 20);
        }

        #endregion // UNITY

        #region MUSIC CONTROLLERS

        public MusicChannel PlayMusic(EMusic musicKey, float volume)
        {
            var channel = GetFreeMusicChannel();
            if (channel == null) return null;

            var musicAudioClip = GetMusicAudioClip(musicKey);
            channel.Play(musicKey, musicAudioClip, volume);

            return channel;
        }

        public void StopMusic(EMusic musicKey)
        {
            _musicChannels.FirstOrDefault(m => m.MusicKey == musicKey)?.Stop();
        }

        public MusicChannel GetFreeMusicChannel()
        {
            var result = _musicChannels.FirstOrDefault(c => !c.IsPlay);
            if (result == null)
            {
                result = _musicChannelPool.Get();
            }
            return result;
        }


        public AudioClip GetMusicAudioClip(EMusic musicKey)
        {
            return _musicClipList.FirstOrDefault(clip => clip.name == musicKey.ToString());
        }

        #endregion // MUSIC CONTROLLERS

        #region SOUND CONTROLLERS

        public SoundChannel PlaySound(ESound soundKey, float volume = 1, float delay = 0, bool isLoop = false, bool isSetTimeStop = false,
            float timeStop = 0)
        {
            var channel = GetFreeSoundChannel();
            if (channel == null) return null;

            var soundAudioClip = GetSoundAudioClip(soundKey);
            if (isLoop)
            {
                channel.PlayLoop(soundKey, soundAudioClip, volume, delay, isSetTimeStop, timeStop);
            }
            else
            {
                channel.Play(soundKey, soundAudioClip, volume, delay);
            }

            return channel;
        }

        public SoundChannel GetFreeSoundChannel()
        {
            var result = _soundChannels.FirstOrDefault(c => !c.IsPlay);
            if (result == null)
            {
                result = _soundChannelPool.Get();
            }
            return result;
        }

        public bool IsSoundPlaying(ESound soundKey)
        {
            return _soundChannels.Any(c => c.SoundKey == soundKey);
        }

        public AudioClip GetSoundAudioClip(ESound soundKey)
        {
            return _soundClipList.FirstOrDefault(clip => clip.name == soundKey.ToString());
        }

        public void StopSound(ESound soundKey)
        {
            _soundChannels
                .Where(channel => channel.SoundKey == soundKey)
                .ToList()
                .ForEach(channel => channel.Stop());
        }

        public void StopAllSound()
        {
            _soundChannels.ForEach(channel => channel.Stop());
        }

        #endregion // SOUND CONTROLLERS


#if UNITY_EDITOR
        public List<AudioClip> SoundClips => _soundClipList;
        public List<AudioClip> MusicClips => _musicClipList;

#endif // EDITOR
    }
}