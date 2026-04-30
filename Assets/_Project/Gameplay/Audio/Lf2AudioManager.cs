using System.Collections.Generic;
using UnityEngine;

namespace Project.Gameplay.Audio
{
    public enum Lf2SoundId
    {
        None = 0,

        HitPunch1 = 1,
        HitPunch2 = 2,
        HitPunch3 = 3,
        HitKick1 = 4,
        HitKick2 = 5,
        HitKick3 = 6,
        HitHeavy1 = 7,
        HitHeavy2 = 8,
        HitSlash1 = 9,
        HitSlash2 = 10,
        HitBleed = 11,

        SpecialCharge = 13,
        SpecialRelease1 = 14,
        SpecialRelease2 = 15,
        SpecialRelease3 = 16,
        SpecialImpact1 = 17,
        SpecialImpact2 = 18,

        ProjectileSpawn = 19,
        ProjectileImpact1 = 20,
        ProjectileImpact2 = 21,

        DefendBlock = 22,
        DefendBreak = 23,

        Footstep1 = 24,
        Footstep2 = 25,
        Jump = 26,
        Land = 27,
        Dash = 28,

        PickupItem = 29,
        DrinkMilk = 30,
        DrinkBeer = 31,

        Death = 32,
        KO = 33,

        MenuOk = 40,
        MenuCancel = 41,
        MenuJoin = 42,
        MenuSelect = 43,
        MenuCursor = 44,

        RoundStart = 50,
        RoundEnd = 51,
        Victory = 52,

        StageMusic1 = 60,
        StageMusic2 = 61,
        StageMusic3 = 62,

        StatusBurn = 70,
        StatusFreeze = 71,
        StatusBleed = 72,

        CrowdCheer = 80,
        CrowdGasp = 81,
    }

