using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

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
public class AudioArrayStorage : SerializableDictionary.Storage<AudioObject[]> { }

[Serializable]
public class AudioDictionary : SerializableDictionary<AudioType, AudioObject[], AudioArrayStorage> { }

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [SerializeField] AudioDictionary audioDictionary;

    [SerializeField] AnimationCurve audioFadeCurve;
    IEnumerator fadeCoroutine;
    float musicAudioMultiplier = 0f;

    [SerializeField] Slider[] volumeSliders;

    PauseHandler pauseHandler;

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

    void PlayMusic(AudioClip clip)
    {
        var audio = Array.Find(audioDictionary[AudioType.Music], a => a.clip == clip);
        if (audio == null) return;

        if (!audio.source.isPlaying)
        {
            audio.source.volume = volumeSliders[(int)AudioType.Music].normalizedValue * musicAudioMultiplier;
            audio.source.Play();
        }
    }

    public void PlaySound(AudioClip clip, bool allowOverlap = false, uint pitchVariance = 0)
    {
        var audio = Array.Find(audioDictionary[AudioType.Sound], a => a.clip == clip);
        if (audio == null) return;

        if (!audio.source.isPlaying || allowOverlap)
        {
            audio.source.volume = volumeSliders[(int)AudioType.Sound].normalizedValue;

            audio.source.pitch = 1f;
            if (pitchVariance > 0)
            {
                //the difference from one semitone to the next = 2^(1/12) = 1.059463(...). take away 1 since it is the default pitch value
                audio.source.pitch += 0.059463f * UnityEngine.Random.Range(-pitchVariance, pitchVariance + 1);
            }

            audio.source.Play();
        }
    }

    //overload that checks Audio
    public void PlaySound(string clipName, bool allowOverlap = false, uint pitchVariance = 0)
    {
        var audioClip = Array.Find(audioDictionary[AudioType.Sound], a => a.name == clipName).clip;
        PlaySound(audioClip, allowOverlap, pitchVariance);
    }

    void Start()
    {
        PlayMusic(audioDictionary[AudioType.Music][0].clip);

        musicAudioMultiplier = 0f;
        FadeAudio(1f, 2f);
    }

    void FadeAudio(float endVolume, float fadeDuration)
    {
        if (fadeCoroutine != null)
        {
            StopCoroutine(fadeCoroutine);
        }

        fadeCoroutine = _FadeAudio(endVolume, fadeDuration);
        StartCoroutine(fadeCoroutine);
    }

    IEnumerator _FadeAudio(float endVolume, float fadeDuration)
    {
        float startVolume = musicAudioMultiplier;
        float currentLerpTime = 0f;

        while (currentLerpTime < fadeDuration)
        {
            yield return null;

            currentLerpTime += Time.unscaledDeltaTime;
            musicAudioMultiplier = Mathf.Lerp(startVolume, endVolume, audioFadeCurve.Evaluate(currentLerpTime / fadeDuration));
            UpdateMusicVolume();
        }

        musicAudioMultiplier = endVolume;
    }

    void UpdateAudioVolume(AudioType audioType)
    {
        for (int i = 0; i < audioDictionary[audioType].Length; i++)
        {
            audioDictionary[audioType][i].source.volume = Mathf.Pow(volumeSliders[(int)audioType].normalizedValue, 1.5f) * musicAudioMultiplier;
        }
    }

    //Button and Slider methods
    public void UpdateMusicVolume() => UpdateAudioVolume(AudioType.Music);
    public void UpdateSoundVolume() => UpdateAudioVolume(AudioType.Sound);
    public void OnTransitionScene() => FadeAudio(0f, 0.8f);
    public void OnQuit() => FadeAudio(0f, 0.5f);

    void OnGamePaused(bool state)
    {
        FadeAudio(state ? 0.2f : 1f, 0.25f);
        //masterAudioMultiplier = state ? 0.5f : 1f;
    }
}