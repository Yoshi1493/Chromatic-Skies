using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Bullet System", menuName = "Scriptable Object/Bullet System", order = 2)]
public class BulletSystem : ScriptableObject
{
    public List<Bullet> bullets;

    public StringObject attackName;
    public float initialDelay;
}