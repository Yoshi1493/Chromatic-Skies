using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class VirgoBulletSystem1 : EnemyShooter<EnemyBullet>
{
    readonly float BulletSpacing = (1f + Mathf.Sqrt(5f)) * 180f;

    protected override float ShootingCooldown => 0.02f;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        SetSubsystemEnabled(1);
        StartCoroutine(Move());

        int i = 0;

        while (enabled)
        {
            float z = i * BulletSpacing;
            SpawnProjectile(0, z, Vector2.zero).Fire();

            yield return WaitForSeconds(ShootingCooldown);
            i++;
        }
    }

    IEnumerator Move()
    {
        while (enabled)
        {
            yield return ownerShip.MoveToRandomPosition(1f, 1f, 2f, 3f);
        }
    }
}