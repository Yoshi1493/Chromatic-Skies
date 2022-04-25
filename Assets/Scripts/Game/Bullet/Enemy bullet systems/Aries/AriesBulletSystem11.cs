using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class AriesBulletSystem11 : EnemyShooter<EnemyBullet>
{
    const int WaveCount = 6;
    const int BulletCount = 12;
    const int BulletSpacing = 360 / BulletCount;

    protected override float ShootingCooldown => 0.5f;

    protected override IEnumerator Shoot()
    {
        yield return WaitForSeconds(3f);

        for (int i = 0; i < WaveCount; i++)
        {
            float randOffset = Random.Range(0f, BulletSpacing * 0.5f);
            float theta = PlayerPosition.GetRotationDifference(transform.position);

            for (int j = 0; j < BulletCount; j++)
            {
                float z = (j * BulletSpacing) + theta;
                SpawnProjectile(1, z, Vector3.zero).Fire();
            }

            yield return WaitForSeconds(ShootingCooldown);
        }

        enabled = false;
    }
}