using System;

[Serializable]
public class UserData 
{
    public float MusicVolume;
    public float SoundVolume;
    public float BackgroundDim;

    public UserData()
    {
        MusicVolume = 0.8f;
        SoundVolume = 0.8f;
        BackgroundDim = 0.3f;
    }
}