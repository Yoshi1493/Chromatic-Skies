using UnityEngine;

[CreateAssetMenu(fileName = "New Ship", menuName = "Scriptable Object/Ship", order = 0)]
public class ShipObject : ScriptableObject
{
    [Header("Appearance")]
    public Sprite Sprite;
    public ColourObject UIColour;

    [Header("Boundaries")]
    public LayerMask boundaryLayer;

    [Header("Constant stats")]
    public StringObject ShipName;

    public IntReference MaxLives;
    public IntReference MaxHealth;
    public IntReference Power;
    public IntReference Defense;

    public FloatReference MovementSpeed;
    public FloatReference ShootingSpeed;

    [Header("Variable stats")]
    public IntReference CurrentLives;
    public IntReference CurrentHealth;

    [HideInInspector] public float CurrentSpeed;
    [HideInInspector] public bool Invincible;
}