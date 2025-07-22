using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class AquariusShooter01 : EnemyShooter<EnemyBullet>
{
    const int WaveCount = 2;
    const int BulletCount = 3;
    const float BulletSpacing = 3f;

    protected override float ShootingCooldown => 1.5f;

    protected override IEnumerator Shoot()
    {
        yield return WaitForSeconds(1f);

        for (int i = 0; i < WaveCount; i++)
        {
            for (int ii = 0; ii < BulletCount; ii++)
            {
                float r = PlayerPosition.GetRotationDifference(transform.position);
                float z = ((ii - (BulletCount - 1) / 2) * BulletSpacing) + r;
                Vector3 pos = Vector3.zero;

                SpawnProjectile(1, z, pos).Fire();
            }

            yield return WaitForSeconds(ShootingCooldown);
        }
    }
}