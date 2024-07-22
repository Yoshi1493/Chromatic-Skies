using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class LibraBulletSystem31 : EnemyShooter<EnemyBullet>
{
    const int WaveCount = 60;
    const float WaveSpacing = 15f;

    protected override IEnumerator Shoot()
    {
        yield return WaitForSeconds(1f);

        for (int i = 0; i < WaveCount; i++)
        {
            float z = i * WaveSpacing;
            Vector3 pos = Vector3.zero;

            SpawnProjectile(1, z, pos).Fire();
            yield return WaitForSeconds(ShootingCooldown);
        }

        enabled = false;
    }
}