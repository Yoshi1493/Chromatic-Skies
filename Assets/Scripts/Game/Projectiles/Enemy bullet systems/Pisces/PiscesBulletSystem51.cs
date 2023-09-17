using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class PiscesBulletSystem51 : EnemyShooter<EnemyBullet>
{
    const float WaveSpacing = 9f;

    protected override float ShootingCooldown => 0.05f;

    protected override IEnumerator Shoot()
    {
        for (int i = 0; enabled; i++)
        {
            float z = -i * WaveSpacing;
            Vector3 pos = Vector3.zero;

            SpawnProjectile(0, z, pos).Fire();
            yield return WaitForSeconds(ShootingCooldown);
        }
    }
}