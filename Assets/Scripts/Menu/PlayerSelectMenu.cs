using UnityEngine;
using UnityEngine.UI;

public class PlayerSelectMenu : Menu
{
    [SerializeField] Button backButton;

    [Space]

    [SerializeField] Transform shipButtonParent;
    [SerializeField] IntObject selectedPlayerIndex;

    public void SelectPlayer(int playerIndex)
    {
        selectedPlayerIndex.value = playerIndex;
        LoadScene(1);
    }

    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
            backButton.OnPointerClick(eventData);
    }

    public override void Enable(GameObject newSelectedGameObject)
    {
        newSelectedGameObject = shipButtonParent.GetChild(selectedPlayerIndex.value).gameObject;
        base.Enable(newSelectedGameObject);
    }
}