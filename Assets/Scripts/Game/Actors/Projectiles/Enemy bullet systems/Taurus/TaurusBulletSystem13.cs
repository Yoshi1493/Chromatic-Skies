using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class TaurusBulletSystem13 : EnemyShooter<EnemyBullet>
{
    const int BulletRowCount = 5;
    const int BulletColCount = 5;
    const float BulletSpacing = 0.8f;

    protected override float ShootingCooldown => 0.05f;

    protected override IEnumerator Shoot()
    {
        for (int c = 0; c < BulletColCount; c++)
        {
            for (int r = 0; r < BulletRowCount; r++)
            {
                float x = (r - ((BulletRowCount - 1) / 2f)) * BulletSpacing;
                float y = (c - ((BulletColCount - 1) / 2f)) * BulletSpacing;
                Vector3 v1 = new(x, y);

                Vector3 pos = Vector3.zero;
                float z = v1.GetRotationDifference(pos);

                var bullet = SpawnProjectile(2, z, pos);
                bullet.MoveSpeed = v1.magnitude * 2f;
                bullet.Fire();

                yield return WaitForSeconds(ShootingCooldown);
            }
        }

        enabled = false;
    }
}