    [DisallowMultipleComponent]
    public sealed class Lf2AudioManager : MonoBehaviour
    {
        private static Lf2AudioManager _instance;
        public static Lf2AudioManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = FindAnyObjectByType<Lf2AudioManager>();
                return _instance;
            }
        }

        [SerializeField] private int sfxSourcePoolSize = 8;

        private AudioSource _musicSource;
        private readonly List<AudioSource> _sfxPool = new List<AudioSource>(16);
        private readonly Dictionary<int, AudioClip> _clipCache = new Dictionary<int, AudioClip>(128);
        private Transform _tr;

        private float _masterVolume = 1f;
        private float _sfxVolume = 1f;
        private float _musicVolume = 0.7f;

        public float MasterVolume
        {
            get => _masterVolume;
            set => _masterVolume = Mathf.Clamp01(value);
        }

        public float SfxVolume
        {
            get => _sfxVolume;
            set => _sfxVolume = Mathf.Clamp01(value);
        }

        public float MusicVolume
        {
            get => _musicVolume;
            set
            {
                _musicVolume = Mathf.Clamp01(value);
                if (_musicSource != null)
                    _musicSource.volume = _musicVolume * _masterVolume;
            }
        }

        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(gameObject);
                return;
            }

            _instance = this;
            _tr = transform;
            DontDestroyOnLoad(gameObject);

            BuildClipCache();
            CreateSfxPool();
            CreateMusicSource();
        }

        private void OnDestroy()
        {
            if (_instance == this)
                _instance = null;
        }

        public void PlaySfx(Lf2SoundId soundId, float volumeScale = 1f, float pitch = 1f)
        {
            if (soundId == Lf2SoundId.None) return;

            var clip = GetClip((int)soundId);
            if (clip == null) return;

            var src = GetFreeSfxSource();
            if (src == null) return;

            src.pitch = pitch;
            src.volume = _sfxVolume * _masterVolume * volumeScale;
            src.PlayOneShot(clip);
        }

        public void PlaySfx(int wavId, float volumeScale = 1f, float pitch = 1f)
        {
            if (wavId <= 0) return;

            var clip = GetClip(wavId);
            if (clip == null) return;

            var src = GetFreeSfxSource();
            if (src == null) return;

            src.pitch = pitch;
            src.volume = _sfxVolume * _masterVolume * volumeScale;
            src.PlayOneShot(clip);
        }

        public void PlaySoundByPath(string soundPath)
        {
            if (string.IsNullOrEmpty(soundPath)) return;
            int wavId = ParseSoundPath(soundPath);
            if (wavId > 0)
                PlaySfx(wavId);
        }

        public void PlayHitSound(int itrKind, int damage)
        {
            float pitch = 1f + Random.Range(-0.05f, 0.05f);
            float volume = Mathf.Lerp(0.7f, 1f, Mathf.Clamp01(damage / 20f));

            Lf2SoundId soundId = itrKind switch
            {
                0 => damage > 12 ? Lf2SoundId.HitHeavy1 : Lf2SoundId.HitPunch1,
                1 => damage > 12 ? Lf2SoundId.HitHeavy2 : Lf2SoundId.HitKick1,
                2 => Lf2SoundId.HitSlash1,
                3 => Lf2SoundId.HitBleed,
                _ => Lf2SoundId.HitPunch1,
            };

            PlaySfx(soundId, volume, pitch);
        }

        public void PlayMenuSfx(Lf2SoundId soundId)
        {
            if (soundId < Lf2SoundId.MenuOk || soundId > Lf2SoundId.MenuCursor) return;
            PlaySfx(soundId, 0.8f);
        }

        public void PlayMusic(AudioClip clip, bool loop = true)
        {
            if (clip == null) return;

            _musicSource.clip = clip;
            _musicSource.loop = loop;
            _musicSource.volume = _musicVolume * _masterVolume;
            _musicSource.Play();
        }

        public void PlayMusicById(int stageMusicId)
        {
            var clip = GetClip(stageMusicId);
            if (clip != null)
                PlayMusic(clip);
        }

        public void PlayStageMusic(int stageIndex)
        {
            int musicId = (int)Lf2SoundId.StageMusic1 + Mathf.Clamp(stageIndex, 0, 2);
            PlayMusicById(musicId);
        }

        public void PlaySpecialMoveSound(int specialType)
        {
            Lf2SoundId soundId = specialType switch
            {
                0 => Lf2SoundId.SpecialCharge,
                1 => Lf2SoundId.SpecialRelease1,
                2 => Lf2SoundId.SpecialRelease2,
                3 => Lf2SoundId.SpecialRelease3,
                _ => Lf2SoundId.SpecialCharge,
            };
            PlaySfx(soundId);
        }

        public void StopMusic()
        {
            if (_musicSource != null && _musicSource.isPlaying)
                _musicSource.Stop();
        }

        public void PauseMusic()
        {
            if (_musicSource != null && _musicSource.isPlaying)
                _musicSource.Pause();
        }

        public void ResumeMusic()
        {
            if (_musicSource != null && !_musicSource.isPlaying && _musicSource.clip != null)
                _musicSource.UnPause();
        }

        public void StopAll()
        {
            StopMusic();
            for (int i = 0; i < _sfxPool.Count; i++)
            {
                if (_sfxPool[i] != null)
                    _sfxPool[i].Stop();
            }
        }

        private void BuildClipCache()
        {
            _clipCache.Clear();

            var allClips = Resources.LoadAll<AudioClip>("Audio/lf2_ref");
            if (allClips != null)
            {
                for (int i = 0; i < allClips.Length; i++)
                {
                    var clip = allClips[i];
                    if (clip == null) continue;

                    if (int.TryParse(clip.name, out int id))
                        _clipCache[id] = clip;
                    else
                        _clipCache[clip.name.GetHashCode() & 0x7FFFFFFF] = clip;
                }
            }

            var projectClips = Resources.LoadAll<AudioClip>("Audio");
            if (projectClips != null)
            {
                for (int i = 0; i < projectClips.Length; i++)
                {
                    var clip = projectClips[i];
                    if (clip == null) continue;

                    if (int.TryParse(clip.name, out int id))
                    {
                        if (!_clipCache.ContainsKey(id))
                            _clipCache[id] = clip;
                    }
                }
            }

            Debug.Log($"[Lf2AudioManager] Loaded {_clipCache.Count} audio clips");
        }

        private void CreateSfxPool()
        {
            _sfxPool.Clear();
            int count = Mathf.Max(1, sfxSourcePoolSize);

            for (int i = 0; i < count; i++)
            {
                var go = new GameObject($"SfxSource_{i}");
                go.transform.SetParent(_tr, false);
                var src = go.AddComponent<AudioSource>();
                src.playOnAwake = false;
                src.loop = false;
                src.spatialBlend = 0f;
                src.priority = 128;
                _sfxPool.Add(src);
            }
        }

        private void CreateMusicSource()
        {
            var go = new GameObject("MusicSource");
            go.transform.SetParent(_tr, false);
            _musicSource = go.AddComponent<AudioSource>();
            _musicSource.playOnAwake = false;
            _musicSource.loop = true;
            _musicSource.spatialBlend = 0f;
            _musicSource.priority = 64;
        }

        private AudioSource GetFreeSfxSource()
        {
            for (int i = 0; i < _sfxPool.Count; i++)
            {
                if (!_sfxPool[i].isPlaying)
                    return _sfxPool[i];
            }

            return _sfxPool.Count > 0 ? _sfxPool[0] : null;
        }

        private AudioClip GetClip(int id)
        {
            return _clipCache.TryGetValue(id, out var clip) ? clip : null;
        }

        private static int ParseSoundPath(string soundPath)
        {
            if (string.IsNullOrEmpty(soundPath)) return 0;

            int slash = soundPath.LastIndexOf('/');
            int dot = soundPath.LastIndexOf('.');
            if (slash < 0) slash = -1;

            string numStr = dot > slash
                ? soundPath.Substring(slash + 1, dot - slash - 1)
                : soundPath.Substring(slash + 1);

            return int.TryParse(numStr, out var id) ? id : 0;
        }
    }
}
