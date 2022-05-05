using System.Collections;
using UnityEngine;

public abstract class ScriptableEnemyBullet<T> : EnemyBullet
    where T : EnemyShooter<EnemyBullet>
{
    protected T enemyShooter;
    protected abstract override IEnumerator Move();

    protected override void Awake()
    {
        base.Awake();
        enemyShooter = FindObjectOfType<T>();
    }

    protected EnemyBullet SpawnBullet(int projectileID, float spawnRotZ, Vector3 spawnPos, bool asLocalPosition = true)
    {
         return enemyShooter.SpawnProjectile(projectileID, spawnRotZ, spawnPos, asLocalPosition);
    }
}