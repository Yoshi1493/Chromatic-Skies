using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class AriesShooter05 : CommonEnemyShooter<EnemyBullet>
{
    const int WaveCount = 33;
    const int BranchCount = 12;
    const float BranchSpacing = 360f / BranchCount;
    const int BulletCount = 2;
    const float BulletRotationSpeed = 350f;
    const float BulletRotationDuration = 3f;

    protected override IEnumerator Shoot()
    {
        yield return WaitForSeconds(1.5f);

        while (enabled)
        {
            for (int i = 0; i < WaveCount; i++)
            {
                for (int ii = 0; ii < BranchCount; ii++)
                {
                    float r = Random.Range(-i, i) * 0.5f;

                    for (int iii = 0; iii < BulletCount; iii++)
                    {
                        float z = ii * BranchSpacing;
                        Vector3 pos = Vector3.zero;

                        bulletData.colour = bulletData.gradient.Evaluate(iii);

                        var bullet = SpawnProjectile(0, z, pos);
                        bullet.StartCoroutine(bullet.RotateBy((iii % 2 * 2 - 1) * BulletRotationSpeed, BulletRotationDuration));
                        bullet.StartCoroutine(bullet.RotateBy(r, 1f, delay: 3f));
                        bullet.Fire();
                    }
                }

                yield return WaitForSeconds(ShootingCooldown);
            }


            yield return WaitForSeconds(10f);
        }
    }
}