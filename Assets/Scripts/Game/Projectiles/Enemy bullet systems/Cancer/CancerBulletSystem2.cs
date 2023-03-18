using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class CancerBulletSystem2 : EnemyShooter<EnemyBullet>
{
    const int WaveCount = 2;
    const float WaveSpacing = BulletSpacing / WaveCount;
    const int BulletCount = 6;
    const float BulletSpacing = 360f / BulletCount;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        while (enabled)
        {
            for (int i = 0; i < WaveCount; i++)
            {
                for (int ii = 0; ii < BulletCount; ii++)
                {
                    float z = (i * WaveSpacing) + (ii * BulletSpacing);
                    Vector3 pos = Vector3.zero;

                    SpawnProjectile(0, z, pos).Fire();
                }

                yield return WaitForSeconds(5f);
            }

            yield return WaitForSeconds(1f);

            StartMoveAction?.Invoke();
            yield return WaitForSeconds(1f);
        }
    }
}