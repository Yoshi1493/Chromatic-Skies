using UnityEngine;
using UnityEngine.UI;

public class EnemySelectMenu : Menu
{
    [SerializeField] Button backButton;

    [Space]

    [SerializeField] Transform shipButtonParent;
    [SerializeField] IntObject selectedEnemyIndex;

    [Space]

    [SerializeField] ShipObject[] enemyShipData;
    [SerializeField] Image backgroundGlowImage;

    public void SelectEnemy(int enemyIndex)
    {
        selectedEnemyIndex.value = enemyIndex;

        backgroundGlowImage.rectTransform.position = shipButtonParent.GetChild(selectedEnemyIndex.value).GetComponent<RectTransform>().position;

        Color c = enemyShipData[enemyIndex].UIColour.value;
        c.a = 0.2f;
        backgroundGlowImage.color = c;
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
        newSelectedGameObject = shipButtonParent.GetComponentsInChildren<Button>()[selectedEnemyIndex.value].gameObject;
        base.Enable(newSelectedGameObject);
    }
}