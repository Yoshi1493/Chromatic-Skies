using UnityEngine;
using UnityEngine.UI;

public class AchievementsMenu : Menu
{
    [SerializeField] Button backButton;

    void Update()
    {
        if (backInput.WasPressedThisFrame())
        {
            backButton.OnPointerClick(eventData);
        }
    }
}