using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class PiscesShooter01 : CommonEnemyShooter<EnemyBullet>
{
    const int BranchCount = 6;
    const float BranchSpacing = 360f / BranchCount;
    const int BulletCount = 12;
    const float BulletBaseSpeed = 2f;
    const float BulletSpeedModifier = 0.6f;
    const float BulletRotationSpeed = 30f;

    protected override IEnumerator Shoot()
    {
        yield return WaitForSeconds(1.5f);

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
                bullet.StartCoroutine(bullet.LerpSpeed(s, 0f, 1f));
                bullet.StartCoroutine(bullet.RotateBy((ii % 2 * 2 - 1) * BulletRotationSpeed, 0f, delay: 1f));
                bullet.Fire();
            }
        }
    }
}