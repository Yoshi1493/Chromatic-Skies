using UnityEngine;

public class PlayerSelectMenu : Menu
{
    [SerializeField] UserSettings userSettings;

    public void SelectPlayer(int playerIndex)
    {
        userSettings.SelectedPlayer = playerIndex;
        LoadScene(1);
    }
}