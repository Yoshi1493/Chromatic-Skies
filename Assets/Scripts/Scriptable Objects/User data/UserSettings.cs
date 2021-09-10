using UnityEngine;

[CreateAssetMenu]
public class UserSettings : ScriptableObject
{
    [SerializeField] IntObject selectedPlayer;
    public int SelectedPlayer
    {
        get => selectedPlayer.value;
        set => selectedPlayer.value = value;
    }

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