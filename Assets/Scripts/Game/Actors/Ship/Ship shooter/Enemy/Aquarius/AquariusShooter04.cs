using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class AquariusShooter04 : EnemyShooter<EnemyBullet>
{
    const int WaveCount = 3;
    const int BulletCount = 3;
    const float BulletSpacing = 15f;

    protected override IEnumerator Shoot()
    {
        yield return WaitForSeconds(2.5f);

        float r = PlayerPosition.GetRotationDifference(transform.position);

        for (int i = 0; i < WaveCount; i++)
        {
            for (int ii = 0; ii < BulletCount; ii++)
            {
                float z = ((ii - (BulletCount - 1) / 2) * BulletSpacing) + r;
                Vector3 pos = Vector3.zero;

                SpawnProjectile(4, z, pos).Fire();
            }

            yield return WaitForSeconds(ShootingCooldown);
        }

    }
}