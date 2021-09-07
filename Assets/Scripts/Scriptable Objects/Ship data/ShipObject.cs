using UnityEngine;

[CreateAssetMenu(fileName = "New Ship", menuName = "Scriptable Object/Ship", order = 0)]
public class ShipObject : ScriptableObject
{
    [Header("Appearance")]
    public Sprite Sprite;
    public ColourObject UIColour;

    [Header("Stats")]
    public StringObject ShipName;
    public IntReference Lives;

    public IntReference Health;
    public IntReference Power;
    public IntReference Defense;

    public FloatReference MovementSpeed;
    public FloatReference ShootingSpeed;
}