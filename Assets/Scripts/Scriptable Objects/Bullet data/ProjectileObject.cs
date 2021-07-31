using UnityEngine;

[CreateAssetMenu (fileName = "New Projectile", menuName = "Scriptable Object/Projectile", order = 1)]
public class ProjectileObject : ScriptableObject
{
    [Header("Appearance")]
    public Sprite sprite;

    [Header("Stats")]
    public IntObject Power;
    public FloatReference MoveSpeed;
}