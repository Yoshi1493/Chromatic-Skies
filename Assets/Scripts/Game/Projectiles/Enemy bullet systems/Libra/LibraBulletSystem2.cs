using System.Collections;
using UnityEngine;
using static CoroutineHelper;
using static MathHelper;

public class LibraBulletSystem2 : EnemyShooter<EnemyBullet>
{
    const int BulletCount = 3;
    const float BulletSpacing = 360f / BulletCount;

    protected override float ShootingCooldown => 5f;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        while (enabled)
        {
            float r = RandomAngleDeg;

            for (int i = 0; i < BulletCount; i++)
            {
                float z = i * BulletSpacing + r;
                Vector3 pos = Vector3.zero;

                bulletData.colour = bulletData.gradient.Evaluate(i / (BulletCount - 1f));
                SpawnProjectile(0, z, pos).Fire();
            }

            yield return WaitForSeconds(ShootingCooldown);
            StartMoveAction?.Invoke();
            yield return WaitForSeconds(1f);
        }
    }
}