using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class TaurusBulletSystem52 : EnemyShooter<EnemyBullet>
{
    const int BranchCount = 4;
    const float BranchSpacing = 180f * (BranchCount - 2) / BranchCount;
    const int BulletCount = 5;
    const float BulletSpawnRadius = 2f;

    protected override float ShootingCooldown => 3f;

    protected override IEnumerator Shoot()
    {
        yield return WaitForSeconds(ShootingCooldown);

        while (enabled)
        {
            for (int i = 0; i < BranchCount; i++)
            {
                Vector3 v1 = Vector3.up.RotateVectorBy(i * BranchSpacing);
                Vector3 v2 = Vector3.up.RotateVectorBy((i + 1) * BranchSpacing);

                for (int ii = 0; ii < BulletCount; ii++)
                {
                    float t = (float)ii / BulletCount;
                    Vector3 v3 = BulletSpawnRadius * Vector3.Lerp(v1, v2, t);

                    Vector3 pos = Vector3.zero;
                    float z = v3.GetRotationDifference(pos);

                    var bullet = SpawnProjectile(2, z, pos);
                    bullet.MoveSpeed = v3.magnitude * 2f;
                    bullet.Fire();
                }
            }

            yield return WaitForSeconds(ShootingCooldown);
        }
    }
}