using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class AriesBulletSystem3 : EnemyShooter<EnemyBullet>
{
    const int BulletCount = 9;
    const float BulletSpacing = 180f / BulletCount;
    const float MaxAngle = 90f;

    protected override float ShootingCooldown => 0.3f;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        SetSubsystemEnabled(1);
        SetSubsystemEnabled(2);
        yield return WaitForSeconds(4f);

        while (enabled)
        {
            float r = Random.Range(-MaxAngle, MaxAngle);

            for (int i = 0; i < BulletCount; i++)
            {
                float z = (i * BulletSpacing) - ((BulletCount - 1) / 2f * BulletSpacing) + r;
                Vector3 pos = transform.up.RotateVectorBy(r);

                SpawnProjectile(0, z, pos).Fire();
            }

            yield return WaitForSeconds(ShootingCooldown);
        }
    }
}