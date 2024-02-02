using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class ScorpioBulletSystem3 : EnemyShooter<EnemyBullet>
{
    const int BulletCount = 30;
    const float BulletSpacing = 360f / BulletCount;
    const float BulletRotationSpeed = 45f;
    public const float BulletRotationDuration = 3f;

    protected override float ShootingCooldown => 0.5f;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        for(int i = 1; enabled; i *= -1)
        {
            for (int ii = 0; ii < BulletCount; ii++)
            {
                float z = 0f;
                Vector3 pos = Vector3.zero;

                SpawnProjectile(0, z, pos).Fire();
                yield return WaitForSeconds(ShootingCooldown);
            }
        }
    }
}