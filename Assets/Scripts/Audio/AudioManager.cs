using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using static CoroutineHelper;

[Serializable]
public enum AudioType
{
    Music = 0,
    Sound = 1
}

[Serializable]
public class AudioObject
{
    public AudioClip clip;

    [HideInInspector] public AudioSource source;
    [HideInInspector] public string name;
}

[Serializable]
public class AudioArrayStorage : SerializableDictionary.Storage<AudioObject[]>
{ }

[Serializable]
public class AudioDictionary : SerializableDictionary<AudioType, AudioObject[], AudioArrayStorage>
{ }

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [SerializeField] AudioDictionary audioDictionary;

    [SerializeField] AnimationCurve audioFadeInCurve;
    float masterAudioMultiplier = 0f;

    [SerializeField] Slider[] volumeSliders;

    PauseHandler pauseHandler;
    const float AudioMultiplierWhilePaused = 0.5f;

    void Awake()
    {
        if (Instance == null) Instance = this;
        InitAudio();

        pauseHandler = FindObjectOfType<PauseHandler>();
        if (pauseHandler != null)
        {
            pauseHandler.GamePauseAction += OnGamePaused;
        }
    }

    //create bgm and sfx gameobjects
    void InitAudio()
    {
        foreach (var (audioType, audioList) in audioDictionary)
        {
            for (int i = 0; i < audioList.Length; i++)
            {
                audioList[i].source = transform.GetChild((int)audioType).gameObject.AddComponent<AudioSource>();
                audioList[i].source.clip = audioList[i].clip;
                audioList[i].name = audioList[i].source.clip.name;
            }
        }
    }

    public void PlayAudio(AudioClip clip, AudioType audioType, bool allowOverlap = false)
    {
        var audio = Array.Find(audioDictionary[audioType], a => a.clip = clip);

        if (audio == null) return;

        if (!audio.source.isPlaying || allowOverlap)
        {
            if (audioType == AudioType.Music)
            {
                PlayMusic(audio);
            }
            else if (audioType == AudioType.Sound)
            {
                PlaySound(audio, allowOverlap);
            }
        }
    }

    void PlayMusic(AudioObject music)
    {
        if (!music.source.isPlaying)
        {
            music.source.volume = volumeSliders[(int)AudioType.Music].normalizedValue * masterAudioMultiplier;
            music.source.Play();
        }
    }

    void PlaySound(AudioObject sound, bool allowOverlap)
    {
        if (!sound.source.isPlaying || allowOverlap)
        {
            sound.source.volume = volumeSliders[(int)AudioType.Sound].normalizedValue * masterAudioMultiplier;
            sound.source.Play();
        }
    }

    //fade-in audio
    IEnumerator Start()
    {
        PlayAudio(audioDictionary[AudioType.Music][0].clip, AudioType.Music);

        float currentLerpTime = 0f;
        float totalLerpTime = 2f;

        while (currentLerpTime < totalLerpTime)
        {
            float udt = Time.unscaledDeltaTime;
            yield return WaitForSecondsRealtime(udt);

            currentLerpTime += udt;
            masterAudioMultiplier = audioFadeInCurve.Evaluate(currentLerpTime / totalLerpTime);
            UpdateMusicVolume();
        }

        masterAudioMultiplier = 1f;
    }

    void UpdateAudioVolume(AudioType audioType)
    {
        //to-do: update audio volume multiplier
        for (int i = 0; i < audioDictionary[audioType].Length; i++)
        {
            audioDictionary[audioType][i].source.volume = volumeSliders[(int)audioType].normalizedValue * masterAudioMultiplier;
        }
    }

    //public Slider OnChange methods
    public void UpdateMusicVolume() => UpdateAudioVolume(AudioType.Music);
    public void UpdateSoundVolume() => UpdateAudioVolume(AudioType.Sound);

    void OnGamePaused(bool state)
    {
        masterAudioMultiplier = state ? AudioMultiplierWhilePaused : 1f;
    }
}