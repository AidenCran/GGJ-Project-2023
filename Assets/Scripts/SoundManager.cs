using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SoundManager : MonoBehaviour
{
    #region Singleton

    public static SoundManager instance { get; private set; }

    private void Singleton()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    #endregion

    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource soundSource;

    [SerializeField] AudioClip defaultMusic;
    
    [SerializeField] List<AudioClip> menuMusic;
    [SerializeField] List<AudioClip> levelMusic;
    
    
    float _musicMaxVolume;
    float _setVolume;

    float _duration = 3;

    public float MaxVolume => _musicMaxVolume;

    public float musicVol => musicSource.volume;
    public float soundVol => soundSource.volume;

    public void SetMusicVolume(float x)
    {
        _setVolume = x;
        musicSource.volume = x;
    }

    public void SetSoundVolume(float x)
    {
        _setVolume = x;
        soundSource.volume = x;
    }

    void Awake()
    {
        Singleton();
    }

    void Start()
    {
        // if (defaultMusic == null) return;
        // PlayMusic(defaultMusic);
        PlayMenuMusic();
    }

    public void PlayMusic(AudioClip clip)
    {
        musicSource.clip = clip;
        musicSource.Play();
    }

    public void PlaySound(AudioClip clip)
    {
        soundSource.clip = clip;
        soundSource.Play();
    }

    public void PlayMenuMusic()
    {
        PlayMusic(menuMusic.SelectRandom());
    }

    
    [SerializeField] AudioClip FirstLevelClip;
    public void PlayLevelMusic()
    {
        StartCoroutine(PlayLevelMusic(FirstLevelClip));
    }

    IEnumerator PlayLevelMusic(AudioClip clip)
    {
        StopAllCoroutines();

        PlayMusic(clip);
        yield return Helper.GetWait(clip.length);
        StartCoroutine(PlayLevelMusic(levelMusic.SelectRandom()));
    }

    public void PlayDefaultMusic() => PlayMusic(defaultMusic);
}
