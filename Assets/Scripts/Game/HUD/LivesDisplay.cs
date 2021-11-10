using System.Collections;
using UnityEngine.UI;
using static CoroutineHelper;

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

        for (int i = 0; i < ship.shipData.MaxLives.Value; i++)
        {
            lifeIcons[i].enabled = true;
            yield return WaitForSeconds(0.1f);
        }
    }

    void OnLoseLife()
    {
        for (int i = 0; i < lifeIcons.Length; i++)
        {
            lifeIcons[i].enabled = i < ship.shipData.CurrentLives.Value;
        }
    }
}