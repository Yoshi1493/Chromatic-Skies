using System.Collections;
using UnityEngine;
using static CoroutineHelper;
using static MathHelper;

public class PiscesBulletSystem31 : EnemyShooter<EnemyBullet>
{
    const int BulletCount = 6;
    const float BulletSpacing = 360f / BulletCount;

    protected override float ShootingCooldown => 1.0f;

    protected override IEnumerator Shoot()
    {
        yield return WaitForSeconds(3f);

        while (enabled)
        {
            float t = RandomAngleDeg;
            Vector3 pos = transform.up.RotateVectorBy(t);
            float r = PlayerPosition.GetRotationDifference(transform.position + pos);

            bulletData.colour = bulletData.gradient.Evaluate(Random.value);

            for (int ii = 0; ii < BulletCount; ii++)
            {
                float z = (ii * BulletSpacing) + r;

                SpawnProjectile(1, z, pos).Fire();
            }

            yield return WaitForSeconds(ShootingCooldown);
        }
    }
}