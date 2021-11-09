using UnityEngine.UI;

public class LivesDisplay<TShip> : HUDComponent<TShip>
    where TShip : Ship
{
    Image[] lifeIcons;

    protected override void Awake()
    {
        base.Awake();

        ship.LoseLifeAction += OnLoseLife;
        lifeIcons = GetComponentsInChildren<Image>();
    }

    void Start()
    {
        InitColour();
        UpdateDisplay();
    }

    void InitColour()
    {
        for (int i = 0; i < lifeIcons.Length; i++)
        {
            lifeIcons[i].color = ship.shipData.UIColour.value;
        }
    }

    void UpdateDisplay()
    {
        for (int i = 0; i < lifeIcons.Length; i++)
        {
            lifeIcons[i].enabled = i < ship.shipData.CurrentLives.Value;
        }
    }

    void OnLoseLife()
    {
        UpdateDisplay();
    }
}