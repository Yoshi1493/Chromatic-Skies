using UnityEngine.UI;

public class LifeCountDisplay : HUDComponent<Enemy>
{
    Image[] lifeIcons;

    protected override void Awake()
    {
        base.Awake();
        ship.LoseLifeAction += OnEnemyLoseLife;

        lifeIcons = GetComponentsInChildren<Image>();
    }

    void Start()
    {
        for (int i = 0; i < lifeIcons.Length; i++)
        {
            lifeIcons[i].enabled = i < ship.shipData.MaxLives.Value;
            lifeIcons[i].color = ship.shipData.UIColour.value;
        }
    }

    void OnEnemyLoseLife()
    {
        lifeIcons[ship.shipData.CurrentLives.Value].enabled = false;
    }
}