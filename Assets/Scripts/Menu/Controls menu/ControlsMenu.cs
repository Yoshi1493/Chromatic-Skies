using UnityEngine;
using UnityEngine.UI;

public class ControlsMenu : Menu
{
    [SerializeField] Button backButton;

    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            backButton.OnPointerClick(eventData);
        }
    }
}