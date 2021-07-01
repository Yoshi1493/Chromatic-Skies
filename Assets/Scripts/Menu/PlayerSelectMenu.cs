using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerSelectMenu : Menu
{
    EventSystem currentEventSystem;
    StandaloneInputModule inputModule;

    public event Action<int> SelectedPlayerChangeAction;

    protected override void Awake()
    {
        base.Awake();

        currentEventSystem = EventSystem.current;
        inputModule = FindObjectOfType<StandaloneInputModule>();
    }

    void Update()
    {
        if (Input.GetButtonDown(inputModule.horizontalAxis))
        {
            int currentSelectedIndex = currentEventSystem.currentSelectedGameObject.transform.GetSiblingIndex();
            SelectedPlayerChangeAction?.Invoke(currentSelectedIndex);
        }
    }
}