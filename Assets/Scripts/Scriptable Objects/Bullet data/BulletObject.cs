using UnityEngine;

[CreateAssetMenu (fileName = "New Bullet", menuName = "Scriptable Object/Bullet", order = 1)]
public class BulletObject : ScriptableObject
{
    public Bullet bullet;
    public Sprite sprite;
}