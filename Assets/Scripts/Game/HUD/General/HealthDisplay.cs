using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class HealthDisplay<TShip> : ShipHUDComponent<TShip>
    where TShip : CharacterShip
{
    protected TextMeshProUGUI healthText;

    [SerializeField] IntObject currentHealth;
    int maxHealth;

    protected override void Awake()
    {
        base.Awake();
        healthText = GetComponent<TextMeshProUGUI>();
    }

    void OnEnable()
    {
        healthText.enabled = true;
        maxHealth = ship.shipData.MaxHealth.Value;
    }

    void Update()
    {
        healthText.text = $"hp: {currentHealth.value}/{maxHealth}";
    }
}