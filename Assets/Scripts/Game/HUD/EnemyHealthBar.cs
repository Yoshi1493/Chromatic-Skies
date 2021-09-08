using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    [SerializeField] Enemy enemyShip;

    Image healthBarImage;

    void Awake()
    {
        healthBarImage = GetComponent<Image>();
        healthBarImage.color = enemyShip.shipData.UIColour.value;
    }

    void Update()
    {
        healthBarImage.fillAmount = (float)enemyShip.shipData.CurrentHealth.Value / enemyShip.shipData.MaxHealth.Value;
    }
}