using System;

[Serializable]
public class UserData 
{
    public float MusicVolume;
    public float SoundVolume;

    public UserData()
    {
        MusicVolume = 0.8f;
        SoundVolume = 0.8f;
    }
}