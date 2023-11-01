using System.Collections;
using UnityEngine;

public class TaurusBulletSystem32 : EnemyShooter<EnemyBullet>
{
    const int BranchCount = 12;
    const float BranchSpacing = 360f / BranchCount;
    const int BulletCount = 6;
    const float BulletBaseSpeed = 2.2f;
    const float BulletSpeedModifier = 0.3f;

    protected override IEnumerator Shoot()
    {
        float r = PlayerPosition.GetRotationDifference(transform.position);

        for (int i = 0; i < BranchCount; i++)
        {
            for (int ii = 0; ii < BulletCount; ii++)
            {
                float z = ((i + 0.5f) * BranchSpacing) + r;
                float s = BulletBaseSpeed + (ii * BulletSpeedModifier);
                Vector3 pos = Vector3.zero;

                bulletData.colour = bulletData.gradient.Evaluate(ii / (BulletCount - 1f));
                var bullet = SpawnProjectile(1, z, pos);
                bullet.MoveSpeed = s;
                bullet.Fire();
            }
        }

        yield return enabled = false;
    }
}