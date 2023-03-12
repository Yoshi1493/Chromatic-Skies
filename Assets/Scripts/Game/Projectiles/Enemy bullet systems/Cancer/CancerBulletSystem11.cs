using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class CancerBulletSystem11 : EnemyShooter<EnemyBullet>
{
    const float ArcHalfWidth = 45f;
    const int WaveCount = 20;
    const int BulletCount = 2;

    protected override float ShootingCooldown => 0.08f;

    protected override IEnumerator Shoot()
    {
        for (int i = 0; i < WaveCount; i++)
        {
            for (int ii = 0; ii < BulletCount; ii++)
            {
                float z = Random.Range(-ArcHalfWidth, ArcHalfWidth) + 180f;
                Vector3 pos = Vector3.zero;

                SpawnProjectile(2, z, pos).Fire();
            }

            yield return WaitForSeconds(ShootingCooldown);
        }

        enabled = false;
    }
}