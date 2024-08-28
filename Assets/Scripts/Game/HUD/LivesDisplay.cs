using System.Collections;
using UnityEngine.UI;
using static CoroutineHelper;

public class LivesDisplay<TShip> : ShipHUDComponent<TShip>
    where TShip : Ship
{
    Image[] lifeIcons;

    protected override void Awake()
    {
        base.Awake();

        lifeIcons = GetComponentsInChildren<Image>();

        if (ship != null)
        {
            ship.LoseLifeAction += OnLoseLife;
        }
    }

    protected virtual void Start()
    {
        InitColour();
        StartCoroutine(InitDisplay());
    }

    protected void InitColour()
    {
        for (int i = 0; i < lifeIcons.Length; i++)
        {
            lifeIcons[i].color = ship.shipData.UIColour.value;
        }
    }

    IEnumerator InitDisplay()
    {
        yield return WaitForSeconds(1f);

        int lifeCount = ship.shipData.MaxLives.Value;

        for (int i = 0; i < lifeCount; i++)
        {
            lifeIcons[i].enabled = true;
            yield return WaitForSeconds(0.1f);
        }
    }

    void OnLoseLife()
    {
        for (int i = 0; i < lifeIcons.Length; i++)
        {
            lifeIcons[i].enabled = i < ship.currentLives;
        }
    }
}