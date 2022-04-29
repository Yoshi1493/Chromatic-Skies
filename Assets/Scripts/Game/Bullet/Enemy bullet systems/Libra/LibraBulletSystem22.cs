using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class LibraBulletSystem22 : EnemyBulletSubsystem<EnemyBullet>
{
    const float WaveOffset = 10f;
    const int BranchCount = 4;
    const int BranchSpacing = 360 / BranchCount;
    const int BulletCount = 2;
    const float BulletSpacing = 7.5f;

    protected override float ShootingCooldown => 0.2f;

    protected override IEnumerator Shoot()
    {
        int i = 0;

        while (enabled)
        {
            for (int j = 0; j < BranchCount; j++)
            {
                float z = (i * WaveOffset) + (j * BranchSpacing);
                Vector3 pos = transform.up.RotateVectorBy(z) * Mathf.PingPong(i * 0.1f, 1f);

                SpawnProjectile(0, z - BulletSpacing, pos).Fire();
                SpawnProjectile(1, z + BulletSpacing, pos).Fire();
            }

            i++;
            yield return WaitForSeconds(ShootingCooldown);
        }

    }
}