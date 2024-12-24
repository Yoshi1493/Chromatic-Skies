using UnityEngine;
using UnityEngine.UI;

public class PlayerSelectMenu : Menu
{
    [SerializeField] Button backButton;

    [Space]

    Button[] playerButtons;
    [SerializeField] Transform shipButtonParent;
    [SerializeField] IntObject selectedPlayerIndex;

    protected override void Awake()
    {
        base.Awake();
        playerButtons = shipButtonParent.GetComponentsInChildren<Button>();
    }

    public void SelectPlayer(int playerIndex)
    {
        selectedPlayerIndex.value = playerIndex;
    }

    void Update()
    {
        if (Input.GetButtonDown("Horizontal"))
        {
            for (int i = 0; i < playerButtons.Length; i++)
            {
                if (i != selectedPlayerIndex.value)
                {
                    playerButtons[i].OnPointerUp(eventData);
                }
            }
        }

        if (Input.GetButtonDown("Cancel"))
        {
            backButton.OnPointerClick(eventData);
        }
    }

    public override void Enable(GameObject newSelectedGameObject)
    {
        newSelectedGameObject = playerButtons[selectedPlayerIndex.value].gameObject;
        base.Enable(newSelectedGameObject);
    }
}