using UnityEngine;

[CreateAssetMenu]
public class UserSettings : ScriptableObject
{
    int selectedPlayer;
    public int SelectedPlayer
    {
        get => selectedPlayer;
        set => selectedPlayer = value;
    }
}