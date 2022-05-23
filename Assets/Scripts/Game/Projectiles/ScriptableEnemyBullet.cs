using System.Collections;
using UnityEngine;

public abstract class ScriptableEnemyBullet<TShooter, TProjectile> : EnemyBullet
    where TShooter : EnemyShooter<TProjectile>
    where TProjectile : Projectile
{
    protected TShooter enemyShooter;
    protected abstract override IEnumerator Move();

    protected override void Awake()
    {
        base.Awake();
        enemyShooter = FindObjectOfType<TShooter>();
    }

    protected TProjectile SpawnBullet(int projectileID, float spawnRotZ, Vector3 spawnPos, bool asLocalPosition = true)
    {
         return enemyShooter.SpawnProjectile(projectileID, spawnRotZ, spawnPos, asLocalPosition);
    }
}