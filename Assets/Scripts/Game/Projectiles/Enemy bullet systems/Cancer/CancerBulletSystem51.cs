using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class CancerBulletSystem51 : EnemyShooter<EnemyBullet>
{
    const float SpawnMaxAngle = 30f;

    protected override float ShootingCooldown => 3f;

    protected override IEnumerator Shoot()
    {
        while (enabled)
        {
            yield return WaitForSeconds(ShootingCooldown);

            float z = Random.Range(-SpawnMaxAngle, SpawnMaxAngle);
            Vector3 pos = Vector3.zero;

            SpawnProjectile(2, z, pos).Fire();
        }
    }
}