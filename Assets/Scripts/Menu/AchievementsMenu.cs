using UnityEngine;
using UnityEngine.UI;

public class AchievementsMenu : Menu
{
    [SerializeField] UserSettings userSettings;

    [SerializeField] Button backButton;

    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            backButton.OnPointerClick(eventData);
        }
    }
}