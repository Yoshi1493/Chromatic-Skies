using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class PiscesBulletSystem61 : EnemyShooter<EnemyBullet>
{
    const int BulletCount = 6;

    protected override float ShootingCooldown => PiscesMovementSystem6.MoveDuration / BulletCount;

    protected override IEnumerator Shoot()
    {
        for (int i = 0; i < BulletCount; i++)
        {
            yield return WaitForSeconds(ShootingCooldown);

            float z = 0f;
            Vector3 pos = Vector3.zero;

            bulletData.colour = bulletData.gradient.Evaluate(i / (BulletCount - 1f));
            SpawnProjectile(1, z, pos).Fire();
        }

        enabled = false;
    }
}