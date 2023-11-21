using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class CancerBulletSystem5 : EnemyShooter<EnemyBullet>
{
    const int BulletCount = 6;
    readonly float BulletSpacing = (1f + Mathf.Sqrt(5f)) * 180f;
    const float BulletSpawnRadius = 0.5f;

    protected override float ShootingCooldown => 0.06f;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        StartMoveAction?.Invoke();

        int i = 0;

        while (enabled)
        {
            for (int ii = 0; ii < BulletCount; ii++)
            {
                float z = i * BulletSpacing;
                Vector3 pos = BulletSpawnRadius * transform.up.RotateVectorBy(z);
                int b = i % 2;

                SpawnProjectile(b, z, pos).Fire();
                i++;
            }

            yield return WaitForSeconds(ShootingCooldown);
            i++;
        }
    }
}