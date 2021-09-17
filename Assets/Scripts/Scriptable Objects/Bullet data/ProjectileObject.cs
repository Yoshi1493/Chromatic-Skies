using UnityEngine;

[CreateAssetMenu (fileName = "New Projectile", menuName = "Scriptable Object/Projectile", order = 1)]
public class ProjectileObject : ScriptableObject
{
    [Header("Appearance")]
    public Sprite sprite;
    public Gradient colour;

    [Header("Stats")]
    public int BulletID;
    public IntObject Power;
}