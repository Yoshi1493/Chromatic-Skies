using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerSelectMenu : Menu
{
    [SerializeField] UserSettings userSettings;

    [Space]

    [SerializeField] Button backButton;
    PointerEventData eventData = new PointerEventData(EventSystem.current);
    
    [Space]

    [SerializeField] Transform shipButtonParent;
    [SerializeField] IntObject selectedPlayerIndex;

    public void SelectPlayer(int playerIndex)
    {
        userSettings.SelectedPlayer = playerIndex;
        LoadScene(1);
    }

    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            backButton.OnPointerClick(eventData);
        }
    }

    public override void Enable(GameObject newSelectedGameObject)
    {
        newSelectedGameObject = shipButtonParent.GetChild(selectedPlayerIndex.value).gameObject;
        base.Enable(newSelectedGameObject);
    }
}