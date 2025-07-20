using System.Collections;
using UnityEngine;

public abstract class ScriptableBossBullet<TShooter, TProjectile> : BossBullet
    where TShooter : BossShooter<TProjectile>
    where TProjectile : Projectile
{
    TShooter bossShooter;
    protected abstract override IEnumerator Move();

    protected override void Awake()
    {
        base.Awake();
        bossShooter = FindObjectOfType<TShooter>();
    }

    protected TProjectile SpawnBullet(int projectileID, float spawnRotZ, Vector3 spawnPos, bool asLocalPosition = true)
    {
         return bossShooter.SpawnProjectile(projectileID, spawnRotZ, spawnPos, asLocalPosition);
    }
}