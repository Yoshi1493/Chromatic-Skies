using UnityEngine;

[CreateAssetMenu (fileName = "New Projectile", menuName = "Scriptable Object/Projectile", order = 1)]
public class ProjectileObject : ScriptableObject
{
    [Header("Appearance")]
    public Sprite sprite;

    public bool useSolidColour;

    public Color colour;
    public Gradient gradient;

    [Header("Stats")]
    public int ID;
    public IntObject Power;
}