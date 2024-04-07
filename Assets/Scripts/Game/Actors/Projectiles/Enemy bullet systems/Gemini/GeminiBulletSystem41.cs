using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class GeminiBulletSystem41 : EnemyShooter<EnemyBullet>
{
    const int BranchCount = 6;
    const float BranchSpacing = 360f / BranchCount;
    const float ArcHalfWidth = 60f;
    const float BulletSpacing = 10f;
    const float BulletSpawnRadius = 1.0f;
    const float SpawnRadiusMultiplier = 0.02f;

    protected override float ShootingCooldown => 0.15f;

    protected override IEnumerator Shoot()
    {
        yield return WaitForSeconds(2f);

        for (int i = 0; enabled; i++)
        {
            for (int ii = 0; ii < BranchCount; ii++)
            {
                float t = Mathf.Sin(i * BulletSpacing * Mathf.Deg2Rad);
                float z = (t * ArcHalfWidth) + (ii * BranchSpacing);
                Vector3 pos = Mathf.PingPong(i * SpawnRadiusMultiplier, BulletSpawnRadius) * transform.up.RotateVectorBy(z);

                SpawnProjectile(2, z, pos).Fire();
            }

            yield return WaitForSeconds(ShootingCooldown);
        }
    }
}