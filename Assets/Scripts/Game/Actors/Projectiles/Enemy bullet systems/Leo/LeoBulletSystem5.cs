using System.Collections;
using UnityEngine;
using static CoroutineHelper;
using static MathHelper;

public class LeoBulletSystem5 : EnemyShooter<EnemyBullet>
{
    const int BulletCount = 3;
    const float BulletSpacing = 360f / BulletCount;

    protected override float ShootingCooldown => 2f;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();
        SetSubsystemEnabled(1);

        while (enabled)
        {
            yield return WaitForSeconds(ShootingCooldown);

            float r = RandomAngleDeg;

            for (int i = 0; i < BulletCount; i++)
            {
                float z = (i * BulletSpacing) + r;
                Vector3 pos = Vector3.zero;

                SpawnProjectile(0, z, pos).Fire();
            }

            StartMoveAction?.Invoke();
            yield return WaitForSeconds(ShootingCooldown);
        }
    }
}