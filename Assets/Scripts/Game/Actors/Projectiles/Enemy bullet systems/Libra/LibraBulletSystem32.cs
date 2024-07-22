using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class LibraBulletSystem32 : EnemyShooter<EnemyBullet>
{
    const int WaveCount = 120;
    const float WaveSpacing = 50f;
    const int BulletCount = 3;
    const float BulletSpacing = 360f / BulletCount;
    const float BulletSpawnRadius = 0.9f;
    const float SpawnRadiusModifier = -0.02f;

    protected override float ShootingCooldown => 0.05f;

    protected override IEnumerator Shoot()
    {
        yield return WaitForSeconds(2f);

        for (int i = 0; i < WaveCount; i++)
        {
            for (int ii = 0; ii < BulletCount; ii++)
            {
                float z = (i * WaveSpacing) + (ii * BulletSpacing);
                float t = i * WaveSpacing;
                Vector3 pos = Mathf.PingPong(BulletSpawnRadius + (i * SpawnRadiusModifier), BulletSpawnRadius) * transform.up.RotateVectorBy(t);

                SpawnProjectile(2, z, pos).Fire();
            }

            yield return WaitForSeconds(ShootingCooldown);
        }

        enabled = false;
    }
}