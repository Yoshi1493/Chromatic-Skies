using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class LibraBulletSystem5 : EnemyShooter<EnemyBullet>
{
    const int WaveCount = 20;
    const int BranchCount = 36;
    const float BranchSpacing = 360f / BranchCount;
    const float BulletSpawnRadius = 0.5f;
    const float BulletRotationSpeed = 90f;
    const float BulletRotationDuration = 2f;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();
        Application.targetFrameRate = 60;

        while (enabled)
        {
            for (int i = 0; i < WaveCount; i++)
            {
                int d = i % 2 * 2 - 1;

                for (int ii = 0; ii < BranchCount; ii++)
                {
                    float z = ii * BranchSpacing;
                    Vector3 pos = BulletSpawnRadius * transform.up.RotateVectorBy(z);

                    bulletData.colour = bulletData.gradient.Evaluate(ii / (BranchCount - 1f));

                    var bullet = SpawnProjectile(0, z, pos);
                    bullet.StartCoroutine(bullet.RotateBy(d * BulletRotationSpeed, BulletRotationDuration, delay: 1f));
                    bullet.Fire();
                }

                yield return WaitForSeconds(0.05f);
            }


            yield return WaitForSeconds(10f);
        }
    }
}