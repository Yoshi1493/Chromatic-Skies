using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerSelectMenu : Menu
{
    [SerializeField] Button backButton;

    [Space]

    [SerializeField] Button[] playerButtons;
    [SerializeField] IntObject selectedPlayerIndex;
    [SerializeField] IntObject selectedBossIndex;
    [SerializeField] StatBarController statBarController;

    public override void Enable(GameObject newSelectedGameObject)
    {
        newSelectedGameObject = playerButtons[selectedPlayerIndex.value].gameObject;
        base.Enable(newSelectedGameObject);
    }

    void SelectPlayer(int playerIndex)
    {
        if (!EventSystem.current.alreadySelecting)
        {
            EventSystem.current.SetSelectedGameObject(playerButtons[playerIndex].gameObject);
        }

        selectedPlayerIndex.value = playerIndex;
        statBarController.AnimateStatBars(selectedPlayerIndex.value);
    }

    public void SelectNextPlayer()
    {
        SelectPlayer((int)Mathf.Repeat(selectedPlayerIndex.value + 1, playerButtons.Length));
    }

    public void SelectPreviousPlayer()
    {
        SelectPlayer((int)Mathf.Repeat(selectedPlayerIndex.value - 1, playerButtons.Length));
    }

    public void OnSelectPlayer()
    {
        selectedBossIndex.value = 0;
    }

    void Update()
    {
        if (backInput.WasPressedThisFrame())
        {
            backButton.OnPointerClick(eventData);
        }
    }
}