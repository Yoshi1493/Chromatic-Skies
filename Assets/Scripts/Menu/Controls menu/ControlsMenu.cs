using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ControlsMenu : Menu
{
    [SerializeField] GameObject menuContentL;
    [SerializeField] GameObject menuContentR;
    Button[] menuButtonsL;
    Button[] menuButtonsR;

    [SerializeField] Button backButton;

    GameObject lastSelectedNonBackButton;
    bool updatedBackButtonNavigation;

    protected override void Awake()
    {
        base.Awake();

        menuButtonsL = menuContentL.GetComponentsInChildren<Button>();
        menuButtonsR = menuContentR.GetComponentsInChildren<Button>();
    }

    void Update()
    {
        if (backInput.WasPressedThisFrame())
        {
            backButton.OnPointerClick(eventData);
        }

        if (EventSystem.current.currentSelectedGameObject != backButton.gameObject)
        {
            lastSelectedNonBackButton = EventSystem.current.currentSelectedGameObject;
            updatedBackButtonNavigation = false;
        }
        else
        {
            if (!updatedBackButtonNavigation)
            {
                Navigation nav = new()
                {
                    mode = Navigation.Mode.Explicit
                };

                if (Array.Exists(menuButtonsL, b => b == lastSelectedNonBackButton.GetComponent<Button>()))
                {
                    nav.selectOnDown = menuButtonsL[0];
                    nav.selectOnUp = menuButtonsL[^1];
                }
                if (Array.Exists(menuButtonsR, b => b == lastSelectedNonBackButton.GetComponent<Button>()))
                {
                    nav.selectOnDown = menuButtonsR[0];
                    nav.selectOnUp = menuButtonsR[^1];
                }

                backButton.navigation = nav;

                updatedBackButtonNavigation = true;
            }
        }
    }

}