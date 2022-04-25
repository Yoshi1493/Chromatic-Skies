using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class VirgoBulletSystem1 : EnemyShooter<EnemyBullet>
{
    const int BulletCount = 377;
    readonly float RotationPerWave = (1f + Mathf.Sqrt(5f)) * 180f;

    protected override float ShootingCooldown => 0.0125f;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();
        SetSubsystemEnabled(1);

        int ctr = 0;

        while (enabled)
        {
            float z = ctr * RotationPerWave;
            SpawnProjectile(0, z, Vector2.zero).Fire();

            yield return WaitForSeconds(ShootingCooldown);
            ctr++;
        }
    }
}