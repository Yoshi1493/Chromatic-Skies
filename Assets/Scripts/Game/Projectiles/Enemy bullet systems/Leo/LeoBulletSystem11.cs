using System.Collections;
using UnityEngine;
using static CoroutineHelper;
using static MathHelper;

public class LeoBulletSystem11 : EnemyShooter<EnemyBullet>
{
    const int BulletCount = 150;
    const float BulletSpacing = 12f;

    protected override float ShootingCooldown => 0.03f;

    protected override IEnumerator Shoot()
    {
        yield return WaitForSeconds(3f);

        float r = Random.Range(0f, 180f);
        int d = PositiveOrNegativeOne;

        for (int i = 0; i < BulletCount; i++)
        {
            float z = (i * BulletSpacing * d) + r;
            Vector3 pos = Vector3.up.RotateVectorBy(z * 0.5f);

            SpawnProjectile(1, z, pos).Fire();

            z += 180f;
            pos = Vector3.up.RotateVectorBy(z * 0.5f);

            SpawnProjectile(1, z, pos).Fire();

            yield return WaitForSeconds(ShootingCooldown);
        }

        enabled = false;
    }
}