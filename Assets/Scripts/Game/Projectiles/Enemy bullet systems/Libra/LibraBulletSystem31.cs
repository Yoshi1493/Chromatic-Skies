using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class LibraBulletSystem31 : EnemyShooter<EnemyBullet>
{
    const int WaveCount = 99;
    const float SafeZone = LibraBulletSystem3.SafeZone;

    protected override float ShootingCooldown => 0.08f;

    protected override IEnumerator Shoot()
    {
        yield return WaitForSeconds(1f);

        for (int i = 0; i < WaveCount; i++)
        {
            float z = 0.4f * Random.Range(-SafeZone, SafeZone);
            Vector3 pos = Vector3.zero;

            bulletData.colour = bulletData.gradient.Evaluate(i / (WaveCount - 1f));
            SpawnProjectile(1, z, pos).Fire();

            yield return WaitForSeconds(ShootingCooldown);            
        }

        enabled = false;
    }
}