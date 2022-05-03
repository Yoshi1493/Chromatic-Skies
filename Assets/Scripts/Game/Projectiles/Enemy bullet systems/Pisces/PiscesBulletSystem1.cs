using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class PiscesBulletSystem1 : EnemyShooter<EnemyBullet>
{
    const int BulletCount = 24;
    const int BulletSpacing = 360 / BulletCount;

    protected override float ShootingCooldown => 0.05f;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        while (enabled)
        {
            for (int i = 0; i < BulletCount; i++)
            {
                float z = i * BulletSpacing;
                float spd = 2 + (i / 10f);

                var bullet = SpawnProjectile(0, z, Vector3.zero);
                bullet.MoveSpeed = spd;
                bullet.Fire();

                bullet = SpawnProjectile(0, z + (0.5f * BulletSpacing), Vector3.zero);
                bullet.MoveSpeed = spd * 0.8f;
                bullet.Fire();

                yield return WaitForSeconds(ShootingCooldown);
            }

            yield return WaitForSeconds(ShootingCooldown * 2f);

            for (int i = 0; i < BulletCount; i++)
            {
                float z = (BulletCount - i) * BulletSpacing;
                float spd = 2 + (i / 10f);

                var bullet = SpawnProjectile(0, z, Vector3.zero);
                bullet.MoveSpeed = spd;
                bullet.Fire();

                bullet = SpawnProjectile(0, z - (0.5f * BulletSpacing), Vector3.zero);
                bullet.MoveSpeed = spd * 0.8f;
                bullet.Fire();

                yield return WaitForSeconds(ShootingCooldown);
            }

            yield return WaitForSeconds(ShootingCooldown * 2f);

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 30; j++)
                {
                    float z = j * 12f;
                    SpawnProjectile(1, z, Vector3.zero).Fire();
                }

                yield return ownerShip.MoveToRandomPosition(0.4f + (i / 10f), minSqrMagDelta: 1, maxSqrMagDelta: 2);
            }

            yield return WaitForSeconds(1f);
        }
    }
}