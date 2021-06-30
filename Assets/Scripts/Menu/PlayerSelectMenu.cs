using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerSelectMenu : Menu
{
    EventSystem currentEventSystem;
    StandaloneInputModule inputModule;

    [SerializeField] ShipObject[] players;

    PlayerStatBar[] statBars;

    protected override void Awake()
    {
        base.Awake();

        statBars = GetComponentsInChildren<PlayerStatBar>();

        currentEventSystem = EventSystem.current;
        inputModule = FindObjectOfType<StandaloneInputModule>();
    }

    void Update()
    {
        if (Input.GetButtonDown(inputModule.horizontalAxis))
        {
            int currentSelectedIndex = currentEventSystem.currentSelectedGameObject.transform.GetSiblingIndex();

            foreach (var statBar in statBars)
            {
                statBar.LerpFillAmount(currentSelectedIndex);
            }
        }
    }
}