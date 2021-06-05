using UnityEngine;

[CreateAssetMenu(fileName = "New Ship", menuName = "Scriptable Object/Ship", order = 1)]
public class ShipObject : ScriptableObject
{
    [Header("General")]
    public StringObject shipName;    

    [Header("Stats")]
    public IntObject maxHealth;
    public IntObject power;
    public IntObject defense;
    public FloatObject movementSpeed;
    public FloatObject shootingSpeed;
}