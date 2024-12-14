using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class HealthDisplay<TShip> : ShipHUDComponent<TShip>
    where TShip : Ship
{
    TextMeshProUGUI healthText;
    int maxHealth;

    protected override void Awake()
    {
        base.Awake();

        healthText = GetComponent<TextMeshProUGUI>();
        ship.TakeDamageAction += OnTakeDamage;
        ship.RespawnAction += OnShipRespawn;
    }

    void Start()
    {
        maxHealth = ship.shipData.MaxHealth.Value;
        UpdateDisplay();
    }

    void OnTakeDamage(int _)
    {
        UpdateDisplay();
    }

    void UpdateDisplay()
    {
        int currentHealth = Mathf.Max(ship.currentHealth, 0);
        healthText.text = $"hp: {currentHealth}/{maxHealth}";
    }

    void OnShipRespawn()
    {
        healthText.text = $"hp: {maxHealth}/{maxHealth}";
    }
}