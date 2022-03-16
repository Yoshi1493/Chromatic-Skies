using UnityEngine;

[System.Serializable]
[CreateAssetMenu]
public class UserSettings : ScriptableObject
{
    [SerializeField] FloatObject musicVolume;
    public float MusicVolume
    {
        get => musicVolume.value;
        set => musicVolume.value = value;
    }

    [SerializeField] FloatObject soundVolume;
    public float SoundVolume
    {
        get => soundVolume.value;
        set => soundVolume.value = value;
    }
}