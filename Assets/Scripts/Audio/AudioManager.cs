using System;
using UnityEngine;

enum AudioType
{
    Music = 0,
    Sound = 1
}

[Serializable]
public class MusicTrack
{
    public AudioClip clip;

    [HideInInspector] public AudioSource source;
    [HideInInspector] public string name;

    readonly AudioType audioType = AudioType.Music;
}

[Serializable]
public class SoundEffect
{
    public AudioClip clip;

    [HideInInspector] public AudioSource source;
    [HideInInspector] public string name;

    readonly AudioType audioType = AudioType.Sound;
}

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [SerializeField] MusicTrack[] backgroundMusic;
    [SerializeField] SoundEffect[] soundEffects;

    [SerializeField] Transform bgmParent;
    [SerializeField] Transform sfxParent;

    void Awake()
    {
        if (Instance == null) Instance = this;
        InitAudio();
    }

    //create bgm and sfx gameobjects
    void InitAudio()
    {
        foreach (var bgm in backgroundMusic)
        {
            bgm.source = bgmParent.gameObject.AddComponent<AudioSource>();
            bgm.source.clip = bgm.clip;
            bgm.name = bgm.source.clip.name;
        }

        foreach (var sfx in soundEffects)
        {
            sfx.source = sfxParent.gameObject.AddComponent<AudioSource>();
            sfx.source.clip = sfx.clip;
            sfx.name = sfx.source.clip.name;
        }
    }

    //play sfx by passing SoundEffect name
    public void PlaySound(string name, bool allowOverlap = false)
    {
        SoundEffect sound = Array.Find(soundEffects, s => s.name == name);
        _PlaySound(sound, allowOverlap);
    }

    //overload; play sfx by passing AudioClip object
    public void PlaySound(AudioClip clip, bool allowOverlap = false)
    {
        SoundEffect sound = Array.Find(soundEffects, s => s.clip = clip);
        _PlaySound(sound, allowOverlap);
    }

    void _PlaySound(SoundEffect sound, bool allowOverlap)
    {
        if (sound == null) return;

        if (!sound.source.isPlaying || allowOverlap)
        {
            sound.source.Play();
        }
    }
}