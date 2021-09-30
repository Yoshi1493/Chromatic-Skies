using UnityEngine;
using UnityEngine.UI;

public class LifeCountDisplay : MonoBehaviour
{
    [SerializeField] Enemy enemyShip;

    Image[] lifeIcons;

    void Awake()
    {
        lifeIcons = GetComponentsInChildren<Image>();
        enemyShip.LoseLifeAction += OnEnemyLoseLife;
    }

    void Start()
    {
        for (int i = 0; i < lifeIcons.Length; i++)
        {
            lifeIcons[i].enabled = i < enemyShip.shipData.MaxLives.Value;
            lifeIcons[i].color = enemyShip.shipData.UIColour.value;
        }
    }

    void OnEnemyLoseLife()
    {
        lifeIcons[enemyShip.shipData.CurrentLives.Value].enabled = false;
    }
}