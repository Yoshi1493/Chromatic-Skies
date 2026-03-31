using UnityEngine;
using UnityEngine.UI;

public class BossSelectMenu : Menu
{
    [SerializeField] Button backButton;

    [Space]

    [SerializeField] Transform shipButtonParent;
    [SerializeField] IntObject selectedBossIndex;

    [Space]

    [SerializeField] ShipObject[] bossShipData;
    [SerializeField] Image backgroundGlowImage;

    public void SelectBoss(int bossIndex)
    {
        selectedBossIndex.value = bossIndex;

        backgroundGlowImage.rectTransform.position = shipButtonParent.GetChild(selectedBossIndex.value).GetComponent<RectTransform>().position;

        Color c = bossShipData[bossIndex].UIColour.value;
        c.a = 0.2f;
        backgroundGlowImage.color = c;
    }

    void Update()
    {
        if (backInput.WasPressedThisFrame())
        {
            backButton.OnPointerClick(eventData);
        }
    }

    public override void Enable(GameObject newSelectedGameObject)
    {
        newSelectedGameObject = shipButtonParent.GetComponentsInChildren<Button>()[selectedBossIndex.value].gameObject;
        base.Enable(newSelectedGameObject);
    }
}