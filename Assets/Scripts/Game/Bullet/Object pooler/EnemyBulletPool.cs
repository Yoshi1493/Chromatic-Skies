using UnityEngine;

public class EnemyBulletPool : GenericProjectilePool<EnemyBullet>
{
    [SerializeField] EnemyBulletSystem[] bulletSystems;
}