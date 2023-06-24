using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class GeminiBulletSystem1 : EnemyShooter<EnemyBullet>
{
    const float WaveSpacing = 10f;
    const int BranchCount = 2;
    const float BranchSpacing = 360f / BranchCount;
    const int BulletCount = 2;
    const float BulletSpacing = 360f / BulletCount;
    const float BulletSpawnRadius = 2f;

    protected override float ShootingCooldown => 0.2f;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        while (enabled)
        {
            for (int i = 0; i < BulletCount; i++)
            {
                float z = i * BulletSpacing;
                Vector3 pos = Vector3.zero;

                SpawnProjectile(0, z, pos).Fire();
            }

            yield return WaitForSeconds(4f);

            StartMoveAction?.Invoke();

            yield return WaitForSeconds(1f);
            //float t = i * WaveSpacing;

            //for (int ii = 0; ii < BranchCount; ii++)
            //{
            //    for (int iii = 0; iii < BulletCount; iii++)
            //    {
            //        float z =  t + (ii * BulletSpacing * 0.5f) + (iii * BulletSpacing);
            //        Vector3 pos = BulletSpawnRadius * transform.up.RotateVectorBy(-t + (ii * BranchSpacing));

            //        SpawnProjectile(0, z, pos).Fire();
            //    }
            //}

            //yield return WaitForSeconds(ShootingCooldown);
            //i++;
        }
    }
}