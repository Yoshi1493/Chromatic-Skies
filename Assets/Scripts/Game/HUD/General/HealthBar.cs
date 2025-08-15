using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class HealthBar<TShip> : ShipHUDComponent<TShip>
    where TShip : CharacterShip
{
    protected Image healthBarImage;

    [SerializeField] IntObject currentHealth;
    int maxHealth;

    protected override void Awake()
    {
        base.Awake();
        healthBarImage = GetComponent<Image>();
    }

    void OnEnable()
    {
        healthBarImage.enabled = true;
        healthBarImage.color = ship.shipData.UIColour.value;
        maxHealth = ship.shipData.MaxHealth.Value;
    }

    void Update()
    {
        healthBarImage.fillAmount = (float)currentHealth.value / maxHealth;
    }
}