using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class PiscesBulletSystem5 : EnemyShooter<EnemyBullet>
{
    const int BulletCount = 25;
    const float BulletSpacing = 360f / (BulletCount - 1);
    const float BulletSpawnRadius = 1f;
    const float SpawnRadiusMultiplier = 1f / BulletCount;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        SetSubsystemEnabled(1);

        for (int i = 1; enabled; i *= -1)
        {
            float r = PlayerPosition.GetRotationDifference(transform.position);

            for (int ii = 0; ii < BulletCount; ii++)
            {
                float z = i * ii * BulletSpacing + r;
                Vector3 pos = (BulletSpawnRadius - (ii * SpawnRadiusMultiplier)) * transform.up.RotateVectorBy(z);

                SpawnProjectile(0, z, pos).Fire();
                yield return WaitForSeconds(ShootingCooldown);
            }

            yield return WaitForSeconds(3f);

            StartMoveAction?.Invoke();
            yield return WaitForSeconds(3f);
        }
    }
}