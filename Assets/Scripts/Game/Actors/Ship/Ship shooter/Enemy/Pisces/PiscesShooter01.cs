using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class PiscesShooter01 : CommonEnemyShooter<EnemyBullet>
{
    const int WaveCount = 12;
    const int BranchCount = 6;
    const float BranchSpacing = 360f / BranchCount;
    const float BulletBaseSpeed = 2f;
    const float BulletSpeedModifier = 0.6f;
    const float BulletRotationSpeed = 30f;

    protected override IEnumerator Shoot()
    {
        yield return WaitForSeconds(1.5f);

        float r = PlayerPosition.GetRotationDifference(transform.position);

        for (int i = 0; i < WaveCount; i++)
        {
            for (int ii = 0; ii < BranchCount; ii++)
            {
                float z = (ii * BranchSpacing) + r;
                float s = BulletBaseSpeed + (i * BulletSpeedModifier);
                Vector3 pos = Vector3.zero;

                bulletData.colour = bulletData.gradient.Evaluate(i / (WaveCount - 1f));

                var bullet = SpawnProjectile(1, z, pos);
                bullet.StartCoroutine(bullet.LerpSpeed(s, 0f, 1f));
                bullet.StartCoroutine(bullet.RotateBy((i % 2 * 2 - 1) * BulletRotationSpeed, 0f, delay: 1f));
                bullet.Fire();
            }
        }
    }
}