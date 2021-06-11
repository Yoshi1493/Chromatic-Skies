using UnityEngine;

[CreateAssetMenu(fileName = "New Ship", menuName = "Scriptable Object/Ship", order = 1)]
public class ShipObject : ScriptableObject
{
    [Header("General")]
    public Sprite sprite;
    public StringObject shipName;

    [Header("Stats")]
    public int maxHealth;
    public IntObject currentHealth;

    public int originalPower;
    public IntObject currentPower;

    public int originalDefense;
    public IntObject currentDefense;

    public float originalMovementSpeed;
    public FloatObject currentMovementSpeed;

    public float originalShootingSpeed;
    public FloatObject currentShootingSpeed;

    [Header("Bullets")]
    public GameObject defaultBullet;
}